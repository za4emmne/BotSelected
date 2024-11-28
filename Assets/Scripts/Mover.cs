using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Mover : MonoBehaviour
{
    private float _speed;
    private Coroutine _coroutine;

    private void Start()
    {
        _coroutine = null;
        _speed = 3.0f;
    }

    public void Move(Transform target)
    {
        _coroutine = StartCoroutine(MoveToTarget(target));
    }

    public void StopMove()
    {
        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    public void ReturnToBase(Transform BaseTransform)
    {
        StopMove();
        _coroutine = StartCoroutine(MoveToTarget(BaseTransform));
    }

    private IEnumerator MoveToTarget(Transform target)
    {
        while (transform.position != target.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

            yield return null;
        }

        _coroutine = null;
    }
}
