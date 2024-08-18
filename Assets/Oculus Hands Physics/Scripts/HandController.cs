using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR;

public class HandController : MonoBehaviour
{
    public InputDeviceCharacteristics controllerCharacteristics;
    private InputDevice targetDevice;
    public Hand hand;
    public enum ButtonType { TriggerButton, GripButton, AOrXButton, YOrBButton };
    public ButtonType GripControl;
    public ButtonType TriggerControl;
    public ButtonType ThumbControl;
    public float triggerValuePublic;
    // Start is called before the first frame update
    void Start()
    {
        TryInitialize();
    }

    void TryInitialize()
    {
        List<InputDevice> devices = new List<InputDevice>();

        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);
        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }
    }
    void UpdateHandAnimation()
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            if (TriggerControl == ButtonType.TriggerButton)
            {
                hand.SetIndex(triggerValue);
                triggerValuePublic = triggerValue;
            }
            else if (GripControl == ButtonType.TriggerButton)
            {
                hand.SetGrip(triggerValue);
            }
            else if (ThumbControl == ButtonType.TriggerButton)
            {
                hand.SetThumb(triggerValue);
            }
            // else
            // {
            //     hand.SetIndex(0);
            // }
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            if (TriggerControl == ButtonType.GripButton)
            {
                hand.SetIndex(gripValue);
            }
            else if (GripControl == ButtonType.GripButton)
            {
                hand.SetGrip(gripValue);
            }
            else if (ThumbControl == ButtonType.GripButton)
            {
                hand.SetThumb(gripValue);
            }
        }
        // else
        // {
        //     hand.SetGrip(0);
        // }
        if (targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue))
        {
            if (TriggerControl == ButtonType.AOrXButton)
            {
                hand.SetIndex(primaryButtonValue ? 1 : 0);
            }
            else if (GripControl == ButtonType.AOrXButton)
            {
                hand.SetGrip(primaryButtonValue ? 1 : 0);
            }
            else if (ThumbControl == ButtonType.AOrXButton)
            {
                hand.SetThumb(primaryButtonValue ? 1 : 0);
            }
        }
        // else
        // {
        //     hand.SetThumb(0);
        // }
        if (targetDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryButtonValue))
        {
            if (TriggerControl == ButtonType.YOrBButton)
            {
                hand.SetIndex(secondaryButtonValue ? 1 : 0);
            }
            else if (GripControl == ButtonType.YOrBButton)
            {
                hand.SetGrip(secondaryButtonValue ? 1 : 0);
            }
            else if (ThumbControl == ButtonType.YOrBButton)
            {
                hand.SetThumb(secondaryButtonValue ? 1 : 0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetDevice.isValid)
        {
            TryInitialize();
        }
        else
        {
            UpdateHandAnimation();
        }
    }
}
