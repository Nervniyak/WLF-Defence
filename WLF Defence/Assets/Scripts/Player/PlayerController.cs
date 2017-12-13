using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    public float JumpForce;

    [Space]
    public GameObject MeleeType1;
    public GameObject MeleeType2;
    public Transform MeleeSpawn;
    public Transform ShockWaveSpawn1;
    public Transform ShockWaveSpawn2;
    public float AttackCooldown;
    public float AerialAttackCooldown;
    public float UltimateAttackCooldown;
    public float AttackVelocity;

    [Space]
    public GameObject ProjectileType;
    public Transform ShotSpawn;
    public GameObject ShotBlast;
    public Transform ShotBlastSpawn;
    public float FireCooldown;
    public float FireVelocity;
    public float RecoilStrength;

    [Space]
    public Transform GroundCheck;
    public LayerMask WhatIsGround;


    private Rigidbody2D _rb;
    private Transform _transform;
    private Animator _animator;

    private bool _facingRight;
    private bool _grounded;
    private float _groundRadius = 0.15f;
    private float _nextJump;

    private float _nextFire;
    private float _nextAttack;
    private float _nextAerialAttack;

    private bool _aerialAttackCharged;

    [HideInInspector]
    public bool UltimateReady;
    [HideInInspector]
    public float NextUltimateAttack;


    void Start()
    {
        _transform = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        UltimateReady = Time.time >= NextUltimateAttack;
        Shoot();
        Melee();
    }

    void FixedUpdate()
    {
        _grounded = Physics2D.OverlapCircle(GroundCheck.position, _groundRadius, WhatIsGround);
        _animator.SetBool("Grounded", _grounded);
        _animator.SetFloat("vSpeed", _rb.velocity.y);
        Move();
        Jump();

        if (_grounded && _aerialAttackCharged)
        {
            _aerialAttackCharged = false;

            SpawnMelee(false, MeleeType1, ShockWaveSpawn1);
            SpawnMelee(true, MeleeType1, ShockWaveSpawn1);

            if (Time.time > NextUltimateAttack)
            {
                NextUltimateAttack = Time.time + UltimateAttackCooldown;
                SpawnMelee(false, MeleeType2, ShockWaveSpawn2);
                SpawnMelee(true, MeleeType2, ShockWaveSpawn2);
            }
        }
    }



    void Move()
    {
        float moveHorizontal;
#if UNITY_ANDROID
        moveHorizontal = CrossPlatformInputManager.GetAxis("Horizontal");                                              //INPUT DANGER
#else
        moveHorizontal = Input.GetAxisRaw("Horizontal");                                        //INPUT DANGER
#endif

        var movement = new Vector2(moveHorizontal, 0.0f);
        _rb.AddForce(movement * Speed);

        if (moveHorizontal > 0 && !_facingRight)
        {
            Flip();
        }
        else if (moveHorizontal < 0 && _facingRight)
        {
            Flip();
        }
        _animator.SetFloat("Movement", Mathf.Abs(moveHorizontal));
    }

    void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 theScale = _transform.localScale;
        theScale.x *= -1;
        _transform.localScale = theScale;
    }

    void Jump()
    {
        bool jumpPressed;
#if UNITY_ANDROID
        jumpPressed = CrossPlatformInputManager.GetAxis("Vertical") > 0.8;                                            //INPUT DANGER
#else
        jumpPressed = Input.GetButtonDown("Jump");                                        //INPUT DANGER
#endif
        if (_grounded && jumpPressed)                                               //INPUT DANGER
        {
            if (!(Time.time > _nextJump)) return;
            _animator.SetBool("Grounded", false);
            _rb.AddForce(new Vector2(0, JumpForce));
            _nextJump = Time.time + 0.1f;
        }
    }

    void Melee()
    {
        _animator.SetBool("Melee", false);

        bool meleePressed;
#if UNITY_ANDROID
        meleePressed = CrossPlatformInputManager.GetButtonDown("Fire1");                                            //INPUT DANGER
#else
        meleePressed = Input.GetButtonDown("Fire1");                                        //INPUT DANGER
#endif

        if (meleePressed)                                                           //INPUT DANGER
        {
            if (_grounded)
            {
                if (!(Time.time > _nextAttack)) return;
                _nextAttack = Time.time + AttackCooldown;
                _animator.SetBool("Melee", true);
                SpawnMelee(false, MeleeType1, MeleeSpawn);
            }
            else
            {
                if (!(Time.time > _nextAerialAttack)) return;
                _nextAerialAttack = Time.time + AerialAttackCooldown;
                _rb.AddForce(new Vector2(0, -(JumpForce * 2.5f)));
                _aerialAttackCharged = true;
            }
        }
    }

    void SpawnMelee(bool reverse, GameObject projectileType, Transform spawn)
    {
        bool localFacingRight;
        if (reverse)
        {
            localFacingRight = !_facingRight;
        }
        else
        {
            localFacingRight = _facingRight;
        }

        var meleeClone = Instantiate(projectileType, spawn.position, spawn.rotation);
        meleeClone.GetComponent<ProjectilePenetrative>().SetOwnerName(tag);
        var cloneRb = meleeClone.GetComponent<Rigidbody2D>();

        if (localFacingRight)
        {
            cloneRb.AddForce(new Vector2(AttackVelocity + _rb.velocity.x, 0));
        }
        else
        {
            cloneRb.AddForce(new Vector2(-AttackVelocity + _rb.velocity.x, 0));
            Vector3 theScale = _transform.localScale;
            if (!reverse)
            {
                theScale.x *= -1;
            }

            meleeClone.transform.localScale = theScale;
        }
        cloneRb.AddForce(localFacingRight
            ? new Vector2(AttackVelocity + _rb.velocity.x, 0)
            : new Vector2(-AttackVelocity + _rb.velocity.x, 0));
    }

    void Shoot()
    {
        _animator.SetBool("Shoot", false);

        bool shootPressed;
#if UNITY_ANDROID
        shootPressed = CrossPlatformInputManager.GetButtonDown("Fire2");                                            //INPUT DANGER
#else
        shootPressed = Input.GetButtonDown("Fire2");                                        //INPUT DANGER
#endif

        if (shootPressed && Time.time > _nextFire)                                   //INPUT DANGER
        {
            _animator.SetBool("Shoot", true);
            _nextFire = Time.time + FireCooldown;
            GameObject bulletClone = Instantiate(ProjectileType, ShotSpawn.position, ShotSpawn.rotation);
            bulletClone.GetComponent<Projectile>().SetOwnerName(tag);
            var cloneRb = bulletClone.GetComponent<Rigidbody2D>();

            if (_facingRight)
            {
                cloneRb.AddForce(new Vector2(FireVelocity + _rb.velocity.x, 0));
            }
            else
            {
                cloneRb.AddForce(new Vector2(-FireVelocity + _rb.velocity.x, 0));
                Vector3 theScale = _transform.localScale;
                theScale.x *= -1;
                bulletClone.transform.localScale = theScale;
            }
            cloneRb.AddForce(_facingRight
                ? new Vector2(FireVelocity + _rb.velocity.x, 0)
                : new Vector2(-FireVelocity + _rb.velocity.x, 0));
            var verticalRecoil = _grounded ? RecoilStrength * 0.5f : 0f;
            _rb.AddForce(!_facingRight
               ? new Vector2(RecoilStrength + _rb.velocity.x, verticalRecoil)
               : new Vector2(-RecoilStrength + _rb.velocity.x, verticalRecoil));
            GameObject blastClone = Instantiate(ShotBlast, ShotBlastSpawn.position, ShotBlastSpawn.rotation);

            Destroy(blastClone, 1.0f);
        }
    }

    public void LevelUp()
    {
        Speed += 0.8f;
        JumpForce += 12f;
        AttackCooldown *= 0.97f;
        AerialAttackCooldown *= 0.95f;
        UltimateAttackCooldown *= 0.90f;
        FireCooldown *= 0.95f;
        Health health = GetComponent<Health>();
        health.RecoveryRate *= 1.01f;
        health.MaxHP = (int)(health.MaxHP * 1.03f);
        health.HP = health.MaxHP;
        NextUltimateAttack = Time.time - UltimateAttackCooldown;
    }
}
