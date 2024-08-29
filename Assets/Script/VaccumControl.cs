using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VaccumControl : MonoBehaviour
{
    [SerializeField]
    private Material m_material;
    [SerializeField]
    private Texture[] pin;
    private bool _isActive = false;
    [SerializeField]
    private bool _isGrab = false;
    [SerializeField]
    private SuctionScript suction;
    [SerializeField]
    private List<Transform> objList;
    [SerializeField]
    private XRGrabInteractable XRGrab;

    /// <summary>
    /// Mode 0: suction <br></br>
    /// Mode 1: blow
    /// </summary>
    [SerializeField]
    private int Mode = 0;
    private int ModeCount = 2;

    public void Start()
    {
        suction.vaccumm = this;
    }
    public void IsGrab()
    {
        _isGrab = true;
        //XRGrab.firstInteractorSelecting.transform.GetComponentInChildren<HandInputValue>().secondaryPressEvent.AddListener(ChangeMode);
    }
    public void IsRelease()
    {
        _isGrab = false;
        //XRGrab.firstInteractorSelecting.transform.GetComponentInChildren<HandInputValue>().secondaryPressEvent.RemoveListener(ChangeMode);
        if (objList.Count > 0) RemoveObj(objList[0]);
    }

    public void ChangeMode()
    {
        Mode = (Mode + 1) % ModeCount;
    }
    public void AddObj(Transform obj)
    {
        if (objList.Count == 0)
        {
            obj.GetComponentInChildren<Collider>().enabled = false;
            obj.GetComponent<Rigidbody>().useGravity = false;
            obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
            obj.GetComponentInChildren<Trash>().enabled = false;

            objList.Add(obj);
        }
    }

    public void ShotObj(Transform obj)
    {
        obj.GetComponentInChildren<Collider>().enabled = true;
        obj.GetComponent<Rigidbody>().useGravity = true;
        obj.GetComponent<Rigidbody>().AddForce(transform.up * 80.0f, ForceMode.Force);
        obj.GetComponentInChildren<Trash>().enabled = false;

        objList.Remove(obj);
    }

    public void RemoveObj(Transform obj)
    {
        obj.GetComponentInChildren<Collider>().enabled = true;
        obj.GetComponent<Rigidbody>().useGravity = true;
        obj.GetComponentInChildren<Trash>().enabled = false;

        objList.Remove(obj);
    }

    public void Active(bool _state)
    {
        if (Mode == 0)
        {
            suction.gameObject.SetActive(_state);
            if (!_state && objList.Count > 0) ChangeMode(); 
        }
        else
        if (Mode == 1)
        {
            if (_state) ShotObj(objList[0]);
            ChangeMode();
        }
    }

    public void Update()
    {
        if (objList.Count > 0)
        {
            for (int i = 0; i < objList.Count; i++)
            {
                objList[i].position =
                    Vector3.Slerp(objList[i].position, suction.transform.position, 10.0f * Time.deltaTime);
            }
        }
    }
}
