using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourseGenerator : SpawnerObject<Resourse>
{
    private void Start()
    {
        StartGeneration();
    }

    public override void StartGeneration()
    {
        if (IsEmptyResourse())
            base.StartGeneration();
    }

    private bool IsEmptyResourse()
    {
        if (ActiveObject.Count < MinObjectInScene)
            return true;
        else
            return false;
    }
}
