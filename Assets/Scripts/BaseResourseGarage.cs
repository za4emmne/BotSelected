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
        Debug.Log(_baseResourse.Count);

        if(_baseResourse.Count >= 3)
        {
            _baseResourse.RemoveRange(0, 3);
            GainThreeResourse?.Invoke();
        }
    }

    public int GetResourseCount()
    {
        return _baseResourse.Count;
    }
}
