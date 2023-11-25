using TMPro;
using UnityEngine;

public class RoleManipulationChooser : MonoBehaviour
{
    [SerializeField] private GameObject _employeePanel;
    [SerializeField] private GameObject _studentPanel;
    [SerializeField] private GameObject _objectPanel;
    [SerializeField] private TextMeshProUGUI _role;

    public void OnEmployeeClick()
    {
        _employeePanel.SetActive(true);
        _studentPanel.SetActive(false);
        _objectPanel.SetActive(false);
        _role.text = "���������";
    }
    public void OnStudentClick()
    {
        _employeePanel.SetActive(false);
        _studentPanel.SetActive(true);
        _objectPanel.SetActive(false);
        _role.text = "�������";
    }
    public void OnSecurityClick()
    {
        _employeePanel.SetActive(false);
        _studentPanel.SetActive(false);
        _objectPanel.SetActive(false);
        _role.text = "��������� �� ������������";
    }
    public void OnObjectClick()
    {
        _employeePanel.SetActive(false);
        _studentPanel.SetActive(false);
        _objectPanel.SetActive(true);
        _role.text = "������";
    }

}
