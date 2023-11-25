using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine;

public class ClearPath : MonoBehaviour
{
    [SerializeField] private Controller _controller;
    [SerializeField] private GameObject _emegencyFrame;
    public void Clear()
    {
        if (_controller.enabled)
        {
            _controller._targetPosition = Vector3.zero;
            Destroy(_controller._startPosition);
        }
        else
        {
            Destroy(GameObject.Find("DangerousZone(Clone)"));
            EmergencyCoordinatesData ESCoordinates = new EmergencyCoordinatesData
            {
                X = 0,
                Y = 0,
                Z = 0
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
            _emegencyFrame.SetActive(false);
        }
        

    }
}
