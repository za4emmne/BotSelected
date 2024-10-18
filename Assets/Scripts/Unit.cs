using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Unit : MonoBehaviour
{
    public bool IsBusy => _isBusy;

    private float _speed = 3f;
    [SerializeField] private bool _isBusy;
    private Transform _target;
    private Transform _base;

    private void Awake()
    {
        _base = transform;
    }

    private void Start()
    {

        Debug.Log(_base.position);
        _isBusy = false;
    }

    private void Update()
    {
        MoveToTarget(_target);
    }

    public void ChangeStatus()
    {
        _isBusy = !_isBusy;
    }

    public void TakeResoursePosition(Transform resourse)
    {
        _target = resourse;
    }

    private void MoveToTarget(Transform target)
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                        target.position, _speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resourse resourse))
        {
            _target = _base;
            Debug.Log(_base.position);
            Debug.Log(_target.position);
        }
    }
}
