using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float motorTorque = 20000;     // Torque máximo do motor
    public float brakeTorque = 2000;      // Torque de frenagem
    public float steeringRange = 30;      // Ângulo máximo de direção
    public float centreOfGravityOffset = -1f; // Deslocamento do centro de massa

    private WheelControl[] wheels;
    private Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        // Ajusta o centro de massa para evitar capotamento
        rigidBody.centerOfMass += Vector3.up * centreOfGravityOffset;

        // Encontra todos os componentes WheelControl nos filhos deste GameObject
        wheels = GetComponentsInChildren<WheelControl>();
    }

    void Update()
    {
        float vInput = Input.GetAxis("Vertical");   // Entrada vertical para acelerar/frear (W/S)
        float hInput = Input.GetAxis("Horizontal"); // Entrada horizontal para direção (A/D)
        bool isBraking = Input.GetKey(KeyCode.Space); // Verifica se o jogador está freando

        foreach (var wheel in wheels)
        {
            // Aplica a direção às rodas direcionáveis com base no input horizontal
            if (wheel.steerable)
            {
                wheel.WheelCollider.steerAngle = hInput * steeringRange;
            }

            if (isBraking)
            {
                // Aplica o torque de frenagem a todas as rodas
                wheel.WheelCollider.brakeTorque = brakeTorque;
                wheel.WheelCollider.motorTorque = 0; // Desativa o torque do motor enquanto freia
            }
            else
            {
                // Aplica o torque do motor nas rodas motorizadas com base no input vertical
                if (wheel.motorized)
                {
                    wheel.WheelCollider.motorTorque = vInput * motorTorque;
                }
                wheel.WheelCollider.brakeTorque = 0; // Desativa o freio enquanto acelera
            }
        }
    }
}
