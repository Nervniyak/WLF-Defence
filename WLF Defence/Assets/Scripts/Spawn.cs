using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Spawn : MonoBehaviour
{
	public string OwnerNameIfNeeded ;
	public GameObject Dummy;
	public Transform SpawnPoint;
	public float SpawnRate;
	public float ForcePush;
	public int SpawnLimit;
	public int MaximumSpawnLimit;
	public float TimeUntilLimitRise;

	public List<GameObject> SpawnedOnes;

	private float _nextSpawn;
	private bool _stop;

	void Start()
	{
		SpawnedOnes = new List<GameObject>(SpawnLimit);
		StartCoroutine(OnCoroutine());
	}
	void Update ()
	{
		if (Time.time > _nextSpawn)
		{ 
			_nextSpawn = Time.time + SpawnRate;
			if (SpawnedOnes.Count < SpawnLimit)
			{
				var clone = (Instantiate(Dummy, SpawnPoint.position, SpawnPoint.rotation));
				SpawnedOnes.Add(clone);
				if (clone.GetComponent<Projectile>() != null)
				{
					clone.GetComponent<Projectile>().SetOwnerName(OwnerNameIfNeeded);
				}
				clone.GetComponent<Rigidbody2D>().AddForce(transform.right * ForcePush);
			}
		}
		for (var i = 0; i < SpawnedOnes.Count-1; i++)
		{
			if (SpawnedOnes[i] == null)
			{
				SpawnedOnes.RemoveAt(i);
			}
		}
	}

	IEnumerator OnCoroutine()
	{
		while (!_stop)
		{
		    SpawnRate *= 0.90f;
			yield return new WaitForSeconds(TimeUntilLimitRise);
			if (SpawnLimit >= MaximumSpawnLimit)
			{
				_stop = true;
			}
			else
			{
				SpawnLimit++;
			}			
		}
	}
}
