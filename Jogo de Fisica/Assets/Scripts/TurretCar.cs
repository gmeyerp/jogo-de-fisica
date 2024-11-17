using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class TurretCar : TurretBlock
{
    [SerializeField] float motorTorque = 20000;     // Torque máximo do motor
    [SerializeField] float brakeTorque = 20000;     // Torque máximo do motor
    [SerializeField] float steeringRange = 30;      // Ângulo máximo de direção
    [SerializeField] float centreOfGravityOffset = -1f; // Deslocamento do centro de massa

    [SerializeField] WheelScript[] wheels;

    // Start is called before the first frame update
    void Start()
    {
        wheels = GetComponentsInChildren<WheelScript>();
    }
    public override void Move(Vector3 force, float maxSpeed)
    {
        Debug.Log(force);
        foreach (WheelScript wheel in wheels)
        {
            if (wheel.isSteer)
            {
                wheel.wheelCollider.steerAngle = force.x * 6;
            }                       

            if (force.z == 0 && wheel.isMotor)
            {
                wheel.wheelCollider.brakeTorque = brakeTorque;
                wheel.wheelCollider.motorTorque = 0;
            }
            else if (wheel.isMotor && rigidbody.velocity.magnitude < maxSpeed)
            {
                wheel.wheelCollider.motorTorque = force.z * motorTorque;
                wheel.wheelCollider.brakeTorque = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        if (rider == null)
        {
            foreach (WheelScript wheel in wheels)
            {
                if (wheel.isMotor)
                {
                    wheel.wheelCollider.motorTorque = 0;
                    wheel.wheelCollider.brakeTorque = 0;
                }                
            }           
        }
    }
}
