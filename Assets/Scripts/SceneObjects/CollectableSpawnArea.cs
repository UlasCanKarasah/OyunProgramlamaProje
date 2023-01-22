using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectableSpawnArea : MonoBehaviour
{
    public Collectable collectablePrefab;

    public List<Collectable> SpawnedCollectables = new List<Collectable>();
    [SerializeField] private int _maxSpawnCount = 10;
    [SerializeField] private float _spawnRadius = 10;

    [SerializeField] private float _spawnPeriod = 2f;

    [SerializeField] private float minimumDistance = 5f;

    private float nextSpawnTime = 0;

    void Update()
    {
        HandleNullElements();
        if (SpawnedCollectables.Count >= _maxSpawnCount)
        {
            return; 
        }

        if (Time.time >= nextSpawnTime)
        {
            nextSpawnTime = Time.time + _spawnPeriod;
            Spawn();
        }
          
    }

    private void Spawn()
    {
        var circlePos = Random.insideUnitCircle;
        Vector3 spawnPosition = new Vector3(circlePos.x, 0, circlePos.y) * _spawnRadius;
        spawnPosition += transform.position;

        if (!IsPositionValid(spawnPosition))
            return;

        var collectable = Instantiate(collectablePrefab, null);
        collectable.transform.position = spawnPosition; 
        SpawnedCollectables.Add(collectable);

        collectable.transform.localScale = Vector3.zero;

        collectable.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack, 2.5f);
        collectable.transform.DORotate(Vector3.up * 360f, 5f, RotateMode.LocalAxisAdd).SetLoops(-1);
         
    }

    private void HandleNullElements()
    { 
        for (int i = SpawnedCollectables.Count - 1; i >= 0; i--)
        {
            if (SpawnedCollectables[i] == null)
            {
                SpawnedCollectables.RemoveAt(i);
            }  
        }

    }

    private bool IsPositionValid(Vector3 position)
    {
        foreach (var existingCollectable in SpawnedCollectables)
        {
            if (Vector3.Distance(existingCollectable.transform.position, position) < minimumDistance)
                return false;
        }
        return true;
    }
}
