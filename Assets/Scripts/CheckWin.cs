using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckWin : MonoBehaviour
{
    public void PlayAgain()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void Quit()
    {
        SceneManager.LoadSceneAsync(0);
    }

}
