using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//получение ресурсов +
//определение координат ресурсов +
//выдача позиции ближайшего ресурса
[RequireComponent(typeof(BaseScaner))]
public class Base : MonoBehaviour
{
    [SerializeField] private float _scanDelay;

    private BaseScaner _scaner;
    private List<Resourse> _freeResourse;
    private Coroutine _coroutine;
    private Resourse _target;

    public event Action SendFreeBot;

    private void Awake()
    {
        _scaner = GetComponent<BaseScaner>();
        _freeResourse = null;
    }

    private void Start()
    {
        StartCoroutine(Work());
    }

    //private void OnAddResourse()
    //{

    //}

    //private void OnEnable()
    //{
    //    _scaner.OnScanComplete += OnAddResourse;
    //}

    //private void OnDisable()
    //{
    //    _scaner.OnScanComplete -= OnAddResourse;
    //}

    public Resourse GetNearResourse()
    {
        float currentDistance;
        float _minDistance = 999;


        for (int i = 0; i < _freeResourse.Count; i++)
        {
            currentDistance = (transform.position - _freeResourse[i].transform.position).sqrMagnitude;

            if (_minDistance > currentDistance)
            {
                _minDistance = currentDistance;
                _target = _freeResourse[i];
                _freeResourse.RemoveAt(i);
            }
        }

        return _target;
    }

    private IEnumerator Work()
    {
        WaitForSeconds waitSpawn = new WaitForSeconds(_scanDelay);

        while (true)
        {
            _freeResourse = _scaner.Scan();

            if (_freeResourse.Count > 0)
            {
                SendFreeBot?.Invoke();
            }

            yield return waitSpawn;
        }
    }
}
