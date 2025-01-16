using System.Collections;
using UnityEngine;

public class ResourseGenerator : SpawnerObject<Resourse>
{
    [SerializeField] private Base _base;
    [SerializeField] private float _minDelaySpawn;
    [SerializeField] private float _maxDelaySpawn;

    private Coroutine SpawnCoroutine;

    private void Start()
    {
        SpawnCoroutine = StartCoroutine(SpawnWithDelay());
    }

    private void Update()
    {
        ControlGenerationResorse();
    }

    private void ControlGenerationResorse()
    {
        if (GetCount() < MinObjectsInScene)
        {
            SpawnCoroutine = StartCoroutine(SpawnWithDelay());
        }
        
        if (GetCount() > MaxObjectsInScene)
        {
            if (SpawnCoroutine != null)
            {
                StopCoroutine(SpawnCoroutine);
            }
        }
    }

    protected override void OnGet(Resourse spawnObject)
    {
        spawnObject.PutOnGarage += OnRelease;
        spawnObject.transform.position = GetRandomPosition();
        base.OnGet(spawnObject);
    }

    private IEnumerator SpawnWithDelay()
    {
        while (enabled)
        {
            float randomDelaySpawn = Random.Range(_minDelaySpawn, _maxDelaySpawn);
            WaitForSeconds waitSpawn = new WaitForSeconds(randomDelaySpawn);
            base.GenerateObject();

            yield return waitSpawn;
        }

        SpawnCoroutine = null;
    }
}

