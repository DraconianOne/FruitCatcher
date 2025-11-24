using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{

    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnExitButtonClick()
    {
        Application.Quit();
    }
    
}
