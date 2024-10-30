using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Unit : MonoBehaviour
{
    public bool IsBusy => _isBusy;

    private float _speed = 3f;
    [SerializeField] private bool _isBusy;
    private Resourse _target;
    [SerializeField] private Transform _garage;
    private Coroutine _coroutine;
    private Base _base;

    private void Start()
    {
        _garage = transform.parent;
        _base = GetComponentInParent<Base>();
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


        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(MoveToTarget(_target.transform));

        }
    }

    private IEnumerator MoveToTarget(Transform target)
    {
        while (transform.position != target.transform.position)
        {
            transform.LookAt(_target.transform);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            if (target != null)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                            target.transform.position, _speed * Time.deltaTime);
            }

            yield return null;
        }

        //TakeResourse(target);
    }

    //private void PutResourse(Component other)
    //{
    //    if (other.TryGetComponent(out Resourse resourse))
    //    {
    //        if (resourse == _target)
    //        {
    //            _target.transform.parent = gameObject.transform;
    //            StopCoroutine(_coroutine);
    //            ReturnToBase();
    //        }
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Resourse>(out Resourse resourse))
        {
            if (resourse == _target)
            {
                _target.transform.parent = gameObject.transform;
                StopCoroutine(_coroutine);
                ReturnToBase();
            }
        }
    }

    public void ReturnToBase()
    {
        _coroutine = StartCoroutine(MoveToTarget(_garage));
    }
}
