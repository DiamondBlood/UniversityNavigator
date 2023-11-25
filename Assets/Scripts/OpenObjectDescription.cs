using UnityEngine;

public class OpenObjectDescription : MonoBehaviour
{
    [SerializeField] private GameObject _objectDescription;
    [SerializeField] private string _objectName;
    [SerializeField] private ObjectDescriptionLoader _dataLoader;
    [SerializeField] private Sprite _imgSprite;
    public void OnOpenObjectClick()
    {
        _dataLoader.SetDocumentPath(_objectName);
        _dataLoader.SetImage(_imgSprite);
        _objectDescription.SetActive(true);
    }
}
