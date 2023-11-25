using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine.UI;

public class EmergencyConfirm : MonoBehaviour
{
    [SerializeField] private GameObject _confirmWindow;
    [SerializeField] private GameObject _controller;
    [SerializeField] private GameObject _emergencyFrame;
    [SerializeField] private Animator _animController;
    [SerializeField] private Image _emergencyButton;
    public void OnConfirmClick()
    {
        Transform point = GameObject.Find("DangerousZone(Clone)").GetComponent<Transform>();
        EmergencyCoordinatesData ESCoordinates = new EmergencyCoordinatesData
        {
            X = point.position.x,
            Y = point.position.y,
            Z = point.position.z
        };
        var documentReference = FirebaseFirestore.DefaultInstance.Collection("ESCoordinates").Document("Coordinates");
        documentReference.SetAsync(ESCoordinates).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Документ успешно обновлен.");
            }
            else if (task.IsFaulted)
            {
                Debug.LogError("Ошибка при обновлении документа: " + task.Exception);
            }
        });
        _controller.GetComponent<Controller>().enabled = true;
        _controller.GetComponent<EmergencyController>().enabled = false;
        _confirmWindow.SetActive(false);
        _emergencyFrame.SetActive(true);
        _animController.enabled = false;
        _emergencyButton.color = Color.white;
    }

    public void OnCancellClick()
    {
        Destroy(GameObject.Find("DangerousZone(Clone)"));
        _confirmWindow.SetActive(false);
    }
}
