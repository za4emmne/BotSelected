using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Resourse : MonoBehaviour
{
    private ResourseGenerator _generator;

    public void Init(ResourseGenerator generator)
    {
        _generator = generator;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Base>(out Base botBase))
        {
            transform.parent = botBase.transform;
            _generator.OnRelease(this);
        }
    }
}
