using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;
//получение ресурсов +
//определение координат ресурсов +
//выдача позиции ближайшего ресурса
[RequireComponent(typeof(BaseScaner))]
public class Base : MonoBehaviour
{
    [SerializeField] private float _scanDelay;
    [SerializeField] private int _resourseCount;

    private BaseScaner _scaner;
    private List<Resourse> _freeResourse;
    private List<Resourse> _busyResourse;
    private Coroutine _coroutine;
    private Resourse _target;

    public event Action SendFreeBot;

    public static Base singleton { get; private set; }

    private void Awake()
    {
        singleton = this;
        _scaner = GetComponent<BaseScaner>();
        _freeResourse = new List<Resourse>();
        _busyResourse = new List<Resourse>();
    }

    private void Start()
    {
        _target = null;
        _resourseCount = 0;
        StartCoroutine(Work());
    }

    public void OnAddResourse()
    {
        _resourseCount++;
    }

    public Resourse GetNearResourse()
    {
        int number = -1;
        float currentDistance;
        float _minDistance = 999;


        for (int i = 0; i < _scaner.Resourses.Count; i++)
        {
            currentDistance = (transform.position - _scaner.Resourses[i].transform.position).sqrMagnitude;

            if (_minDistance > currentDistance)
            {
                _minDistance = currentDistance;
                number = i;
            }
        }

        if (number >= 0)
        {
            _target = _scaner.Resourses[number];
            _busyResourse.Add(_target);
            _scaner.RemoveResourse(_target);
        }

        return _target;
    }

    private IEnumerator Work()
    {
        WaitForSeconds waitSpawn = new WaitForSeconds(_scanDelay);

        while (true)
        {
            _scaner.Scan();

            if (_scaner.Resourses.Count > 0)
            {
                SendFreeBot?.Invoke();
            }

            if (_busyResourse.Count > 0)
            {
                foreach (Resourse resourse in _scaner.Resourses)
                {
                    if (_busyResourse.Contains(resourse))
                    {
                        _scaner.RemoveResourse(resourse);
                    }
                }
            }

            yield return waitSpawn;
        }
    }
}
