using UnityEngine;

public class UnitGenerator : SpawnerObject<Unit>
{
    [SerializeField] private Base _base;

    private Vector3 _basePosition;

    private void Update()
    {
        StopGenerateUnits();
    }

    protected override Vector3 GetRandomPosition()
    {
        _basePosition = transform.localPosition;
        return _basePosition;
    }

    protected override void OnGet(Unit spawnObject)
    {
        base.OnGet(spawnObject);
        spawnObject.Init(_base);
    }

    private void StopGenerateUnits()
    {
        if (SpawnCoroutine != null)
        {
            if (GetCount() == MaxObjectsInScene)
            {
                StopCoroutine(SpawnCoroutine);
            }
        }
    }
}
