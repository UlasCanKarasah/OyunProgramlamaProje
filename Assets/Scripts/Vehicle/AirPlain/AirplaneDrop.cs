using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AirplaneDrop : MonoBehaviour
{
    public List<Collectable> SpawnedCollectables = new List<Collectable>();
    [SerializeField] private int _maxSpawnCount = 10;
    [SerializeField] private float _spawnRadius = 10;
    [SerializeField] private float minimumDistance = 2.5f;

    private float nextSpawnTime = 0;
    AirplaneCollector airplaneCollector;
    private Stash _planeStash;
    public Collectable metal;
    private int _metal;


    private void Start()
    {
        _planeStash = GetComponent<Stash>();
        airplaneCollector = FindObjectOfType<AirplaneCollector>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Plane"))
        {
            //Tren drop pointe geldiginde ustundeki metaller sayilir ve spawn edilir
            _metal = airplaneCollector.metalCount;
            Debug.Log(_metal+"sayisi");
            SpawnMetal();
            _planeStash.CollectedObjects.Clear();
        }
    }

    private void SpawnMetal()
    {

        for (int i = 0; i < _metal; i++)
        {
            var circlePos = Random.insideUnitCircle;
            Vector3 spawnPosition = new Vector3(circlePos.x, 0, circlePos.y) * _spawnRadius;
            spawnPosition += transform.position;
            
            if (!IsPositionValid(spawnPosition))
            {
                //Invalidposition oldugunda metal count azalmasini engelliyoruz
                _metal += 1;
                return;
            }

            var collectable = Instantiate(metal, null);
            collectable.transform.position = spawnPosition;
            SpawnedCollectables.Add(collectable);

            collectable.transform.localScale = Vector3.zero;

            collectable.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack, 2.5f);
            collectable.transform.DORotate(Vector3.up * 360f, 5f, RotateMode.LocalAxisAdd).SetLoops(-1);
        }
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