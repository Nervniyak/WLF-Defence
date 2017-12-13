using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmoothlyDissapear : MonoBehaviour {

    public float Duration;
    public float SecondsToWait;
    private Color _textStartColor, _textEndColor;
    private Text _text;

    void Start()
    {
        _text = GetComponent<Text>();
        _textStartColor = _text.color;
        _textEndColor = new Color(_textStartColor.r, _textStartColor.g, _textStartColor.b, 0f);
        StartCoroutine(Dissapear());
    }

    IEnumerator Dissapear()
    {
        yield return new WaitForSeconds(SecondsToWait);

        float elapsedTime = 0;

        while (elapsedTime < Duration)
        {
            float t = elapsedTime / Duration;
            _text.color = Color.Lerp(_textStartColor, _textEndColor, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
