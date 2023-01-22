using UnityEngine;
using System.Collections;
using DG.Tweening;

public class AirplaneCollector : MonoBehaviour
{
    public Stash _stash;
    public Transform _dropPoint;
    public GameObject metal;
    public int metalCount;

    private void Awake()
    {
        _stash = GetComponent<Stash>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable") && metalCount < 80)
        {
            if (other.TryGetComponent(out Collectable collected))
            {
                _stash.AddStash(collected);
                metalCount = _stash.CollectedCount;
                

            }
        }
        if (other.CompareTag("Start"))
        {
            Debug.Log("Start");
            transform.DORotate(new Vector3(0, 0, 0), 1f).OnComplete(() => {
                transform.position = new Vector3(20, 65, 0);
            });
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DropPoint"))
        {
            transform.DORotate(new Vector3(0, 180, 0), 1f).OnComplete(() => {
                transform.position += new Vector3(0, 10, 0);
            });
            for (int i = 0; i < metalCount; i++)
            {
                GameObject metalClone = Instantiate(metal, _dropPoint.position, Quaternion.identity);
                metalClone.transform.parent = _dropPoint;
            }

            for (int i = 0; i < _stash.CollectedObjects.Count; i++)
            {
                Destroy(_stash.CollectedObjects[i].gameObject);
            }
            _stash.CollectedObjects.Clear();
            metalCount = 0;
        }
    }
}