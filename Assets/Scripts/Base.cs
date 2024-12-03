using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private UnitGenerator _unitGenerator;
    [SerializeField] private FlagSetter _flagSetter;
    [SerializeField] private BaseResourseGarage _garage;
    [SerializeField] private BaseScaner _scaner;

    private List<Resourse> _freeResourses;
    private List<Resourse> _busyResourses;
    private List<Unit> _freeBots;
    private int _resourseCount;
    private Transform _newBase;
    

    public event Action ChangedResourseCount;

    public int ResourseCount => _resourseCount;

    private void Start()
    {
        _resourseCount = 0;
        _busyResourses = new();
        _freeResourses = new();
        _freeBots = new();
        _unitGenerator.InitStartUnit();
        StartCoroutine(Work());
    }

    private void OnEnable()
    {
        _garage.GainThreeResourse += CreateUnit;
        _flagSetter.BuildNewBase += Build;
    }

    private void OnDisable()
    {
        _garage.GainThreeResourse -= CreateUnit;
        _flagSetter.BuildNewBase -= Build;
    }

    public void AddResourse(Resourse resourse)
    {
        _garage.AddResourse(resourse);
        ChangedResourseCount?.Invoke();
    }

    private IEnumerator Work()
    {
        while (enabled)
        {
            SelectResourses();
            SelectBots();

            if (TryGetFreeResource(out Resourse resourse) && TryGetFreeUnit(out Unit unit))
            {
                unit.IsGetJob(resourse);
                _freeResourses.Remove(resourse);
                _busyResourses.Add(resourse);
            }

            yield return null;
        }
    }

    private void Build()
    {
        Debug.Log("base");
        StopCoroutine(Work());

        if(_flagSetter.Position() != null && TryGetFreeUnit(out Unit unit))
        {
            
            
            _newBase.position = new Vector3(_flagSetter.Position().x, _flagSetter.Position().y, _flagSetter.Position().z);
            unit.MoveToTarget(_newBase);
        }
    }

    private void CreateUnit()
    {
        _unitGenerator.Create();
    }

    private bool TryGetFreeUnit(out Unit unit)
    {
        unit = _freeBots.FirstOrDefault();

        return unit != null;
    }

    private bool TryGetFreeResource(out Resourse resourse)
    {
        resourse = _freeResourses.FirstOrDefault();

        return resourse != null;
    }

    private void SelectBots()
    {
        _freeBots.Clear();

        for (int i = 0; i < _unitGenerator.GetCount(); i++)
        {
            if (_unitGenerator.GetObjectForIndex(i).IsBusy == false)
            {
                _freeBots.Add(_unitGenerator.GetObjectForIndex(i));
            }
        }
    }

    private void SelectResourses()
    {
        List<Resourse> resourses = _scaner.Scan();

        foreach (Resourse resource in resourses.Where(resource => CheckAvailabilityInLists(resource) == false))
        {
            _freeResourses.Add(resource);
        }
    }

    private bool CheckAvailabilityInLists(Resourse resourse)
    {
        return _freeResourses.Contains(resourse) || _busyResourses.Contains(resourse);
    }
}
