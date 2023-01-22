using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneCamera : MonoBehaviour
{
    public Transform TargetTransform;
    Airplane airplane;
    public Vector3 offset = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - TargetTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = TargetTransform.position + offset;

        if (TargetTransform != null)
        {

            if (TargetTransform.GetComponent<Airplane>().ters)
            {
                transform.rotation = Quaternion.Euler(45, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(45, 0, 0);
            }

            transform.position = newPosition;
        }
    }
}
