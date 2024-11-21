using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnerObject<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected int maxObjectsInScene;

    [SerializeField] private T _prefab;
    [SerializeField] private float _minPostionX;
    [SerializeField] private float _maxPostionX;
    [SerializeField] private float _minPositionZ;
    [SerializeField] private float _maxPositionZ;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;
    [SerializeField] private float _minDelaySpawn;
    [SerializeField] private float _maxDelaySpawn;

    [SerializeField] private int _minObjectsInScene;

    public int MinObjectInScene => _minObjectsInScene;
    public List<T> ActiveObject => _activeObject;

    private ObjectPool<T> _objectPool;
    private List<T> _activeObject;
    protected Coroutine spawnCoroutine;

    private void Awake()
    {
        _activeObject = new List<T>();
        _objectPool = new ObjectPool<T>
        (
            createFunc: () => Create(GetRandomPosition()), //�������� ��� �������� �������
            actionOnGet: (spawnObject) => OnGet(spawnObject), //�������� ��� ������ ���������� ������� �� ����
            actionOnRelease: (spawnObject) => OnRelease(spawnObject), //�������� ��� ����������� ������� � ���
            actionOnDestroy: (spawnerObject) => Delete(spawnerObject), //�������� ��� �������� ������� �� ����
            collectionCheck: true, //���������� �� ��������� ��������� ��� ����������� � ���, �������� ������ � ���������
            defaultCapacity: _poolCapacity, //������ ���� �� ���������
            maxSize: _poolMaxSize //������������ ������ ����
        );
    }


    public virtual void StartGeneration()
    {
        if (spawnCoroutine == null)
            spawnCoroutine = StartCoroutine(SpawnWithDelay());
    }

    protected virtual T Create(Vector3 vector3)
    {
        T spawnObject = Instantiate(_prefab, vector3, Quaternion.identity);

        return spawnObject;
    }

    protected virtual void OnGet(T spawnObject)
    {
        spawnObject.transform.parent = transform;
        spawnObject.gameObject.SetActive(true);
        _activeObject.Add(spawnObject);
    }

    public virtual void OnRelease(T spawnObject)
    {
        spawnObject.gameObject.SetActive(false);
        _activeObject.Remove(spawnObject);
    }

    public virtual void Delete(T spawnObject)
    {
        Destroy(spawnObject.gameObject);
    }

    protected virtual Vector3 GetRandomPosition()
    {
        float randomPositionX = Random.Range(_minPostionX, _maxPostionX);
        float randomPositionZ = Random.Range(_minPositionZ, _maxPositionZ);

        return new Vector3(randomPositionX, 0.55f, randomPositionZ);
    }

    //protected virtual bool IsEnough()
    //{
    //    if (ActiveObject.Count < maxObjectsInScene)
    //    {
    //        return false;
    //    }
    //    else
    //    {
    //        return true;
    //    }
    //}

    private IEnumerator SpawnWithDelay()
    {
        while (enabled)
        {
            float randomDelaySpawn = Random.Range(_minDelaySpawn, _maxDelaySpawn);
            WaitForSeconds waitSpawn = new WaitForSeconds(randomDelaySpawn);
            T obj = _objectPool.Get();

            yield return waitSpawn;
        }

        spawnCoroutine = null;
    }
}
