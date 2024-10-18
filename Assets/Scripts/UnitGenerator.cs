using UnityEngine;

public class UnitGenerator : SpawnerObject<Unit>
{
    private void Start()
    {
        base.StartGeneration();
    }

    protected override Vector3 GetRandomPosition()
    {
        return new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
}
