using UnityEngine;
using UnityEngine.UI;

public class EmergencyButton : MonoBehaviour
{
    [SerializeField] private GameObject _controller;
    [SerializeField] private Image _emergencyButton;
    private Animator _animController;
    private void OnEnable()
    {

        _animController = GetComponent<Animator>();
    }
    public void OnEmergencyButtonClick()
    {
        if (_controller.GetComponent<Controller>().enabled == true)
        {
            _animController.enabled = true;
            _controller.GetComponent<Controller>()._targetPosition = Vector3.zero;
            Destroy(_controller.GetComponent<Controller>()._startPosition);
            _controller.GetComponent<Controller>().enabled = false;
            _controller.GetComponent<EmergencyController>().enabled = true;

        }
        else
        {
            _animController.enabled = false;
            _emergencyButton.color = Color.white;
            _controller.GetComponent<Controller>().enabled = true;
            _controller.GetComponent<EmergencyController>().enabled = false;
        }
    }
}
