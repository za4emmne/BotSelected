using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourseGenerator : SpawnerObject<Resourse>
{
    public override void StartGeneration()
    {
        if (IsEmptyResourse())
            base.StartGeneration();
    }
}
