using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using EVP;
using UnityEngine.XR.Content.Interaction;

public class InOutCarControl : MonoBehaviour
{
    [SerializeField]
    private Transform InPlace;
    [SerializeField]
    private Transform OutPlace;
    [SerializeField]
    private Transform Player;
    [SerializeField]
    private XRKnob Steer;
    [SerializeField]
    private XRLever Gear;
    [SerializeField]
    private XRLever HandBrake;
    [SerializeField]
    private bool InCar = false;

    public void Awake()
    {
        transform.GetComponentInParent<VehicleController>().handbrakeInput = 1;
    }

    public void PlayerGetIntoCar()
    {
        Player.SetParent(transform);

        Player.GetComponent<PlayerDriveInputManager>()
            .UpdateCar(transform.GetComponentInParent<Rigidbody>(), transform.GetComponentInParent<VehicleController>());

        Player.GetComponent<CapsuleCollider>().enabled = false;
        Player.GetComponent<CharacterController>().enabled = false;
        Player.GetComponent<ContinuousMoveProviderBase>().enabled = false;

        Player.GetComponent<PlayerInputFromVRController>().enabled = true;
        Player.GetComponent<PlayerDriveInputManager>().enabled = true;

        Steer.onValueChange.AddListener(Player.GetComponent<PlayerInputFromVRController>().HandleSteer);
        Gear.onLeverActivate.AddListener(() => Player.GetComponent<PlayerInputFromVRController>().SetGear(true));
        Gear.onLeverDeactivate.AddListener(() => Player.GetComponent<PlayerInputFromVRController>().SetGear(false));
        HandBrake.onLeverActivate.AddListener(() => Player.GetComponent<PlayerInputFromVRController>().SetHandbrake(true));
        HandBrake.onLeverDeactivate.AddListener(() => Player.GetComponent<PlayerInputFromVRController>().SetHandbrake(false));

        Player.position = InPlace.position;
        Player.rotation = InPlace.rotation;
        Player.localScale = InPlace.localScale;
    }

    public void PlayerGetOutCar()
    {
        Player.SetParent(transform.parent.parent);
        Player.position = OutPlace.position;
        Player.rotation = OutPlace.rotation;
        Player.localScale = Vector3.one;

        Steer.onValueChange.RemoveAllListeners();
        Gear.onLeverActivate.RemoveAllListeners(); ;
        Gear.onLeverDeactivate.RemoveAllListeners(); ;
        HandBrake.onLeverActivate.RemoveAllListeners();
        HandBrake.onLeverDeactivate.RemoveAllListeners();

        Player.GetComponent<PlayerInputFromVRController>().enabled = false;
        Player.GetComponent<PlayerDriveInputManager>().enabled = false;

        Player.GetComponent<CapsuleCollider>().enabled = true;
        Player.GetComponent<CharacterController>().enabled = true;
        Player.GetComponent<ContinuousMoveProviderBase>().enabled = true;

        
    }
}
