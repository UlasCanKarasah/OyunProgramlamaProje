using UnityEngine;

public class Airplane : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public VariableJoystick joystick;

    public bool ters = false;

    public float forwardSpeed = 15f;
    public float horizontalSpeed = 4f;
    public float verticalSpeed = 4f;

    public float maxHorizontalRotation = 0.1f;
    public float maxVerticalRotation = 0.06f;

    public float smoothness = 5f;
    public float rotationSmoothness = 5f;

    private float forwardSpeedMultiplier = 100f;
    private float speedMultiplier = 1000f;

    public float maxAltitude = 50f;

    private float horizontalInput;
    private float verticalInput;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>(); 
    }

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            horizontalInput = joystick.Horizontal;
            verticalInput = joystick.Vertical;
        }
        else
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
        }

        if (transform.position.y < maxAltitude)
        {
            transform.position = new Vector3(transform.position.x, maxAltitude, transform.position.z);
        }

        //HandlePlaneRotation();

    }

    //donerken sikinti yasadigim icin kaldirdim

    //private void HandlePlaneRotation()
    //{
    //    float horizontalRotation = horizontalInput * maxHorizontalRotation;
    //    float verticalRotation = verticalInput * maxVerticalRotation;

    //    transform.rotation = Quaternion.Lerp(
    //            transform.rotation,
    //            new Quaternion(
    //                verticalRotation,
    //                transform.rotation.y,
    //                horizontalRotation,
    //                transform.rotation.w
    //                ),
    //                Time.deltaTime * rotationSmoothness
    //            );
    //}

    private void FixedUpdate()
    {
        HandlePlaneMovement();
    }

    private void HandlePlaneMovement()
    {
        _rigidbody.velocity = new Vector3(
        _rigidbody.velocity.x,
        _rigidbody.velocity.y,
        forwardSpeed * forwardSpeedMultiplier * Time.deltaTime
        );

        float xVelocity = horizontalInput * speedMultiplier * horizontalSpeed * Time.deltaTime;
        float yVelocity = -verticalInput * speedMultiplier * verticalSpeed * Time.deltaTime;

        if (transform.position.y < maxAltitude)
        {
            yVelocity += 50;
        }

        if (transform.rotation.eulerAngles.y == 180 )
        {
            ters = true;
            _rigidbody.velocity = new Vector3(-_rigidbody.velocity.x, _rigidbody.velocity.y, -_rigidbody.velocity.z);
        }
        else
        {
            ters = false;
            _rigidbody.velocity = Vector3.Lerp(
            _rigidbody.velocity,
            new Vector3(xVelocity, yVelocity, _rigidbody.velocity.z),
            Time.deltaTime * smoothness
            );
        }


    }


}


