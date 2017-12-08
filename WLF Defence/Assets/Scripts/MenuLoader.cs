using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuLoader : MonoBehaviour {

    public void Play()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Training()
    {
        SceneManager.LoadScene("Training1");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
