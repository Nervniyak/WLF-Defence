using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuLoader : MonoBehaviour {

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
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
