using UnityEngine;

public class UnitGenerator : SpawnerObject<Unit>
{
    [SerializeField] private Base _base;

    private Vector3 _basePosition;

    private void Update()
    {
        CheckUnits();
    }

    private void CheckUnits()
    {
        if (spawnCoroutine != null)
        {
            if (ActiveObject.Count == maxObjectsInScene)
            {
                StopCoroutine(spawnCoroutine);
            }
        }
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
}
