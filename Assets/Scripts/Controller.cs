using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Controller : MonoBehaviour
{
    [SerializeField] private float _zoomMin = 5f;
    [SerializeField] private float _zoomMax = 20f;
    [SerializeField] private float _zoomSpeed = 1f;
    [SerializeField] private GameObject _startPositionPrefab;
    [SerializeField] private GameObject _roleCanvas;
    protected Plane _plane;

    private Camera _camera;
    private Vector2 tpos1;
    private Vector3 _qrStartPosition;
    private List<Transform> _ESExitsCollection = new List<Transform>();

    public GameObject _startPosition;
    public Vector3 _targetPosition = Vector3.zero;


    private void Start()
    {
        _camera = Camera.main;
        if (DataManager.LoadPositionY() != -3)
        {
            _qrStartPosition = new Vector3(DataManager.LoadPositionX(), DataManager.LoadPositionY(), DataManager.LoadPositionZ());
            _startPosition = Instantiate(_startPositionPrefab, _qrStartPosition, Quaternion.identity);
            _startPosition.GetComponent<PathDrawer>().Initialize();
        }
       
    }
    private IEnumerator StartInitialize()
    {
        yield return new WaitForSeconds(2f);
        if (GameObject.Find("DangerousZone(Clone)") != null)
        {
            
        }
    }

    private void Update()
    {
        if (Input.touchCount >= 1)
            _plane.SetNormalAndPosition(transform.up, transform.position);

        var Delta1 = Vector3.zero;
        var Delta2 = Vector3.zero;

        //Перемещение камеры
        if (Input.touchCount >= 1)
        {
            Delta1 = PlanePositionDelta(Input.GetTouch(0));
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
                _camera.transform.Translate(Delta1, Space.World);
        }

        //Зум
        if (Input.touchCount >= 2)
        {
            // Получение позиций касания на экране
            Vector2 touch0Pos = Input.GetTouch(0).position;
            Vector2 touch1Pos = Input.GetTouch(1).position;

            // Рассчет текущего и предыдущего расстояния между касаниями
            float distance = Vector2.Distance(touch0Pos, touch1Pos);
            float prevDistance = Vector2.Distance(touch0Pos - Input.GetTouch(0).deltaPosition,
                                                  touch1Pos - Input.GetTouch(1).deltaPosition);

            // Рассчет изменения зума
            float deltaDistance = distance - prevDistance;

            // Изменение зума камеры
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
                    if (GameObject.Find("DangerousZone(Clone)")!=null)
                    {
                        _ESExitsCollection.Clear();
                        GameObject[] pointObjects = GameObject.FindGameObjectsWithTag("EmergencyExit");
                        foreach (GameObject pointObject in pointObjects)
                        {
                            Transform pointTransform = pointObject.transform;
                            _ESExitsCollection.Add(pointTransform);
                        }
                        if (_startPosition == null)
                        {
                            _startPosition = Instantiate(_startPositionPrefab, new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z), Quaternion.identity);
                            _startPosition.GetComponent<PathDrawer>().Initialize();
                        }
                        else
                        {
                            Destroy(_startPosition);
                            _startPosition = Instantiate(_startPositionPrefab, new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z), Quaternion.identity);
                            _startPosition.GetComponent<PathDrawer>().Initialize();
                        }
                        Transform nearestPoint = null;
                        float closestTargetDistance = float.MaxValue;
                        NavMeshPath path = null;
                        NavMeshPath shortestPath = null;
                        foreach (Transform exit in _ESExitsCollection)
                        {
                            path = new NavMeshPath();
                            if (NavMesh.CalculatePath(_startPosition.transform.position, exit.transform.position, _startPosition.GetComponent<NavMeshAgent>().areaMask, path))
                            {
                                float distance = Vector3.Distance(_startPosition.transform.position, exit.transform.position);
                                for (int i = 1; i < path.corners.Length; i++)
                                    distance += Vector3.Distance(path.corners[i - 1], path.corners[i]);
                                if (distance<closestTargetDistance)
                                {
                                    closestTargetDistance = distance;
                                    shortestPath = path;
                                    nearestPoint = exit;
                                }
                            }
                        }
                        _startPosition.GetComponent<PathDrawer>().SetPoint(nearestPoint.transform.position);

                    }
                    else
                    {
                        if (_startPosition == null)
                        {
                            _startPosition = Instantiate(_startPositionPrefab, new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z), Quaternion.identity);
                            _startPosition.GetComponent<PathDrawer>().Initialize();
                        }
                        else if (_targetPosition == Vector3.zero)
                        {
                            _targetPosition = hit.point;
                            _startPosition.GetComponent<PathDrawer>().SetPoint(_targetPosition);
                        }
                        else if (_targetPosition != Vector3.zero)
                        {
                            Destroy(_startPosition);
                            _startPosition = Instantiate(_startPositionPrefab, new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z), Quaternion.identity);
                            _startPosition.GetComponent<PathDrawer>().Initialize();
                            _startPosition.GetComponent<PathDrawer>().SetPoint(_targetPosition);
                        }
                    }
                }
            }
    }
    IEnumerator WaitUntilInstantiate()
    {
        yield return new WaitForSeconds(0.2f);
    }

    protected Vector3 PlanePositionDelta(Touch touch)
    {
        //нет движения
        if (touch.phase != TouchPhase.Moved)
            return Vector3.zero;

        //delta
        var rayBefore = _camera.ScreenPointToRay(touch.position - touch.deltaPosition);
        var rayNow = _camera.ScreenPointToRay(touch.position);
        if (_plane.Raycast(rayBefore, out var enterBefore) && _plane.Raycast(rayNow, out var enterNow))
            return rayBefore.GetPoint(enterBefore) - rayNow.GetPoint(enterNow);

        //объект не на этаже
        return Vector3.zero;
    }

    protected Vector3 PlanePosition(Vector2 screenPos)
    {
        //позиция
        var rayNow = _camera.ScreenPointToRay(screenPos);
        if (_plane.Raycast(rayNow, out var enterNow))
            return rayNow.GetPoint(enterNow);

        return Vector3.zero;
    }

}
