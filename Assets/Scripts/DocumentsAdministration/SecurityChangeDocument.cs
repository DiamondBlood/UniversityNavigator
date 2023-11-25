using Firebase.Extensions;
using Firebase.Firestore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
public class SecurityChangeDocument : MonoBehaviour
{
    [SerializeField] private GameObject _dropdownList;
    [SerializeField] private TMP_InputField _famInput;
    [SerializeField] private TMP_InputField _nameInput;
    [SerializeField] private TMP_InputField _otchInput;
    [SerializeField] private TMP_InputField _loginInput;
    [SerializeField] private TMP_InputField _passwordInput;

    private async void OnEnable()
    {
        List<DocumentSnapshot> documentList = await GetAllDocumentsFromCollection();
        PopulateDropdown(documentList);

    }

    private async Task<List<DocumentSnapshot>> GetAllDocumentsFromCollection()
    {
        var collectionReference = FirebaseFirestore.DefaultInstance.Collection("Security");
        var querySnapshot = await collectionReference.GetSnapshotAsync();
        return querySnapshot.Documents.ToList();
    }
    private void PopulateDropdown(List<DocumentSnapshot> documents)
    {
        TMP_Dropdown dropdown = _dropdownList.GetComponent<TMP_Dropdown>();
        dropdown.ClearOptions();

        List<string> options = new List<string>();
        foreach (var document in documents)
        {
            options.Add(document.Id);
        }

        dropdown.AddOptions(options);
    }
    public void OnSearchChange()
    {
        TMP_Dropdown dropdown = _dropdownList.GetComponent<TMP_Dropdown>();
        if (dropdown.options.Count == 1)
        {
            string selectedDocumentId = dropdown.options[dropdown.value].text;
            FillInputFields(selectedDocumentId);
        }
    }
    public void OnDropdownValueChanged()
    {
        TMP_Dropdown dropdown = _dropdownList.GetComponent<TMP_Dropdown>();
        string selectedDocumentId = dropdown.options[dropdown.value].text;

        FillInputFields(selectedDocumentId);
    }

    private void FillInputFields(string documentId)
    {
        var documentReference = FirebaseFirestore.DefaultInstance.Collection("Security").Document(documentId);
        documentReference.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot documentSnapshot = task.Result;
            if (documentSnapshot.Exists)
            {
                var data = documentSnapshot.ConvertTo<SecurityData>();
                _famInput.text = data.Fam;
                _nameInput.text = data.Name;
                _otchInput.text = data.Otch;
                _loginInput.text = data.Login;
                _passwordInput.text = data.Password;
            }
        });
    }

    public void UpdateDocument()
    {
        TMP_Dropdown dropdown = _dropdownList.GetComponent<TMP_Dropdown>();
        string selectedDocumentId = dropdown.options[dropdown.value].text;

        string collectionName = "Security";
        string documentId = _famInput.text + _nameInput.text + _otchInput.text;

        var documentReference = FirebaseFirestore.DefaultInstance.Collection(collectionName).Document(documentId);


        SecurityData newData = new SecurityData
        {
            Fam = _famInput.text,
            Name = _nameInput.text,
            Otch = _otchInput.text,
            Login = _loginInput.text,
            Password = _passwordInput.text
        };

        if (selectedDocumentId != documentId)
        {
            var documentReferenceForDelete = FirebaseFirestore.DefaultInstance.Collection(collectionName).Document(selectedDocumentId);
            documentReferenceForDelete.DeleteAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("Document deleted successfully!");
                }
                else
                {
                    Debug.LogError("Failed to delete document: " + task.Exception);
                }
            });
        }

        documentReference.SetAsync(newData).ContinueWithOnMainThread(task =>
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
        UpdateList();
        _famInput.text = "";
        _nameInput.text = "";
        _otchInput.text = "";
        _loginInput.text = "";
        _passwordInput.text = "";
    }

    private async void UpdateList()
    {
        List<DocumentSnapshot> documentList = await GetAllDocumentsFromCollection();
        PopulateDropdown(documentList);
    }
}
