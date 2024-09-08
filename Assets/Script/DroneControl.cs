using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneControl : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private List<GameObject> rotor;
    [SerializeField]
    private float rotorSpeed = 2000;
    [SerializeField]
    private float speed = 1.0f;
    [SerializeField]
    private Transform location;
    [SerializeField]
    private Trash ObjectDetect;

    private Rigidbody rigidbody;
    
    
    void Start()
    {
        animator = transform.GetComponentInChildren<Animator>();
        rigidbody = transform.GetComponent<Rigidbody>();
    }

    private void RotorSpin()
    {
        foreach (var r in rotor)
        {
            r.transform.Rotate(0, 0, rotorSpeed * Time.deltaTime);
        }
    }   
    
    private void Movement(Vector3 locate)
    {
        transform.position = Vector3.Slerp(transform.position, locate, speed * Time.deltaTime);
    }    
    
    void Update()
    {
        RotorSpin();

        //if (rigidbody.velocity > 0)
        //{

        //}    
        Movement(location.position);
    }
}
