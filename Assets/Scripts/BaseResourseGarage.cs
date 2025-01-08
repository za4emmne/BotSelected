using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseResourseGarage : MonoBehaviour
{
    [SerializeField] private FlagSetter _flagSetter;

    private List<Resourse> _baseResourse;

    public event Action GainThreeResourse;
    public event Action OnCanBuilded;

    private void Start()
    {
        _baseResourse = new();
    }

    public void AddResourse(Resourse resourse)
    {
        _baseResourse.Add(resourse);

        if (_baseResourse.Count >= 3 && _flagSetter.IsCreated == false)
        {
            _baseResourse.RemoveRange(0, 3);
            GainThreeResourse?.Invoke();
        }

        if (_baseResourse.Count >= 5 && _flagSetter.IsCreated == true)
        {
            OnCanBuilded?.Invoke();
        }
    }

    public int GetResourseCount()
    {
        if (_baseResourse == null)
            return 0;
        else
            return _baseResourse.Count;
    }
}
