using System;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentBreakForce;
    private bool isBreaking;
    private bool isHandBrake;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;

    [SerializeField] private Light headLightLeft;
    [SerializeField] private Light headLightRight;

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float handBrakeForce;
    [SerializeField] private float maxSteeringAngle;

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        RestartPosition();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);
        isHandBrake = Input.GetKey(KeyCode.LeftShift);

        if (Input.GetKeyDown(KeyCode.L))
        {
            ToggleHeadlights();
        }
    }

    private void HandleMotor()
    {
        if (isBreaking)
        {
            frontLeftWheelCollider.motorTorque = 0;
            frontRightWheelCollider.motorTorque = 0;
            rearLeftWheelCollider.motorTorque = 0;
            rearRightWheelCollider.motorTorque = 0;
        }
        else
        {
            frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
            frontRightWheelCollider.motorTorque = verticalInput * motorForce;
            rearLeftWheelCollider.motorTorque = verticalInput * motorForce;
            rearRightWheelCollider.motorTorque = verticalInput * motorForce;
        }

        currentBreakForce = isBreaking ? breakForce : (isHandBrake ? handBrakeForce : 0f);
        ApplyBrakes();
    }


    private void ApplyBrakes()
    {

        frontLeftWheelCollider.brakeTorque = currentBreakForce;
        frontRightWheelCollider.brakeTorque = currentBreakForce;
        rearLeftWheelCollider.brakeTorque = currentBreakForce;
        rearRightWheelCollider.brakeTorque = currentBreakForce;
    }


    private void HandleSteering()
    {
        currentSteerAngle = maxSteeringAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheelCollider(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheelCollider(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheelCollider(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheelCollider(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheelCollider(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

    private void RestartPosition()
    {
        if (Input.GetKey("r"))
        {
            Debug.Log("RestartPosition");
            transform.position = new Vector3(3f, transform.position.y, transform.position.z);
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    private void ToggleHeadlights()
    {
        if (headLightLeft != null && headLightRight != null)
        {
            bool currentState = headLightLeft.enabled;
            headLightLeft.enabled = !currentState;
            headLightRight.enabled = !currentState;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(frontLeftWheelTransform.position, frontRightWheelTransform.position);
        Gizmos.DrawLine(rearLeftWheelTransform.position, rearRightWheelTransform.position);
    }
}
