using Firebase.Extensions;
using Firebase.Firestore;
using TMPro;
using UnityEngine;
public class SecurityAddDocument : MonoBehaviour
{
    [SerializeField] private TMP_InputField _famInput;
    [SerializeField] private TMP_InputField _nameInput;
    [SerializeField] private TMP_InputField _otchInput;
    [SerializeField] private TMP_InputField _loginInput;
    [SerializeField] private TMP_InputField _passwordInput;
    public void OnSecurityAddDocumentButtonClicked()
    {
        SecurityData newData = new SecurityData
        {
            Fam = _famInput.text,
            Name = _nameInput.text,
            Otch = _otchInput.text,
            Login = _loginInput.text,
            Password = _passwordInput.text
        };
        SecurityAddDocumentToFirestore(newData);
    }

    private void SecurityAddDocumentToFirestore(SecurityData newData)
    {
        var collectionReference = FirebaseFirestore.DefaultInstance.Collection("Security");

        string documentId = _famInput.text + _nameInput.text + _otchInput.text;

        var newDocumentReference = collectionReference.Document(documentId);

        // Установите данные в новый документ
        newDocumentReference.SetAsync(newData).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Document added successfully!");
            }
            else
            {
                Debug.LogError("Failed to add document: " + task.Exception);
            }
        });
        _famInput.text = "";
        _nameInput.text = "";
        _otchInput.text = "";
        _loginInput.text = "";
        _passwordInput.text = "";
    }
}
