using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float JumpForce;
    public Transform GroundCheck;
    public LayerMask WhatIsGround;

    private Rigidbody2D _rb;
    private Transform _transform;
    private Animator _animator;
    private AIforMelee _AI;

    private bool _facingRight;
    private bool _grounded;
    private float _groundRadius = 0.2f;




    [Space]
    public GameObject MeleeType1;
    public Transform MeleeSpawn;
    public float AttackVelocity;


    void Update()
    {
        _animator.SetBool("Melee", false);
    }

    void Start()
    {
        _transform = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _AI = GetComponent<AIforMelee>();
        StartCoroutine(Attack());
        StartCoroutine(Jump());
    }


    void FixedUpdate()
    {
        _grounded = Physics2D.OverlapCircle(GroundCheck.position, _groundRadius, WhatIsGround);
        _AI.Grounded = _grounded;
        _animator.SetBool("Grounded", _grounded);
        _animator.SetFloat("vSpeed", _rb.velocity.y);

        if (_rb.velocity.x >= 0.2)
        {
            _animator.SetFloat("Movement", Mathf.Abs(_rb.velocity.x));
        }

        if (_rb.velocity.x > 0.4 && !_facingRight)
        {
            Flip();
        }
        else if (_rb.velocity.x < -0.4 && _facingRight)
        {
            Flip();
        }



    }


    IEnumerator Attack()
    {
        while (true)
        {
            Vector2 movement;
            if (_grounded)
            {
                movement = _facingRight ? new Vector2(0.75f, 1) : new Vector2(-0.75f, 1);

            }
            else
            {
                movement = _facingRight ? new Vector2(0.75f, 0) : new Vector2(-0.75f, 0);
            }
            _rb.AddForce(movement * JumpForce);





            _animator.SetBool("Melee", true);
            var meleeClone = Instantiate(MeleeType1, MeleeSpawn.position, MeleeSpawn.rotation);
            meleeClone.GetComponent<ProjectilePenetrative>().SetOwnerName(tag);
            var cloneRb = meleeClone.GetComponent<Rigidbody2D>();

            if (_facingRight)
            {
                cloneRb.AddForce(new Vector2(AttackVelocity + _rb.velocity.x, 0));
            }
            else
            {
                cloneRb.AddForce(new Vector2(-AttackVelocity + _rb.velocity.x, 0));
                Vector3 theScale = _transform.localScale;     
                    theScale.x *= -1;
                

                meleeClone.transform.localScale = theScale;
            }
            cloneRb.AddForce(_facingRight
                ? new Vector2(AttackVelocity + _rb.velocity.x, 0)
                : new Vector2(-AttackVelocity + _rb.velocity.x, 0));


            yield return new WaitForSeconds(Random.Range(1.20f, 3.5f));
        }
    }

    IEnumerator Jump()
    {
        while (true)
        {

            if (_grounded)
            {
                _rb.AddForce(new Vector3(0, 0.75f) * JumpForce);
            }
            yield return new WaitForSeconds(Random.Range(0.6f, 0.85f));
        }
    }


    void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 theScale = _transform.localScale;
        theScale.x *= -1;
        _transform.localScale = theScale;
    }

}
