using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{
    public void OnExitButtonClick()
    {
        SceneManager.LoadScene("MainScene");
    }
}
