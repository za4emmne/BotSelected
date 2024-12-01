using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Mover : MonoBehaviour
{
    private float _speed;
    private NavMeshAgent _agent;

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
    }

    public void ReturnToBase(Transform BaseTransform)
    {
        _agent.destination = BaseTransform.position;
    }
}
