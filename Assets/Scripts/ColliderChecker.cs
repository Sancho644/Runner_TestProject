using UnityEngine;

public class ColliderChecker : MonoBehaviour
{
    [SerializeField] private Transform _sphereTransform;
    [SerializeField] private float _sphereRadius;

    private Collider[] _hits = new Collider[2];
    private int _layerMask;

    public bool IsInLayer { get; private set; }

    private void Awake()
    {
        _layerMask = 1 << LayerMask.NameToLayer("Ground");
    }

    private void Update()
    {
        IsInLayer = Hit() != 0;
    }

    private int Hit() =>
        Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward, _sphereRadius, _hits, _layerMask);

    private Vector3 StartPoint()
    {
        Vector3 position = _sphereTransform.position;
        return new Vector3(position.x, position.y, position.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(StartPoint(), _sphereRadius);
    }
}