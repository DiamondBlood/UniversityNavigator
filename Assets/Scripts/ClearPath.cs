using UnityEngine;

public class ClearPath : MonoBehaviour
{
    [SerializeField] private Controller _controller;
    public void Clear()
    {
        Destroy(_controller._startPosition);
    }
}
