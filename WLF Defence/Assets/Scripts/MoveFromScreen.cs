using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFromScreen : MonoBehaviour
{

	public int Speed;
	public float Duration;
	public float DistanceX;
	public float DistanceY;
	public float SecondsToWait;

	private RectTransform rectTransform;

	void Start () {
		rectTransform = GetComponent<RectTransform>();
		if (DistanceX == 0)
		{
			DistanceX = rectTransform.anchoredPosition.x;
		}
		if (DistanceY == 0)
		{
			DistanceY = rectTransform.anchoredPosition.y;
		}
		StartCoroutine(MoveAway());
	}

	IEnumerator MoveAway()
	{
		yield return new WaitForSeconds(SecondsToWait);
		while (Duration >= 0)
		{

			Duration -= Time.smoothDeltaTime;

			rectTransform.anchoredPosition = Vector2.Lerp(transform.localPosition, new Vector2(DistanceX, DistanceY), Time.deltaTime * Speed );
			yield return null;
		}
	}
}
