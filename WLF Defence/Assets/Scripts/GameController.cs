using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Transform PauseMenu;
    public Transform Player;
    public Transform DeathScreen;
    public Transform GUIControls;

    public Text WinLoseText;
    public Text RemainingText;

    public bool GoBackToMenuAfterCompletion = false;


    private float _timeBefore;
    private bool _endBegan;

    void Update()
    {
        if (Input.GetButtonDown("Escape"))
        {
            Pause();
        }
        if (_endBegan)
        {
            var remaining = GameObject.FindGameObjectsWithTag("NPC");
            RemainingText.text = string.Format(" {0} left.", remaining.Length);
            if (remaining.Length == 0)
            {
                RemainingText.text = " ";
                StartCoroutine(Victory());
            }
        }
    }

    public void Pause()
    {

        if (PauseMenu.gameObject.activeInHierarchy == false)
        {
            PauseMenu.gameObject.SetActive(true);
            GUIControls.gameObject.SetActive(false);
            _timeBefore = Time.timeScale;
            Time.timeScale = 0;
            Player.GetComponent<PlayerController>().enabled = false;
        }
        else
        {
            PauseMenu.gameObject.SetActive(false);
            GUIControls.gameObject.SetActive(true);
            Time.timeScale = _timeBefore;
            Player.GetComponent<PlayerController>().enabled = true;
        }

    }

    void StopSpawn()
    {
        _endBegan = true;
        var spawners = GameObject.FindGameObjectsWithTag("Spawner");
        for (var i = 0; i < spawners.Length; i++)
        {
            var spawn = spawners[i].GetComponent<Spawn>();
            spawn.MaximumSpawnLimit = 0;
            spawn.SpawnLimit = 0;
        }
    }

    IEnumerator GameOver()
    {
        DeathScreen.gameObject.SetActive(true);

        Player.GetComponent<Health>().enabled = false;
        Player.GetComponent<Animator>().SetBool("Dead", true);
        Player.GetComponent<Animator>().SetBool("Grounded", true);
        Player.GetComponent<PlayerController>().enabled = false;
        WinLoseText.text = "Slain!";
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    IEnumerator Victory()
    {

        WinLoseText.text = "You Win!";
        yield return new WaitForSeconds(6);
        if (GoBackToMenuAfterCompletion)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }

    public void Quit()
    {
        Time.timeScale = _timeBefore;
        SceneManager.LoadScene("MainMenu");
    }
}
