using UnityEngine;
using Assets.Scripts;

public class ProjectilePenetrative : MonoBehaviour
{
    public int Damage;
    public int Force;
    public int KnockForce;

    public string OwnerName;

    private void Start()
    {
        if (OwnerName == "") OwnerName = "Neutral";
        Destroy(gameObject, 0.10f);
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (OwnerName == collider.gameObject.tag)
        {
            return;
        }
        if (collider.gameObject.CompareTag("NPC") || collider.gameObject.CompareTag("Player"))
        {
            collider.gameObject.SendMessage("Damaged", new DamagePackage(Damage,OwnerName), SendMessageOptions.RequireReceiver);
            collider.gameObject.GetComponent<Rigidbody2D>()
                .AddForce(new Vector2(Force*gameObject.transform.localScale.x, KnockForce));
        }
    }

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (OwnerName == collision.gameObject.tag)
    //    {
    //        return;
    //    }
    //    if (collision.gameObject.CompareTag("NPC") || collision.gameObject.CompareTag("Player"))
    //    {
    //        collision.gameObject.SendMessage("Damaged", Damage, SendMessageOptions.RequireReceiver);
    //        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Force * -gameObject.transform.localScale.x ,0));
    //    }
    //}

    public void SetOwnerName(string otag)
    {
        OwnerName = otag;
    }

  
   
}


