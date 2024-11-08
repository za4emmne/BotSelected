using System;
using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public bool IsBusy => _isBusy;

    [SerializeField] private float _speed = 3f;
    [SerializeField] private bool _isBusy;
    [SerializeField] private bool _isPutUpResourse;

    private Resourse _target;
    private Coroutine _coroutine;

    private void Start()
    {
        _target = null;
        _isPutUpResourse = false;
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

        PutOnResourse();
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
                _isPutUpResourse = true;
                ReturnToBase();
            }
        }
    }

    private void ReturnToBase()
    {
        _coroutine = StartCoroutine(MoveToTarget(Base.singleton.transform));
    }

    private void PutOnResourse()
    {
        if (_isPutUpResourse)
        {
            _target.transform.parent = Base.singleton.transform;
            Base.singleton.OnAddResourse();
            _coroutine = null;
            _isBusy = false;
            _isPutUpResourse = false;
        }
    }
}
