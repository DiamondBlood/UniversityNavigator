using UnityEngine;
using UnityEngine.SceneManagement;

public class QRCodeScannerSceneChange : MonoBehaviour
{
    public void OnScanClick()
    {
        DataManager.Delete();
        SceneManager.LoadScene("QRCodeScannerScene");
    }
    
}
