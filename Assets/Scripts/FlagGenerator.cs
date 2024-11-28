using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagGenerator : SpawnerObject<Flag>
{
    private Vector3 target;

    public void CreateFlag(Vector3 position)
    {
        target = position;
        base.GenerateObject();
    }

    protected override Vector3 GetRandomPosition()
    {
        return target;
    }
}
