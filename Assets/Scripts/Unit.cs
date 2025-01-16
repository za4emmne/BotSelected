using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Mover))]

public class Unit : MonoBehaviour
{
    private Mover _mover;
    private BaseGenerator _generator;
    private Resourse _target;
    private Base _base;
    private bool _isBusy;

    public bool IsBusy => _isBusy;

    public event Action<Flag, Unit> OnCreatedBase;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _generator = GetComponent<BaseGenerator>();
    }

    private void Start()
    {
        _isBusy = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Resourse>(out Resourse resourse) && _target == resourse)
        {
            _target.transform.parent = transform;
            _mover.ReturnToBase(_base.transform);
        }

        if (other.TryGetComponent<Base>(out Base BaseBots) && transform.childCount > 0 && _isBusy)
        {
            _isBusy = false;
            _target.transform.parent = _base.transform;
            _base.AddResourse(_target); 
            _target.Release();
        }

        if (other.TryGetComponent<Flag>(out Flag flag) && _isBusy)
        {
            Base botBase = _generator.Create(flag.transform);
            Init(botBase);
            botBase.Initialize(0);
            flag.gameObject.SetActive(false);
            _isBusy = false;
        }
    }

    public void MoveToTarget(Transform position)
    {
        _isBusy = !_isBusy;

        if (position != null)
        {
            _mover.Move(position);
        }
    }

    public void Init(Base baseBot)
    {
        _base = baseBot;
        transform.parent = _base.transform;
    }

    public bool IsGetJob(Resourse resourse)
    {
        _target = resourse;
        _mover.Move(_target.transform);
        _isBusy = !_isBusy;
        return _isBusy;
    }
}
