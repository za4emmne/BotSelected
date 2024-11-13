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

    private bool IsEmptyResourse() //переделать
    {
        if (ActiveObject.Count < MinObjectInScene)
            return true;
        else
            return false;
    }

    protected override void OnGet(Resourse spawnObject)
    {
        base.OnGet(spawnObject);
        spawnObject.Init(this);
    }
}
