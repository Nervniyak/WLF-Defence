
using System.Collections;
using UnityEngine;
using Pathfinding;
// ReSharper disable All

[RequireComponent(typeof(Seeker))]
public class AIforMelee : MonoBehaviour
{

	public Transform Target;

	public float UpdateRate = 2f;

	private Seeker seeker;
	private Rigidbody2D rb;

	public Path Path;
	public float Speed = 300f;
	public bool Grounded = false;
	public ForceMode2D FMode;

	public bool PathIsEnded = false;
	public float NextWaypointDistance = 3f;

	private int CurrentWaypoint = 0;
	void Start ()
	{
		seeker = GetComponent<Seeker>();
		rb = GetComponent<Rigidbody2D>();

		if (Target == null)
		{
			return;
		}
		seeker.StartPath(transform.position, Target.position, OnPathComplete);

		StartCoroutine(UpdatePath());
	}
	
	
	void FixedUpdate () 
	{
		if (Target == null )
		{
			var gObj = GameObject.FindGameObjectWithTag("Player");
			if (gObj)
			{
				Target = gObj.transform;
				seeker.StartPath(transform.position, Target.position, OnPathComplete);

				StartCoroutine(UpdatePath());
			}
		   
			return;
		}
		if (Path == null)
		{
			return;
		}

		if (CurrentWaypoint >= Path.vectorPath.Count)
		{
			if (PathIsEnded)
			{
				return;
			}
			PathIsEnded = true;
			return;
		}
		PathIsEnded = false;
		Vector3 dir = (Path.vectorPath[CurrentWaypoint] - transform.position).normalized;
		dir *= Speed*Time.fixedDeltaTime;

		if (Grounded)
		{
			rb.AddForce(dir, FMode);
		}
		

		float dist = Vector3.Distance(transform.position, Path.vectorPath[CurrentWaypoint]);

		if (dist < NextWaypointDistance)
		{
			CurrentWaypoint++;
			return;
		}
	}

	public IEnumerator UpdatePath()
	{
		if (Target == null)
		{
			yield return false;
		}

		seeker.StartPath(transform.position, Target.position, OnPathComplete);
		yield return new WaitForSeconds(1f/UpdateRate);
		StartCoroutine(UpdatePath());

	}

	public void OnPathComplete(Path p)
	{
		if (!p.error)
		{
			Path = p;
			CurrentWaypoint = 0;
		}
	}
}
