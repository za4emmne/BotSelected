using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Resourse : MonoBehaviour
{
    private ResourseGenerator _generator;

    public void Init(ResourseGenerator generator)
    {
        _generator = generator;
    }

    public void Release()
    {
        _generator.OnRelease(this);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.TryGetComponent<Base>(out Base botBase))
    //    {
    //generator.OnRelease(this);
    //    }
    //}
}
