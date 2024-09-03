﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class HandInputValue : MonoBehaviour
{
    [SerializeField]
    private InputActionProperty activeAction;
    [SerializeField]
    private InputActionProperty selectAction;
    [SerializeField]
    private InputActionProperty turnAction;
    [SerializeField]
    private InputActionProperty primaryButtonAction;
    [SerializeField]
    private InputActionProperty secondaryButtonAction;
 
    public UnityEvent primaryPressEvent;
    public UnityEvent secondaryPressEvent;
    public UnityEvent activePressEvent;
    public UnityEvent selectPressEvent;
    public UnityEvent snapTurnEvent;

    public float triggerValue
    {
        get => activeValue;
    }
    public float gridValue
    {
        get => selectValue;
    }
    public float activeValue
    {
        get => activeAction.action.ReadValue<float>();
    }
    public float selectValue
    {
        get => selectAction.action.ReadValue<float>();
    }
    public Vector2 turnValue
    {
        get => turnAction.action.ReadValue<Vector2>();
    }
    private void OnEnable()
    {
        primaryButtonAction.action.started += PrimaryPress;
        secondaryButtonAction.action.started += SecondaryPress;
        activeAction.action.started += ActivePress;
        turnAction.action.performed += SnapTurnPress;
        turnAction.action.canceled += SnapTurnPress;
    }
    private void OnDisable()
    {
        primaryButtonAction.action.started -= PrimaryPress;
        secondaryButtonAction.action.started -= SecondaryPress;
        activeAction.action.started -= ActivePress;
        turnAction.action.performed -= SnapTurnPress;
    }

    private void PrimaryPress(InputAction.CallbackContext obj)
    {
        primaryPressEvent.Invoke();
    }
    private void SecondaryPress(InputAction.CallbackContext obj)
    {
        secondaryPressEvent.Invoke();
    }
    private void ActivePress(InputAction.CallbackContext obj)
    {
        activePressEvent.Invoke();
    }  
    private void SnapTurnPress(InputAction.CallbackContext obj)
    {
        snapTurnEvent.Invoke();
    }    
}
