using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Interactable : MonoBehaviour
{
    
    bool canInteract = false;
    private PlayerInputActions playerInputActions;
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }
    private void OnEnable()
    {
        playerInputActions.Player.Interact.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Player.Interact.Disable();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            canInteract = true;
            EnableVisual();
        }

    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            canInteract = false;
            DisableVisual();
        }
    }
    private void Update()
    {
        if (canInteract && playerInputActions.Player.Interact.WasPressedThisFrame()) {
            Interact();
        }
    }

    public virtual void Interact() { }
    public virtual void EnableVisual() { }
    public virtual void DisableVisual() { }
}

