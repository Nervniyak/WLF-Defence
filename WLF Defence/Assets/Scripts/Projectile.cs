using Assets.Scripts;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	public int Damage;
	
	public GameObject Blast;

	public string OwnerName;
	void Start ()
	{
		if (OwnerName == "")    OwnerName = "Neutral";
	}
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		if (OwnerName == collision.gameObject.tag )
		{
			return;
		}
		if (collision.gameObject.CompareTag("NPC") || collision.gameObject.CompareTag("Player"))
		{
			collision.gameObject.SendMessage("Damaged", new DamagePackage(Damage, OwnerName), SendMessageOptions.RequireReceiver);
		}
		
		Destroy(gameObject);
		if (Blast != null)
		{
			GameObject clone = Instantiate(Blast, transform.position, transform.rotation);
			Destroy(clone, 1.0f);
		}	
	}

	public void SetOwnerName(string otag)
	{
		OwnerName = otag;
	}
}
