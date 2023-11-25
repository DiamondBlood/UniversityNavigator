using Firebase.Extensions;
using Firebase.Firestore;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectDescriptionLoader : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _type;
    [SerializeField] private TextMeshProUGUI _workingHours;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private GameObject _currentWindow;
    [SerializeField] private Image _typeImage;

    private string _documentPath;

    public void SetImage(Sprite imageSprite) => _typeImage.sprite = imageSprite;
    public void SetDocumentPath(string path) => _documentPath = path;

    public void CloseWindow() => _currentWindow.SetActive(false);

    private void OnEnable()
    {
        var documentReference = FirebaseFirestore.DefaultInstance.Collection("Objects").Document(_documentPath);
        documentReference.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot documentSnapshot = task.Result;
            if (documentSnapshot.Exists)
            {
                var data = documentSnapshot.ConvertTo<ObjectData>();
                _name.text = data.Name;
                _type.text = data.Type;
                _description.text = data.Description;
                _workingHours.text = "Режим работы: " + data.WorkingHours;
            }
        });
    }

}
