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
    private List<Resourse> _resourses;

    public event Action SendFreeBot;

    private void Awake()
    {
        _scaner = GetComponent<BaseScaner>(); 
        _resourses = new List<Resourse>(); 
    }

    private void Start()
    {

        StartCoroutine(GetResourses());
    }

    public Transform GetPositionResourse()
    {
        //счетчик сделать в этом классе
        return _resourses[0].transform;
    }

    private IEnumerator GetResourses()
    {
        WaitForSeconds waitSpawn = new WaitForSeconds(_scanDelay);

        while (true)
        {
            _resourses = _scaner.Scan();
            SendFreeBot?.Invoke();
            yield return waitSpawn;
        }
    }
}
