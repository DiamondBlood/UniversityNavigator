using Firebase.Extensions;
using Firebase.Firestore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class DeleteDocument : MonoBehaviour
{
    [SerializeField] private GameObject _dropdownList;
    [SerializeField] private TMP_InputField _famInput;
    [SerializeField] private TMP_InputField _nameInput;
    [SerializeField] private TMP_InputField _otchInput;
    [SerializeField] private string _collectionName;
    private async void OnEnable()
    {
        List<DocumentSnapshot> documentList = await GetAllDocumentsFromCollection();
        PopulateDropdown(documentList);
    }

    private async Task<List<DocumentSnapshot>> GetAllDocumentsFromCollection()
    {
        var collectionReference = FirebaseFirestore.DefaultInstance.Collection(_collectionName);
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
        var documentReference = FirebaseFirestore.DefaultInstance.Collection(_collectionName).Document(documentId);

        documentReference.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot documentSnapshot = task.Result;
            if (documentSnapshot.Exists)
            {

                switch (_collectionName)
                {
                    case "Employees": EmployeeData data1 = documentSnapshot.ConvertTo<EmployeeData>();
                        _famInput.text = data1.Fam;
                        _nameInput.text = data1.Name;
                        _otchInput.text = data1.Otch; break;
                    case "Students": StudentData data2 = documentSnapshot.ConvertTo<StudentData>();
                        _famInput.text = data2.Fam;
                        _nameInput.text = data2.Name;
                        _otchInput.text = data2.Otch; break;
                    case "Security": SecurityData data3 = documentSnapshot.ConvertTo<SecurityData>();
                        _famInput.text = data3.Fam;
                        _nameInput.text = data3.Name;
                        _otchInput.text = data3.Otch; break;
                    default:
                        break;
                }
                

            }
        });
    }

    public void DeleteDocuments()
    {
        TMP_Dropdown dropdown = _dropdownList.GetComponent<TMP_Dropdown>();

        string documentId = _famInput.text + _nameInput.text + _otchInput.text;

        var documentReference = FirebaseFirestore.DefaultInstance.Collection(_collectionName).Document(documentId);

        documentReference.DeleteAsync().ContinueWithOnMainThread(task =>
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
        UpdateList();
        _famInput.text = "";
        _nameInput.text = "";
        _otchInput.text = "";
    }

    private async void UpdateList()
    {
        List<DocumentSnapshot> documentList = await GetAllDocumentsFromCollection();
        PopulateDropdown(documentList);
    }
}
