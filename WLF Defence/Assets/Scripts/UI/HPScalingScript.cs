using UnityEngine;

public class HPScalingScript : MonoBehaviour
{
	public float Maxscale = 2;
	public Health Health1 { get; set; }
	public float MaxHealth { get; set; }

	private Material material;

	void Start ()
	{
		material = GetComponent<Renderer>().material;
		
		Health1 = GameObject.FindWithTag("Player").GetComponent<Health>();
		MaxHealth = Health1.MaxHP;
		transform.localScale = new Vector3(Maxscale, 1.0f, 1.0f);
		transform.localPosition = new Vector3((transform.localScale.x /2), 0, 0);
	}
	
	void Update ()
	{
		if (Health1.enabled)
		{
			MaxHealth = Health1.MaxHP;
			transform.localScale = new Vector3((Health1.HP/MaxHealth)*Maxscale, 1.0f, 1.0f);
			transform.localPosition = new Vector3((transform.localScale.x/2), 0, 0);
			var red = (byte) (Mathf.Abs(1 - (transform.localScale.x/Maxscale)*0.75f)*255);
			var green = (byte) ((transform.localScale.x/Maxscale)*255);

			material.SetColor("_EmissionColor", new Color32(red, green, 0, 255));
		}
		else
		{
			transform.localScale = new Vector3((0 / MaxHealth) * Maxscale, 1.0f, 1.0f);
			transform.localPosition = new Vector3((transform.localScale.x / 2), 0, 0);
		}
	}
}
