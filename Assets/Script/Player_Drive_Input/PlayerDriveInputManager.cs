using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EVP;

public class PlayerDriveInputManager : MonoBehaviour
{
    public VehicleController controller;
    public CarControl controller_1;
    public Rigidbody Rb;

    public void UpdateCar(Rigidbody rb, VehicleController controller)
    {
        this.controller = controller;
        Rb = rb;
    }
    public void SetAccelValue(float accel)
    {
        controller.throttleInput = accel;
    }    
    public void SetBrakeValue(float brake)
    {
        controller.brakeInput = brake;
    }    
    public void SetHandbrakeValue(float handbrake)
    {
        controller.handbrakeInput = handbrake;
    }    
    public void SetSteerValue(float steer)
    {
        controller.steerInput = steer;
    }   

    public void SetValue(float steer, float move, float brake, float handbreak, int gear)
    {
        controller.steerInput = steer;
        controller.throttleInput = move;
        controller.brakeInput = brake;
        controller.handbrakeInput = handbreak;
    }
}
