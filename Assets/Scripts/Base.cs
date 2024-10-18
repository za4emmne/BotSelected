using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//получение ресурсов +
//определение координат ресурсов
//рекью компонент +
//корутина сканирования карты +
[RequireComponent(typeof(BaseScaner))]
public class Base : MonoBehaviour
{
    [SerializeField] private float _scanDelay;

    private BaseScaner _scaner;
    private List<Resourse> _freeResourse;
    private Transform _lastResoursePosition;

    public event Action SendFreeBot;

    private void Awake()
    {
        _scaner = GetComponent<BaseScaner>();
        _freeResourse = new List<Resourse>();
    }

    private void Start()
    {
        StartCoroutine(GetResourses());
    }

    public Transform GetNearPositionResourse()
    {
        float currentDistance;
        float _minDistance = 999;
        Transform target = null;

        foreach (var resourse in _freeResourse)
        {
            currentDistance = (transform.position - resourse.transform.position).sqrMagnitude;

            if (_minDistance > currentDistance)
            {
                if (_lastResoursePosition != resourse.transform)
                {
                    _minDistance = currentDistance;
                }
            }
        }

        return target;
    }

    public void RemoveResourseFromList()
    {
        _freeResourse.RemoveAt(0);
    }

    private IEnumerator GetResourses()
    {
        WaitForSeconds waitSpawn = new WaitForSeconds(_scanDelay);

        while (true)
        {
            _freeResourse = _scaner.Scan();
            SendFreeBot?.Invoke();
            yield return waitSpawn;
        }
    }
}
