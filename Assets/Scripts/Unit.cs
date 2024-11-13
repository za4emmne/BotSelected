using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Base _base;
    private bool _isBusy;
    private float _speed;
    private Coroutine _coroutine;
    private Resourse _target;

    public event Action OnReached;

    public bool IsBusy => _isBusy;

    private void Start()
    {
        _coroutine = null;
        _isBusy = false;
        _speed = 6.0f;
    }

    public void Init(Base baseBot)
    {
        _base = baseBot;
    }

    public bool ChangeStatus()
    {
        _isBusy = !_isBusy;
        return _isBusy;
    }

    public void Move(Transform target)
    {
        _target = target.GetComponent<Resourse>();
        _coroutine = StartCoroutine(MoveToTarget(target));
    }

    private IEnumerator MoveToTarget(Transform target)
    {
        while (transform.position != target.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
            yield return null;
        }

        _coroutine = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Resourse>(out Resourse resourse))
        {
            if(_target == resourse)
            {
                _target.transform.parent = transform;
                _coroutine = StartCoroutine(MoveToTarget(_base.transform));
            }
        }
    }
}
