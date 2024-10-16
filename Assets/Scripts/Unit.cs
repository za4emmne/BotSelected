using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Unit : MonoBehaviour
{
    public bool IsBusy => _isBusy;
    
    private bool _isBusy;

    private void Start()
    {
        _isBusy = false;
    }

    public void ChangeStatus()
    {
        _isBusy = !_isBusy;
    }
}
