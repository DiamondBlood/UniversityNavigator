using UnityEngine;

public class ExitFromFrame : MonoBehaviour
{
    [SerializeField] private GameObject _currentWindow;
    [SerializeField] private GameObject _previousWindow;

    public void OnExitClick()
    {
        _currentWindow.SetActive(false);
        _previousWindow.SetActive(true);
    }
}
