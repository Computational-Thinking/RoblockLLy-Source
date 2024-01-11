// SCRIPT NOT USED
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMotionController2 : MonoBehaviour
{
    private float horizontaInput;
    private float verticalInput;
    private float steeringAngle;
    
    public WheelCollider frontDriverWheel, frontPassengerWheel;
    public WheelCollider rearDriverWheel, rearPassengerWheel;
    public Transform frontDriverTransform, frontPassengerTransform;
    public Transform rearDriverTransform, rearPassengerTransform;
    public float maxSteerAngle = 30; // Controls the steering radious. How fast can we turn or would we do a u-turn in the robot. (Rotation angle for the wheels).
    public float motorForce = 50; // How fast the robot can go.


    public void GetInput()
    {
        horizontaInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void Steer()
    {
        steeringAngle = maxSteerAngle * horizontaInput;
        frontDriverWheel.steerAngle = steeringAngle;
        frontPassengerWheel.steerAngle = steeringAngle;
    }

    /// <summary>
    /// Applies acceleretion to the wheels in order to Emulate a real wheel 
    /// driving.
    /// </summary>
    private void Accelerate()
    {
        frontDriverWheel.motorTorque = verticalInput * motorForce;
        frontPassengerWheel.motorTorque = verticalInput * motorForce;
        rearDriverWheel.motorTorque = verticalInput * motorForce;
        rearPassengerWheel.motorTorque = verticalInput * motorForce;
    }

    /// <summary>
    /// This method is used to update the rotations and position of the wheels.
    /// It makes possible the wheels look like real wheels spinning.
    /// </summary>
    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontDriverWheel, frontDriverTransform);
        UpdateWheelPose(frontPassengerWheel, frontPassengerTransform);
        UpdateWheelPose(rearDriverWheel, rearDriverTransform);
        UpdateWheelPose(rearPassengerWheel, rearPassengerTransform);
    }

    /// <summary>
    /// This method is an overload of the previous one in orther to configure 
    /// each individual wheel and not duplicate all the code for each wheel in
    /// the previous method.
    /// </summary>
    private void UpdateWheelPose(WheelCollider currentCollider, Transform currentTransform)
    {
        Vector3 currentPosition = currentTransform.position;
        Quaternion currentRotation = currentTransform.rotation;

        currentCollider.GetWorldPose(out currentPosition, out currentRotation);
        currentTransform.position = currentPosition;
        currentTransform.rotation = currentRotation;
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the 
    /// MonoBehaviour is enabled.
    /// </summary>
    private void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
    }
}
