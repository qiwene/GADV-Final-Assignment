using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuPlayButton : MonoBehaviour
{
    public void GoToScene (string sceneName)
    {
        SceneManager.LoadScene (sceneName);
    }

    public void QuitApp ()
    {
        Application.Quit ();
        Debug.Log("You have quit the game");
    }
}