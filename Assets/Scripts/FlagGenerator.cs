using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagGenerator : SpawnerObject<Flag>
{
    private Vector3 target;

    public Flag CreateFlag(Vector3 position)
    {
        return base.Create(position);
    }

    public void Replace(Flag flag, Vector3 position)
    {
        OnGet(flag);
        flag.transform.position = position;
    }

    protected override Vector3 GetRandomPosition()
    {
        return target;
    }
}
