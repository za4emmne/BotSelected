using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourseGenerator : SpawnerObject<Resourse>
{
    private void Start()
    {
        StartGeneration();
    }

    private void Update()
    {
        CheckResourses();
    }

    private void CheckResourses()
    {
        if (ActiveObject.Count < MinObjectInScene)
        {
            StartGeneration();
        }
        else if (ActiveObject.Count > maxObjectsInScene)
        {
            if (spawnCoroutine != null)
            {
                StopCoroutine(spawnCoroutine);
            }
        }
    }

    protected override void OnGet(Resourse spawnObject)
    {
        base.OnGet(spawnObject);
        spawnObject.Init(this);
    }
}
