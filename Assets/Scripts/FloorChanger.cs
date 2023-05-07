using UnityEngine;

public class FloorChanger : MonoBehaviour
{
    [SerializeField] private float _cameraPositionY;

    public void ChangeFloor()
    {
        var cameraPos = Camera.main.transform;
        Camera.main.transform.position = new Vector3(cameraPos.position.x, _cameraPositionY, cameraPos.position.z);
    }
}
