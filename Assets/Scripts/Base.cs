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

    private void Start()
    {
        _baseResourses = new List<Resourse>();
        _freeResourses = new List<Resourse>();
        _freeBots = new List<Unit>();
        _unitGenerator.StartGeneration();

        StartCoroutine(Work());
    }

    private IEnumerator Work()
    {
        while (enabled)
        {
            _freeResourses = _scaner.Scan();

            if (_freeResourses.Count > 0)
            {
                SelectedBots();

                foreach (Unit unit in _freeBots)
                {
                    unit.Move(_freeResourses[0].transform);
                    unit.ChangeStatus();
                    _baseResourses.Add(_freeResourses[0]);
                }
            }
            else
            {
                Debug.Log("резурсов нет");
            }


            yield return null;
        }
    }

    private void SelectedBots()
    {
        _freeBots.Clear();
        _freeBots = _unitGenerator.ActiveObject.Where(unit => unit.IsBusy == false).ToList();
    }

    private void SelectedResourses()
    {
        _freeResourses = _scaner.Scan();

        foreach (Resourse resourse in _freeResourses)
        {
            foreach (Resourse baseResourse in _baseResourses)
            {
                if(resourse.transform ==  baseResourse.transform)
                    _freeResourses.Remove(resourse);
            }
        }

        Debug.Log(_freeResourses.Count);
    }
}
