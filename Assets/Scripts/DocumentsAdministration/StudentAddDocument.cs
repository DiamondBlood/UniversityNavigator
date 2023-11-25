using Firebase.Extensions;
using Firebase.Firestore;
using TMPro;
using UnityEngine;

public class StudentAddDocument : MonoBehaviour
{
    [SerializeField] private TMP_InputField _famInput;
    [SerializeField] private TMP_InputField _nameInput;
    [SerializeField] private TMP_InputField _otchInput;
    [SerializeField] private TMP_InputField _loginInput;
    [SerializeField] private TMP_InputField _passwordInput;
    public void OnStudentAddDocumentButtonClicked()
    {
        StudentData newData = new StudentData
        {
            Fam = _famInput.text,
            Name = _nameInput.text,
            Otch = _otchInput.text,
            Login = _loginInput.text,
            Password = _passwordInput.text
        };
        StudentAddDocumentToFirestore(newData);
    }

    private void StudentAddDocumentToFirestore(StudentData newData)
    {
        var collectionReference = FirebaseFirestore.DefaultInstance.Collection("Students");

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
