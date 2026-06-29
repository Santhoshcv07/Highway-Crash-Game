using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CarController: MonoBehaviour
{
   [SerializeField] private WheelCollider frontRightWheelCollider;
     [SerializeField] private WheelCollider backRightWheelCollider; 
     [SerializeField] private WheelCollider frontLeftWheelCollider;
     [SerializeField] private WheelCollider backLeftWheelCollider;

   [SerializeField] private Transform frontRightWheelTransform;
  [SerializeField]   private Transform backRightWheelTransform;
     [SerializeField] private Transform frontLeftWheelTransform;
     [SerializeField]  private Transform backLeftWheelTransform;
      
      [SerializeField] private Transform carCentreOfMassTransform;
      
      [SerializeField] private float motorForce = 100f;
      [SerializeField] private float steeringAngle = 30f;
     [SerializeField]  private float brakeForce = 1000f;
     [SerializeField] UIManager uiManager;

     private Rigidbody rigidbody;
      private float verticalInput;
    private float horizontalInput;
    private float currentMotorTorque;
    private float currentSteerAngle;


private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
// Start is called before the first frame update
void Start()
{
   
   rigidbody.centerOfMass = carCentreOfMassTransform.localPosition;
}
public void SetUiManager(UIManager manager)
    {
        uiManager = manager;
    }
// Update is called once per frame
void FixedUpdate()
{
   MotorForce();
   UpdateWheels();
   GetInput();
   Steering();
   ApplyBrakes();
   PowerSteering();
   Debug.Log(CarSpeed());
}
void GetInput()
    {
       verticalInput = Input.GetAxis("Vertical");
  horizontalInput = Input.GetAxis("Horizontal");
    }
    void ApplyBrakes()
    {
        if (Input.GetKey(KeyCode.Space))
        {
             frontRightWheelCollider.brakeTorque = brakeForce;
        backRightWheelCollider.brakeTorque = brakeForce;
        frontLeftWheelCollider.brakeTorque = brakeForce;
        backLeftWheelCollider.brakeTorque = brakeForce;
        rigidbody.linearDamping = 1f;
        }
        else
        {
               frontRightWheelCollider.brakeTorque = 0f;
        backRightWheelCollider.brakeTorque = 0f;
        frontLeftWheelCollider.brakeTorque = 0f;
        backLeftWheelCollider.brakeTorque = 0f;
        rigidbody.linearDamping = 0f;
        }

       
    }
void MotorForce()
{
    float targetTorque = motorForce * verticalInput;

    currentMotorTorque = Mathf.Lerp(
        currentMotorTorque,
        targetTorque,
        Time.fixedDeltaTime * 3f
    );

    frontRightWheelCollider.motorTorque = currentMotorTorque;
    frontLeftWheelCollider.motorTorque = currentMotorTorque;
}
void Steering()
{
    float speed = CarSpeed();

    // Steering becomes smaller as speed increases
    float maxSteer = Mathf.Lerp(30f, 8f, speed / 120f);

    float targetSteer = horizontalInput * maxSteer;

   if (horizontalInput != 0)
{
    currentSteerAngle = horizontalInput * maxSteer;
}
else
{
    currentSteerAngle = 0f;
}

    frontRightWheelCollider.steerAngle = currentSteerAngle;
    frontLeftWheelCollider.steerAngle = currentSteerAngle;
}
    void PowerSteering()
    {
        if(horizontalInput==0)
        {
            transform.rotation=Quaternion.Slerp(transform.rotation,Quaternion.Euler(0,0,0),Time.deltaTime);
        }
    }
    void UpdateWheels()
    {
        RotateWheel(frontRightWheelCollider,frontRightWheelTransform);
        RotateWheel(backRightWheelCollider,backRightWheelTransform);
        RotateWheel(frontLeftWheelCollider,frontLeftWheelTransform);
        RotateWheel(backLeftWheelCollider,backLeftWheelTransform);
    }
    void RotateWheel(WheelCollider wheelCollider,Transform transform)
    {
        Vector3 pos;
        Quaternion rot;

        wheelCollider.GetWorldPose(out pos,out rot);

        transform.position = pos;
        transform.rotation = rot;
        
    }
    public float CarSpeed()
    {
       float speed =  rigidbody.linearVelocity.magnitude* 2.23693629f;
       return speed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "TrafficVehicle")
        {
         
          StartCoroutine(GameOverDelay());
            
        }
       
    }
    IEnumerator GameOverDelay()
    {
           GetComponent<CarSound>().PlayHitSound();
           yield return new WaitForSeconds(1f);
             AudioManager.instance.StopLevel01Music();
             GetComponent<CarSound>().StopAllCarSounds();
             uiManager.GameOver();

    }
}