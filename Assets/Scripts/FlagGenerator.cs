using UnityEngine;

public class FlagGenerator : MonoBehaviour
{
    [SerializeField] private Flag _prefab;

    public Flag CreateFlag(Vector3 position)
    {
        return Instantiate(_prefab, position, Quaternion.identity);
    }

    public void Replace(Flag flag, Vector3 transform)
    {
        flag.transform.position = transform;
    }
}
