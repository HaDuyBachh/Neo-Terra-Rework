using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerInputFromVRController : MonoBehaviour
{
    [SerializeField]
    private PlayerDriveInputManager _carControl;
    [SerializeField]
    private HandInputValue leftHandInput;
    [SerializeField]
    private HandInputValue rightHandInput;

    private float gear;
    private float brake = 0;
    private float steer = 0;
    private float handbreak = 0;


    void OnEnable()
    {
        leftHandInput.snapTurnEvent.AddListener(SetBrakeValue);
        SetHandbrake(true);
    }

    void OnDisable()
    {
        leftHandInput.snapTurnEvent.RemoveListener(SetBrakeValue);
        SetHandbrake(true);
    }

    public void Update()
    {
        Debug.Log(gear + "   " + brake + "   " + steer + "   " + handbreak);
    }

    public void SetGear(bool status)
    {
        gear = status ? 0.3f : -0.3f;
        _carControl.SetAccelValue(gear);
    }    

    public void SetBrakeValue()
    {
        Debug.Log(leftHandInput.turnValue);
        SetBrakeValue(-leftHandInput.turnValue.y);
    }    

    public void HandleSteer(float value)
    {
        steer = -(value - 0.5f) / 0.5f;
        _carControl.SetSteerValue(steer);
    }

    public void SetHandbrake(bool status)
    {
        Debug.Log("Đang " + (status ? "khóa" : "mở" )+ " phanh tay");
        handbreak = status ? 1 : 0;
        _carControl.SetHandbrakeValue(handbreak);
    }    

    public void SetBrakeValue(float brake)
    {
        this.brake = brake;
        _carControl.SetBrakeValue(brake);
    }    
}
