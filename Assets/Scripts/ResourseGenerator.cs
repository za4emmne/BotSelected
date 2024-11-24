using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResourseGenerator : SpawnerObject<Resourse>
{
    [SerializeField] private Base _base;

    private void Start()
    {
        StartGeneration();
    }

    private void Update()
    {
        ControlGenerationResorse();
    }

    private void ControlGenerationResorse()
    {
        if (GetCount() < MinObjectsInScene)
        {
            StartGeneration();
        }
        
        if (GetCount() > MaxObjectsInScene)
        {
            if (spawnCoroutine != null)
            {
                StopCoroutine(spawnCoroutine);
            }
        }
    }

    protected override void OnGet(Resourse spawnObject)
    {
        spawnObject.PutOnGarage += OnRelease;
        spawnObject.transform.position = GetRandomPosition();
        base.OnGet(spawnObject);
    }
}
