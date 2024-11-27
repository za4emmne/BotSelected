using UnityEngine;

public class UnitGenerator : SpawnerObject<Unit>
{
    [SerializeField] private Base _base;
    [SerializeField] private int _startCountUnits;

    private Vector3 _basePosition;

    public void InitStartUnit()
    {
        for (int i = 0; i < _startCountUnits; i++)
        {
            GenerateObject();
        }
    }

    public void Create()
    {
        base.GenerateObject();
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
