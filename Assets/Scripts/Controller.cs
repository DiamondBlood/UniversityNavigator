using UnityEngine;
using UnityEngine.EventSystems;

public class Controller : MonoBehaviour
{
    [SerializeField] private float _zoomMin = 5f;
    [SerializeField] private float _zoomMax = 20f;
    [SerializeField] private float _zoomSpeed = 1f;
    [SerializeField] private GameObject _startPositionPrefab;

    protected Plane _plane;

    private Camera _camera;
    private Vector2 tpos1;
    private Vector3 _qrStartPosition;
    public GameObject _startPosition;
    public bool Rotate;
    private void Start()
    {
        _camera = Camera.main;
        if (DataManager.LoadPositionY() != -3)
        {
            _qrStartPosition = new Vector3(DataManager.LoadPositionX(), DataManager.LoadPositionY(), DataManager.LoadPositionZ());
            _startPosition = Instantiate(_startPositionPrefab, _qrStartPosition, Quaternion.identity);
        }
    }
    private void Update()
    {
        if (Input.touchCount >= 1)
            _plane.SetNormalAndPosition(transform.up, transform.position);

        var Delta1 = Vector3.zero;
        var Delta2 = Vector3.zero;

        //����������� ������
        if (Input.touchCount >= 1)
        {
            Delta1 = PlanePositionDelta(Input.GetTouch(0));
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
                _camera.transform.Translate(Delta1, Space.World);
        }

        //���
        if (Input.touchCount >= 2)
        {
            // ��������� ������� ������� �� ������
            Vector2 touch0Pos = Input.GetTouch(0).position;
            Vector2 touch1Pos = Input.GetTouch(1).position;

            // ������� �������� � ����������� ���������� ����� ���������
            float distance = Vector2.Distance(touch0Pos, touch1Pos);
            float prevDistance = Vector2.Distance(touch0Pos - Input.GetTouch(0).deltaPosition,
                                                  touch1Pos - Input.GetTouch(1).deltaPosition);

            // ������� ��������� ����
            float deltaDistance = distance - prevDistance;

            // ��������� ���� ������
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - deltaDistance * _zoomSpeed,
                                                        _zoomMin, _zoomMax);


        }

        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            tpos1 = Input.GetTouch(0).position;

        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
            if (Input.GetTouch(0).position == tpos1)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;
                bool hasHit = Physics.Raycast(ray, out hit);
                if (hasHit)
                {
                    if (_startPosition == null)
                        _startPosition = Instantiate(_startPositionPrefab, new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z), Quaternion.identity);
                    else if (_startPosition != null)
                        _startPosition.GetComponent<PathDrawer>().SetPoint(hit.point);
                }
            }
    }

    protected Vector3 PlanePositionDelta(Touch touch)
    {
        //��� ��������
        if (touch.phase != TouchPhase.Moved)
            return Vector3.zero;

        //delta
        var rayBefore = _camera.ScreenPointToRay(touch.position - touch.deltaPosition);
        var rayNow = _camera.ScreenPointToRay(touch.position);
        if (_plane.Raycast(rayBefore, out var enterBefore) && _plane.Raycast(rayNow, out var enterNow))
            return rayBefore.GetPoint(enterBefore) - rayNow.GetPoint(enterNow);

        //������ �� �� �����
        return Vector3.zero;
    }

    protected Vector3 PlanePosition(Vector2 screenPos)
    {
        //�������
        var rayNow = _camera.ScreenPointToRay(screenPos);
        if (_plane.Raycast(rayNow, out var enterNow))
            return rayNow.GetPoint(enterNow);

        return Vector3.zero;
    }

}
