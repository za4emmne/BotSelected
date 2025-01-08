using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnerObject<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected int MaxObjectsInScene;
    [SerializeField] protected int MinObjectsInScene;
    [SerializeField] protected int PoolMaxSize;

    [SerializeField] private T _prefab;
    [SerializeField] private float _minPostionX;
    [SerializeField] private float _maxPostionX;
    [SerializeField] private float _minPositionZ;
    [SerializeField] private float _maxPositionZ;
    [SerializeField] private int _poolCapacity;


    private ObjectPool<T> _objectPool;
    private List<T> _activeObjects;

    private void Awake()
    {
        PoolMaxSize = 3;
        _activeObjects = new List<T>();
        _objectPool = new ObjectPool<T>
        (
            createFunc: () => Create(GetRandomPosition()),
            actionOnGet: (spawnObject) => OnGet(spawnObject),
            actionOnRelease: (spawnObject) => OnRelease(spawnObject),
            actionOnDestroy: (spawnerObject) => Delete(spawnerObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: PoolMaxSize
        );
    }

    public T GetObjectForIndex(int index)
    {
        return _activeObjects[index];
    }

    public int GetCount()
    {
        return _activeObjects.Count;
    }

    protected virtual void GenerateObject()
    {
        T obj = _objectPool.Get();
    }

    public virtual void OnRelease(T spawnObject)
    {
        spawnObject.gameObject.SetActive(false);
        _activeObjects.Remove(spawnObject);
    }

    public virtual void Delete(T spawnObject)
    {
        Destroy(spawnObject.gameObject);
    }
    protected virtual T Create(Vector3 vector3)
    {
        return Instantiate(_prefab, vector3, Quaternion.identity);
    }

    protected virtual void OnGet(T spawnObject)
    {
        spawnObject.transform.parent = transform;
        spawnObject.gameObject.SetActive(true);
        _activeObjects.Add(spawnObject);
    }

    protected virtual Vector3 GetRandomPosition()
    {
        float randomPositionX = Random.Range(_minPostionX, _maxPostionX);
        float randomPositionZ = Random.Range(_minPositionZ, _maxPositionZ);

        return new Vector3(randomPositionX, 0.55f, randomPositionZ);
    }
}
