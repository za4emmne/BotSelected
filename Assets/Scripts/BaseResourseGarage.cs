using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseResourseGarage : MonoBehaviour
{
    private List<Resourse> _baseResourse;

    private void Start()
    {
        _baseResourse = new();
    }

    public void AddResourse(Resourse resourse)
    {
        _baseResourse.Add(resourse);
    }

    public int GetResourseCount()
    {
        return _baseResourse.Count;
    }
}
