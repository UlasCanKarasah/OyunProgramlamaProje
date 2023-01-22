using UnityEngine;

public class PropellerRotation : MonoBehaviour
{
    public float rotationSpeed = 100f;


    private void Update()
    {
        transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
    }
}
