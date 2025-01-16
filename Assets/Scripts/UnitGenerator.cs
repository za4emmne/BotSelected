using UnityEngine;

public class UnitGenerator : SpawnerObject<Unit>
{
    [SerializeField] private Base _base;
    [SerializeField] private int _startCountUnits;

    private Vector3 _basePosition;

    public void InitStartUnit(int count)
    {
        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                GenerateObject();
            }
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

    protected override void OnGet(Unit unit)
    {
        base.OnGet(unit);
        unit.Init(_base);
    }
}
