using Assets.Scripts;
using UnityEngine;

public class DamageCollide : MonoBehaviour
{

    public int Damage;
    public float Force;
    public float KnockForce;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Friendly") || collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SendMessage("Damaged", new DamagePackage(Damage, tag), SendMessageOptions.RequireReceiver);
            collision.gameObject.GetComponent<Rigidbody2D>()
                .AddForce(new Vector2(-1*Force*gameObject.transform.localScale.x, KnockForce));
        }
    }
}
