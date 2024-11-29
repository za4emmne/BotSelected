using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseResourseGarage : MonoBehaviour
{
    private List<Resourse> _baseResourse;

    public event Action GainThreeResourse;

    private void Start()
    {
        _baseResourse = new();
    }

    public void AddResourse(Resourse resourse)
    {
        _baseResourse.Add(resourse);

        if (_baseResourse.Count >= 3)
        {
            _baseResourse.RemoveRange(0, 3);
            GainThreeResourse?.Invoke();
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
