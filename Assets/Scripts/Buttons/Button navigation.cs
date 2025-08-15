using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttonnavigation : MonoBehaviour
{

    // input sceneName to choose which scene in Asset to go to
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