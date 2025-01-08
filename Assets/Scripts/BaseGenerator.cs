using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BaseGenerator : MonoBehaviour
{
    [SerializeField] private Base _prefab;

    public Base Create(Transform transform)
    {
        return Instantiate(_prefab, transform.position, Quaternion.identity);
    }
}
