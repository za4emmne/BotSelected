using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Unit : MonoBehaviour
{
    public bool IsBusy => _isBusy;

    private float _speed = 3f;
    [SerializeField] private bool _isBusy;
    private Resourse _target;
    private Transform _base;
    private Coroutine _coroutine;

    private void Awake()
    {
        _base = transform;
    }

    private void Start()
    {
        _target = null;
        _isBusy = false;
    }

    public void ChangeStatus()
    {
        _isBusy = !_isBusy;
    }

    public void TakeResourse(Resourse resourse)
    {
        _target = resourse;
        transform.LookAt(_target.transform);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(MoveToTarget(_target.transform));
        }
    }

    private IEnumerator MoveToTarget(Transform target)
    {
        while (transform.position != target.transform.position)
        {
            if (target != null)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                            target.transform.position, _speed * Time.deltaTime);
            }

            yield return null;
        }

        //TakeResourse(target);
        _coroutine = null;
    }

    private void TakeResourse(Component other)
    {
        if (other.TryGetComponent(out Resourse resourse))
        {  
            Debug.Log("во что то вошел");
            if (resourse == _target)
            {
                _target.transform.parent = gameObject.transform;
                //ReturnToBase();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        TakeResourse(other);
    }

    private void ReturnToBase()
    {
        if (_coroutine == null)
        {
            Debug.Log("return");
            _coroutine = StartCoroutine(MoveToTarget(_base));
        }
    }
}
