using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    [SerializeField] private UnitGenerator _unitGenerator;
    [SerializeField] private BaseScaner _scaner;
    [SerializeField] private Text _resourseCountText;

    private List<Resourse> _freeResourses;
    private List<Resourse> _baseResourses;
    private List<Unit> _freeBots;
    private int _resourseCount;

    private void Start()
    {
        _resourseCount = 0;
        _resourseCountText.text = "Ресурсов на базе: " + _resourseCount;
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
            SelectedResourses();
            SelectedBots();

            if (TryGetFreeResource(out Resourse resourse) && TryGetFreeUnit(out Unit unit))
            {
                unit.ChangeStatus();
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
        _resourseCountText.text = "Ресурсов на базе: " + _resourseCount;
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

    private void SelectedBots()
    {
        _freeBots.Clear();
        _freeBots = _unitGenerator.ActiveObject.Where(unit => unit.IsBusy == false).ToList();
    }

    private void SelectedResourses()
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
