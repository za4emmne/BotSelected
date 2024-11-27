using UnityEngine;

[RequireComponent (typeof(Mover))]

public class Unit : MonoBehaviour
{
    private Mover _mover;
    private Resourse _target;
    private Base _base;
    private bool _isBusy;

    public bool IsBusy => _isBusy;

    private void Start()
    {
        _mover = GetComponent<Mover>();
        _isBusy = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Resourse>(out Resourse resourse))
        {
            if (_target == resourse)
            {
                Debug.Log("GotUp");
                _target.transform.parent = transform;
                _mover.ReturnToBase(_base.transform);
            }
        }

        if (other.TryGetComponent<Base>(out Base BaseBots))
        {
            if (_isBusy)
            {
                _target.transform.parent = _base.transform;
                _base.AddResourse(_target);
                _target.Release();
                _mover.StopMove();
                _isBusy = false;
            }
        }
    }

    public void Init(Base baseBot)
    {
        _base = baseBot;
    }

    public bool IsGetJob(Resourse resourse)
    {
        _target = resourse;
        _mover.Move(resourse.transform);
        _isBusy = !_isBusy;
        return _isBusy;
    }
}
