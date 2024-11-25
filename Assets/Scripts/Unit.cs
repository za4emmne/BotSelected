using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Unit : MonoBehaviour
{
    private Base _base;
    private bool _isBusy;
    private float _speed;
    private Coroutine _coroutine;
    private Resourse _target;

    public bool IsBusy => _isBusy;

    private void Start()
    {
        _coroutine = null;
        _isBusy = false;
        _speed = 6.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Resourse>(out Resourse resourse))
        {
            if (_target == resourse)
            {
                _target.transform.parent = transform;
                ReturnToBase();
            }
        }

        if (other.TryGetComponent<Base>(out Base BaseBots))
        {
            if (_isBusy)
            {
                _target.transform.parent = _base.transform;
                _target.Release();
                _base.AddResourse();

                if (_coroutine != null)
                {
                    StopCoroutine(_coroutine);
                }

                IsChangeBusyStatus();
            }
        }
    }

    public void Init(Base baseBot)
    {
        _base = baseBot;
    }

    public bool IsChangeBusyStatus()
    {
        _isBusy = !_isBusy;
        return _isBusy;
    }

    public void Move(Transform target)
    {
        _coroutine = StartCoroutine(MoveToTarget(target));

        if(target.GetComponent<Resourse>() != null)
        {
            _target = target.GetComponent<Resourse>();
        }
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

    private void ReturnToBase()
    {
        StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(MoveToTarget(_base.transform));
    }
}
