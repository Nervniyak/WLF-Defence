using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ExpGain : MonoBehaviour
{
    public int CurrentLevel;
    public int NextLevel;
    public float XP { get; set; }

    public Text DisplayText;
    public PlayerController PlayerController { get; set; }

    void Start()
    {
        PlayerController = GetComponent<PlayerController>();
        CurrentLevel = 1;
        XP = 0;
        NextLevel = 100;
    }
    void Update()
    {
        XP += Time.deltaTime;
        while(XP >= NextLevel)
        {
            CurrentLevel++;
            XP -= NextLevel;
            NextLevel += (int)(NextLevel * 0.3f);
            StartCoroutine("LevelUpText");
            PlayerController.LevelUp();
        }
    }

    void ReceiveXP(int xpReceived)
    {
        XP += xpReceived;
        if (DisplayText != null)
        {
            StartCoroutine("XPGainedText", xpReceived);
        }
    }

    IEnumerator XPGainedText(int xpReceived)
    {
        DisplayText.enabled = true;
        DisplayText.text = string.Format("+ {0} Exp", xpReceived);
        yield return new WaitForSeconds(1);
        DisplayText.enabled = false;
    }

    IEnumerator LevelUpText()
    {
        yield return new WaitForSeconds(1);
        DisplayText.enabled = true;
        DisplayText.text = " Level UP!";
        yield return new WaitForSeconds(1);
        DisplayText.enabled = false;
    }
}
