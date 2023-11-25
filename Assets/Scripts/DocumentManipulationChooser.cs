using UnityEngine;

public class DocumentManipulationChooser : MonoBehaviour
{
    [SerializeField] private GameObject _addPanel;
    [SerializeField] private GameObject _changePanel;
    [SerializeField] private GameObject _deletePanel;

    public void OnAddClick()
    {
        _addPanel.SetActive(true);
        _changePanel.SetActive(false);
        _deletePanel.SetActive(false);
    }
    public void OnChangeClick()
    {
        _addPanel.SetActive(false);
        _changePanel.SetActive(true);
        _deletePanel.SetActive(false);
    }
    public void OnDeleteClick()
    {
        _addPanel.SetActive(false);
        _changePanel.SetActive(false);
        _deletePanel.SetActive(true);
    }
}
