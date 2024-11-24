using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Resourse : MonoBehaviour
{
    public event Action<Resourse> PutOnGarage;

    public void Release()
    {
        PutOnGarage?.Invoke(this);
    }
}
