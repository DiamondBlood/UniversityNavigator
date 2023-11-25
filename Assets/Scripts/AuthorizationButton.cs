using Firebase.Firestore;
using System.Collections;
using TMPro;
using UnityEngine;

public class AuthorizationButton : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private GameObject _mainCanvas;
    [SerializeField] private GameObject _adminCanvas;
    [SerializeField] private TMP_InputField _login;
    [SerializeField] private TMP_InputField _password;
    [SerializeField] private GameObject _map;
    [SerializeField] private GameObject _emergencyNotificate;
    [SerializeField] private GameObject _roleChoser;
    [SerializeField] private GameObject _authCanvas;

    private string _colleciton;

    private void Start()
    {
        StartCoroutine(AutoAuthorization());
    }
    private IEnumerator AutoAuthorization()
    {

        yield return new WaitForSeconds(1f);
        _colleciton = DataManager.LoadCredoRole();
        if (_colleciton != "")
        {
            var collectionReference = FirebaseFirestore.DefaultInstance.Collection(_colleciton);

            var getSnapshotTask = collectionReference.GetSnapshotAsync();
            yield return new WaitUntil(() => getSnapshotTask.IsCompleted);

            var querySnapshot = getSnapshotTask.Result;
            switch (_colleciton)
            {
                case "Administrator":
                    foreach (var documentSnapshot in querySnapshot.Documents)
                    {
                        var data = documentSnapshot.ConvertTo<AdminData>();
                        var admin = new AdminData { Login = data.Login, Password = data.Password };
                        if (DataManager.LoadCredoLogin() == admin.Login && DataManager.LoadCredoPassword() == admin.Password)
                        {
                            _container.SetActive(false);
                            _adminCanvas.SetActive(true);
                            _emergencyNotificate.SetActive(false);
                            _roleChoser.SetActive(false);
                            _authCanvas.SetActive(false);
                        }
                    }
                    break;
                case "Security":
                    foreach (var documentSnapshot in querySnapshot.Documents)
                    {
                        var data = documentSnapshot.ConvertTo<SecurityData>();
                        var security = new SecurityData { Login = data.Login, Password = data.Password };
                        if (DataManager.LoadCredoLogin() == security.Login && DataManager.LoadCredoPassword() == security.Password)
                        {
                            _container.SetActive(false);
                            _map.SetActive(true);
                            _mainCanvas.SetActive(true);
                            _emergencyNotificate.SetActive(true);
                            _roleChoser.SetActive(false);
                            _authCanvas.SetActive(false);
                        }
                    }
                    break;
                case "Students":
                    foreach (var documentSnapshot in querySnapshot.Documents)
                    {
                        var data = documentSnapshot.ConvertTo<StudentData>();
                        if (DataManager.LoadCredoLogin() == data.Login && DataManager.LoadCredoPassword() == data.Password)
                        {
                            _container.SetActive(false);
                            _map.SetActive(true);
                            _mainCanvas.SetActive(true);
                            _emergencyNotificate.SetActive(false);
                            _roleChoser.SetActive(false);
                            _authCanvas.SetActive(false);
                        }
                    }
                    break;
                case "Employees":
                    foreach (var documentSnapshot in querySnapshot.Documents)
                    {
                        var data = documentSnapshot.ConvertTo<EmployeeData>();
                        if (DataManager.LoadCredoLogin() == data.Login && DataManager.LoadCredoPassword() == data.Password)
                        {
                            _container.SetActive(false);
                            _mainCanvas.SetActive(true);
                            _emergencyNotificate.SetActive(false);
                            _map.SetActive(true);
                            _roleChoser.SetActive(false);
                            _authCanvas.SetActive(false);
                        }
                    }
                    break;
                default: break;
            }
        }
        
    }
    public void SetDatabaseCollection(string value) => _colleciton = value;
    public void OnCheckButtonClick()
    {
        StartCoroutine(CheckDocumentsInCollection());
    }

    private IEnumerator CheckDocumentsInCollection()
    {
        var collectionReference = FirebaseFirestore.DefaultInstance.Collection(_colleciton);

        var getSnapshotTask = collectionReference.GetSnapshotAsync();
        yield return new WaitUntil(() => getSnapshotTask.IsCompleted);

        var querySnapshot = getSnapshotTask.Result;
        switch (_colleciton)
        {
            case "Administrator":
                foreach (var documentSnapshot in querySnapshot.Documents)
                {
                   
                    var data = documentSnapshot.ConvertTo<AdminData>();
                    var admin = new AdminData { Login = data.Login, Password = data.Password };
                    if (_login.text == admin.Login && _password.text == admin.Password)
                    {
                        SaveCredo("Administrator", _login.text, _password.text);
                        _container.SetActive(false);
                        _adminCanvas.SetActive(true);
                        _emergencyNotificate.SetActive(false);
                    }
                }
                break;
            case "Security":
                foreach (var documentSnapshot in querySnapshot.Documents)
                {
                    var data = documentSnapshot.ConvertTo<SecurityData>();
                    var security = new SecurityData { Login = data.Login, Password = data.Password };
                    if (_login.text == security.Login && _password.text == security.Password)
                    {
                        SaveCredo("Security", _login.text, _password.text);
                        _container.SetActive(false);
                        _map.SetActive(true);
                        _mainCanvas.SetActive(true);
                        _emergencyNotificate.SetActive(true);
                    }
                }
                break;
            case "Students":
                foreach (var documentSnapshot in querySnapshot.Documents)
                {
                    var data = documentSnapshot.ConvertTo<StudentData>();
                    if (_login.text == data.Login && _password.text == data.Password)
                    {
                        SaveCredo("Students", _login.text, _password.text);
                        _container.SetActive(false);
                        _map.SetActive(true);
                        _mainCanvas.SetActive(true);
                        _emergencyNotificate.SetActive(false);
                    }
                }
                break;
            case "Employees":
                foreach (var documentSnapshot in querySnapshot.Documents)
                {
                    var data = documentSnapshot.ConvertTo<EmployeeData>();
                    if (_login.text == data.Login && _password.text == data.Password)
                    {
                        SaveCredo("Employees", _login.text, _password.text);
                        _container.SetActive(false);
                        _mainCanvas.SetActive(true);
                        _emergencyNotificate.SetActive(false);
                        _map.SetActive(true);
                    }
                }
                break;
            default: break;
        }
    }
    private void SaveCredo(string role, string log, string pass)
    {
        DataManager.SaveCredo(role, log, pass);
    }
}
