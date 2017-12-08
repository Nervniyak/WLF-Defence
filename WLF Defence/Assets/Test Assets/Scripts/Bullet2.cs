using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2 : MonoBehaviour {

    void OnCollisionEnter(Collision collision)
    {
        GameObject hit = collision.gameObject;
        Health2 health = hit.GetComponent<Health2>();
        if (health != null)
        {
            health.TakeDamage(10);
        }
        Destroy(gameObject);
    }
}
