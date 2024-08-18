using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;

public class InputDevicesManagement : MonoBehaviour
{
    private static InputDevicesManagement instance;
    public static InputDevicesManagement Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InputDevicesManagement>();
                if (instance == null)
                {
                    Debug.LogError("There is no InputDevicesManagement object in the scene.");
                }
            }
            return instance;
        }
    }

    public List<InputDevice> inputDevices;
    
    void Awake()
    {
        inputDevices = new List<InputDevice>();
        InputDevices.GetDevices(inputDevices);
    }

    public bool IsTriggerPressed()
    {
        foreach (InputDevice device in inputDevices)
        {
            if (triggerValue >= 0.01f)
            {   
                return true;
            }
        }
        return false;
    }
    public GameObject rightHandGO;
    public GameObject leftHandGO;
    public float triggerValue;
    public float GetTriggerValue()
    {
        InputDevices.GetDevices(inputDevices);
        float leftTrigger = 0f;
        float rightTrigger = 0f;
        leftTrigger = leftHandGO.GetComponent<HandController>().triggerValuePublic;
        rightTrigger = rightHandGO.GetComponent<HandController>().triggerValuePublic;
        
        if (leftTrigger > rightTrigger)
        {
            return leftTrigger;
        }
        else if (rightTrigger > leftTrigger)
        {
            return rightTrigger;
        }
        else if (leftTrigger != 0f)
        {
            return leftTrigger;
        }
        else
        {
            //TODO Tay phải đang bị không có giá trị
            return 0f;
        }
    }
    public bool isTriggerPressed;
    void Update(){
        isTriggerPressed = InputDevicesManagement.Instance.IsTriggerPressed();
        triggerValue = GetTriggerValue();
    }
}
