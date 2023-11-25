using Firebase;
using Firebase.Firestore;
using UnityEngine;

public class Database : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                // Firebase � ��� ����������� ������ � �������������
                // ������������� Firestore
                FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
            }
            else
            {
                Debug.LogError($"Failed to initialize Firebase: {dependencyStatus}");
            }
        });
    }
}