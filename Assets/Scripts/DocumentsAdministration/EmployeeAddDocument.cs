using Firebase.Firestore;
using Firebase;
using TMPro;
using UnityEngine;
using Firebase.Extensions;

public class EmployeeAddDocument : MonoBehaviour
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

        string documentId = _famInput.text + _nameInput.text + _otchInput.text;

        var newDocumentReference = collectionReference.Document(documentId);

        // ���������� ������ � ����� ��������
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
        _roleInput.text = "";
        _loginInput.text = "";
        _passwordInput.text = "";
    }
}
