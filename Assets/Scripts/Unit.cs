using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private bool _isBusy;

    public bool IsBusy => _isBusy;

    private void Start()
    {
        _isBusy = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
