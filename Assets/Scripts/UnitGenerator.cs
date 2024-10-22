using UnityEngine;

public class UnitGenerator : SpawnerObject<Unit>
{
    private Vector3 _basePosition;
    private void Start()
    {
        _basePosition = transform.localPosition;
        base.StartGeneration();
    }

    protected override Vector3 GetRandomPosition()
    {
        return _basePosition;
    }
}
