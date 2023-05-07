using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(LineRenderer))]
public class PathDrawer : MonoBehaviour
{

    [SerializeField] private GameObject _clickMarkerPrefab;

    private Transform _visualObjectsParent;
    private GameObject _clickMarker;
    private NavMeshAgent _navMeshAgent;
    private LineRenderer _lineRenderer;

    private void Start()
    {
        _clickMarker = Instantiate(_clickMarkerPrefab);
        _visualObjectsParent = GameObject.Find("VisualObjectsParent").GetComponent<Transform>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.startWidth = 0.3f;
        _lineRenderer.endWidth = 0.3f;
        _lineRenderer.positionCount = 0;
    }


    public void SetPoint(Vector3 touchPosition)
    {
        _clickMarker.SetActive(true);
        _clickMarker.transform.SetParent(_visualObjectsParent);
        _clickMarker.transform.position = new Vector3(touchPosition.x, touchPosition.y + 0.1f, touchPosition.z);
        _navMeshAgent.SetDestination(touchPosition);
        StartCoroutine(DrawPath());
    }
    private IEnumerator DrawPath()
    {
        _navMeshAgent.CalculatePath(_navMeshAgent.destination, _navMeshAgent.path);

        yield return new WaitUntil(() => _navMeshAgent.path.status == NavMeshPathStatus.PathComplete);
        yield return new WaitForSeconds(0.1f);
        _lineRenderer.positionCount = _navMeshAgent.path.corners.Length;
        _lineRenderer.SetPosition(0, transform.position);

        for (int i = 0; i < _navMeshAgent.path.corners.Length; i++)
        {
            Vector3 pointPosition = new Vector3(_navMeshAgent.path.corners[i].x, _navMeshAgent.path.corners[i].y + 0.3f, _navMeshAgent.path.corners[i].z);
            _lineRenderer.SetPosition(i, pointPosition);
        }
    }
    private void OnDestroy()
    {
        Destroy(_clickMarker);
    }
}
