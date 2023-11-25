using Firebase.Extensions;
using Firebase.Firestore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;


public class ObjectChangeDocument : MonoBehaviour
{
    [SerializeField] private GameObject _dropdownList;
    [SerializeField] private TMP_InputField _nameInput;
    [SerializeField] private TMP_InputField _typeInput;
    [SerializeField] private TMP_InputField _descriptionInput;
    [SerializeField] private TMP_InputField _workingHoursInput;

    private async void OnEnable()
    {
        List<DocumentSnapshot> documentList = await GetAllDocumentsFromCollection();
        PopulateDropdown(documentList);

    }

    private async Task<List<DocumentSnapshot>> GetAllDocumentsFromCollection()
    {
        var collectionReference = FirebaseFirestore.DefaultInstance.Collection("Objects");
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
        var documentReference = FirebaseFirestore.DefaultInstance.Collection("Objects").Document(documentId);
        documentReference.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot documentSnapshot = task.Result;
            if (documentSnapshot.Exists)
            {
                var data = documentSnapshot.ConvertTo<ObjectData>();
                _nameInput.text = data.Name;
                _typeInput.text = data.Type;
                _descriptionInput.text = data.Description;
                _workingHoursInput.text = data.WorkingHours;
            }
        });
    }

    public void UpdateDocument()
    {
        TMP_Dropdown dropdown = _dropdownList.GetComponent<TMP_Dropdown>();
        string selectedDocumentId = dropdown.options[dropdown.value].text;

        string collectionName = "Objects";

        var documentReference = FirebaseFirestore.DefaultInstance.Collection(collectionName).Document(selectedDocumentId);


        ObjectData newData = new ObjectData
        {
            Name = selectedDocumentId,
            Type = _typeInput.text,
            Description = _descriptionInput.text,
            WorkingHours = _workingHoursInput.text
        };

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
        _nameInput.text = "";
        _typeInput.text = "";
        _descriptionInput.text = "";
        _workingHoursInput.text = "";
    }

    private async void UpdateList()
    {
        List<DocumentSnapshot> documentList = await GetAllDocumentsFromCollection();
        PopulateDropdown(documentList);
    }
}
