using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private UnitGenerator _unitGenerator;
    [SerializeField] private BaseScaner _scaner;

    private List<Resourse> _freeResourses;
    private List<Resourse> _baseResourses;
    private List<Unit> _freeBots;
    private int _resourseCount;

    public event Action ChangedResourseCount;

    public int ResourseCount => _resourseCount;

    private void Start()
    {
        _resourseCount = 0;
        _baseResourses = new();
        _freeResourses = new();
        _freeBots = new();
        _unitGenerator.StartGeneration();
        StartCoroutine(Work());
    }

    private IEnumerator Work()
    {
        while (enabled)
        {
            SelectResourses();
            SelectBots();

            if (TryGetFreeResource(out Resourse resourse) && TryGetFreeUnit(out Unit unit))
            {
                unit.IsChangeBusyStatus();
                unit.Move(resourse.transform);
                _freeResourses.Remove(resourse);
                _baseResourses.Add(resourse);
            }

            yield return null;
        }
    }

    public void AddResourse()
    {
        _resourseCount++;
        ChangedResourseCount?.Invoke();
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
        return _freeResourses.Contains(resourse) || _baseResourses.Contains(resourse);
    }
}
