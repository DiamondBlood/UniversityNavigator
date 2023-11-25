using UnityEngine;

public class RoleChooser : MonoBehaviour
{
    [SerializeField] private AuthorizationButton _authButton;
    [SerializeField] private GameObject _map;
    [SerializeField] private GameObject _mainCanvas;
    [SerializeField] private GameObject _authCanvas;
    [SerializeField] private GameObject _emergencyNotificate;

    public void OnAdminClick()
    {
        _authButton.SetDatabaseCollection("Administrator");
        _authCanvas.SetActive(true);
        gameObject.SetActive(false);
    }
    public void OnSecurityClick()
    {
        _authButton.SetDatabaseCollection("Security");
        _authCanvas.SetActive(true);
        gameObject.SetActive(false);
    }
    public void OnStudentClick()
    {
        _authButton.SetDatabaseCollection("Students");
        _authCanvas.SetActive(true);
        gameObject.SetActive(false);
    }
    public void OnEmployeeClick()
    {
        _authButton.SetDatabaseCollection("Employees");
        _authCanvas.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnGuestClick()
    {
        gameObject.SetActive(false);
        _map.SetActive(true);
        _mainCanvas.SetActive(true);
        _authCanvas.SetActive(false);
        _emergencyNotificate.SetActive(false);
    }

}
