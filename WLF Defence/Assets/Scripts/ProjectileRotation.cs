
using UnityEngine;

public class ProjectileRotation : MonoBehaviour
{

	private Rigidbody2D _rb;
	void Start ()
	{
		_rb = GetComponent<Rigidbody2D>();
	}
	
	
	void Update () 
	{
		Vector3 dir = _rb.velocity;
		var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
}
