using UnityEngine;

public class XPScalingScript : MonoBehaviour
{
	public float Maxscale = 4;
	public TextMesh LevelText;
	public ExpGain ExpGain { get; set; }

	private float _previousFrame_XP;

	void Start ()
	{	
		ExpGain = GameObject.FindWithTag("Player").GetComponent<ExpGain>();
		transform.localScale = new Vector3(Maxscale, 1.0f, 1.0f);
		transform.localPosition = new Vector3((transform.localScale.x /2), 0, 0);
		_previousFrame_XP = ExpGain.XP;
	}
	
	void Update ()
	{
		LevelText.text = ExpGain.CurrentLevel.ToString();
		transform.localScale = new Vector3(_previousFrame_XP / ExpGain.NextLevel * Maxscale, 1.0f, 1.0f);
		transform.localPosition = new Vector3((transform.localScale.x/2), 0, 0);
		_previousFrame_XP = ExpGain.XP;
	}
}
