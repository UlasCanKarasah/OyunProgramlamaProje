using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stash))]
public class CollectorTrain : MonoBehaviour
{
    private Stash _stash;
    TrainDropPoint traindroppoint;
    public int metalCount;
    public GameObject metal;

    private void Awake()
    {
        _stash = GetComponent<Stash>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable") && metalCount < 20)
        {
            
            if (other.TryGetComponent(out Collectable collected))
            {
                _stash.AddStash(collected);
                metalCount = _stash.CollectedCount;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DropPoint"))
        {
            metalCount = _stash.CollectedCount;
            for (int i = 0; i < _stash.CollectedObjects.Count; i++)
            {
                Destroy(_stash.CollectedObjects[i].gameObject);
            }
            _stash.CollectedObjects.Clear();
        }
           
    }
}