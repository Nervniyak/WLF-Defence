using Assets.Scripts;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int MaxHP;
    public float RecoveryRate;
    public float HP { get; set; }

    private float _nextRecieveDamage;
    private float _ghostLeaveTime;

    void Start()
    {
        HP = MaxHP;
    }
    void Update()
    {
        HP = Mathf.MoveTowards(HP, MaxHP, RecoveryRate*Time.deltaTime);
        if (HP <= 0)
        {
            if (CompareTag("Player"))
            {
                GameObject.FindGameObjectWithTag("GameController").SendMessage("GameOver");
                return;
            }
            if (CompareTag("NPC"))
            {
                GameObject.FindGameObjectWithTag("Player").SendMessage("ReceiveXP", GetComponent<Reward>().XP);
            }
            Destroy(gameObject);
        }

        if (CompareTag("Player"))
        {
            if (Time.time > _ghostLeaveTime) gameObject.layer = 8;
        };
    }

    void Damaged(DamagePackage damagePackage)
    {
        if (tag == damagePackage.OwnerName)
        {
            return;
        }
        if (CompareTag("Player"))   
        {
            if (!(Time.time > _nextRecieveDamage)) return;
            _nextRecieveDamage = Time.time + 0.1f;
            _ghostLeaveTime = _nextRecieveDamage + 0.4f;
            gameObject.layer = 31;
        }
        HP -= damagePackage.Damage;
    } 
}
