using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Mover : MonoBehaviour
{
    private float _speed;
    private NavMeshAgent _agent;

    public event Action<Transform> OnBaseReached;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _speed = 3.0f;
    }

    public void Move(Transform target)
    {
        _agent.destination = target.position;
        
        //if(_agent.remainingDistance ==  0)
        //{
        //    OnBaseReached?.Invoke(target);
        //}
    }

    public void ReturnToBase(Transform BaseTransform)
    {
        _agent.destination = BaseTransform.position;
    }
}
