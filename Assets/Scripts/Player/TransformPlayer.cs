using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformPlayer : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Rigidbody rb;
    private CapsuleCollider _collider;
    private bool _isInTrigger = false;
    private Transform stashParent;

    public Stash stash;
    public GameObject stickman;
    public GameObject car;
    public GameObject yacht;
    public GameObject plane;
    public GameObject transformcar;
    public GameObject transformYacht;
    public GameObject transformStickman;
    public GameObject transformPlane;
    public GameObject camera;
    public GameObject planeCamera;
    public GameObject aircraft;


    public Movement movement;

    private void Start()
    {
        stashParent = GameObject.Find("StashParent").transform;
    }

    private void Awake()
    {
        aircraft = GameObject.Find("Aircraft");
        rb = GameObject.Find("Aircraft").GetComponent<Rigidbody>();
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Car"))
        {
            _isInTrigger = true;
            StartCoroutine(TransformCar());
        }
        if (other.CompareTag("Stickman"))
        {
            _isInTrigger = true;
            StartCoroutine(TransformStickman());
        }        
        if (other.CompareTag("Dock"))
        {
            _isInTrigger = true;
            StartCoroutine(TransformYacht());
        }
        if (other.CompareTag("Plane"))
        {
            _isInTrigger = true;
            StartCoroutine(TransformPlane());
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Car"))
        {
            _isInTrigger = false;
            StopCoroutine(TransformCar());
        }
        if (other.CompareTag("Stickman"))
        {
            _isInTrigger = false;
            StartCoroutine(TransformStickman());
        }
        if (other.CompareTag("Plane"))
        {
            _isInTrigger = true;
            StopCoroutine(TransformPlane());
        }
    }


    IEnumerator TransformCar()
    {
        yield return new WaitForSeconds(3f);

        while (_isInTrigger)
        {
            stickman.SetActive(false);
            car.SetActive(true);
            transformcar.SetActive(false);
            transformStickman.SetActive(true);
            movement.Speed = 35f;
            _isInTrigger = false;
            stash.maxCollectableCount = 10;
            stashParent.localPosition = new Vector3(0, 4.5f, -6f);
            yield return null;
        }

    }

    IEnumerator TransformYacht()
    {
        yield return new WaitForSeconds(3f);

        while (_isInTrigger)
        {
            stickman.SetActive(false);
            car.SetActive(false);
            yacht.SetActive(true);
            transformcar.SetActive(true);
            transformStickman.SetActive(true);
            transformYacht.SetActive(false);
            movement.Speed = 50f;
            _isInTrigger = false;
            stash.maxCollectableCount = 100;
            stashParent.localPosition = new Vector3(0, 2.5f, -1f);
            transform.position = new Vector3(120f ,1.6f, -30f);
            yield return null;
        }

    }

    IEnumerator TransformStickman()
    {
        yield return new WaitForSeconds(3f);
        
        while(_isInTrigger)
        {           
            camera.SetActive(true);
            planeCamera.SetActive(false);
            stickman.SetActive(true);
            car.SetActive(false);
            yacht.SetActive(false);
            transformcar.SetActive(true);
            transformYacht.SetActive(true);
            transformStickman.SetActive(false);
            movement.Speed = 15f;
            _isInTrigger = false;
            stash.maxCollectableCount = 5;
            stashParent.localPosition = new Vector3(0, 2.5f, -1f);
            yield return null;
        }

    }

    IEnumerator TransformPlane()
    {
        yield return new WaitForSeconds(3f);

        while (_isInTrigger)
        {
            Debug.Log("plane3s");
            camera.SetActive(false);
            planeCamera.SetActive(true);
            stickman.SetActive(false);
            car.SetActive(false);
            plane.SetActive(true);
            transformPlane.SetActive(false);
            transformcar.SetActive(true);
            transformStickman.SetActive(false);
            rb.constraints = RigidbodyConstraints.FreezePositionY;
            _isInTrigger = false;
            yield return null;
        }
    }

    public void StopPlane()
    {
        camera.SetActive(true);
        planeCamera.SetActive(false);
        stickman.SetActive(true);
        plane.SetActive(false);
        transformPlane.SetActive(true);
        stash.maxCollectableCount = 5;
        stashParent.localPosition = new Vector3(0, 2.5f, -1f);
        
        movement.Speed = 15f;
    }

}
