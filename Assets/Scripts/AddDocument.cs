using Firebase.Extensions;
using Firebase.Firestore;
using TMPro;
using UnityEngine;

public class AddDocument : MonoBehaviour
{
    [SerializeField] private TMP_InputField _famInput;
    [SerializeField] private TMP_InputField _nameInput;
    [SerializeField] private TMP_InputField _otchInput;
    [SerializeField] private TMP_InputField _roleInput;
    [SerializeField] private TMP_InputField _loginInput;
    [SerializeField] private TMP_InputField _passwordInput;
    public void OnEmployeeAddDocumentButtonClicked()
    {
        EmployeeData newData = new EmployeeData
        {
            Fam = _famInput.text,
            Name = _nameInput.text,
            Otch = _otchInput.text,
            Role = _roleInput.text,
            Login = _loginInput.text,
            Password = _passwordInput.text
        };
        EmployeeAddDocumentToFirestore(newData);
    }

    private void EmployeeAddDocumentToFirestore(EmployeeData newData)
    {
        var collectionReference = FirebaseFirestore.DefaultInstance.Collection("Employees");

        string documentId = "fam"; // Замените "my_custom_id" на ваш собственный идентификатор

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
    }
}
