using Firebase.Extensions;
using Firebase.Firestore;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class EmergencyInitialize : MonoBehaviour
{
    [SerializeField] private GameObject _emergencyPointPrefab;
    [SerializeField] private GameObject _emergencyFrame;
    private void OnEnable()
    {
        float x, y, z;
        var documentReference = FirebaseFirestore.DefaultInstance.Collection("ESCoordinates").Document("Coordinates");
        documentReference.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot documentSnapshot = task.Result;
            if (documentSnapshot.Exists)
            {
                var data = documentSnapshot.ConvertTo<EmergencyCoordinatesData>();
                x = data.X; 
                y = data.Y; 
                z = data.Z;
                if (x != 0 && y != 0 && z != 0)
                    InitializeEmergency(x,y,z);
                else
                    _emergencyFrame.SetActive(false);
            }
        });
        
    }
    private void InitializeEmergency(float x, float y, float z)
    {
        Instantiate(_emergencyPointPrefab, new Vector3(x, y, z), Quaternion.identity);
        _emergencyFrame.SetActive(true);
    }
}
