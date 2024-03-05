using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Keybinds : MonoBehaviour
{
    private SaveHandler saver;
    private PlayerInputActions playerInputActions;
    private void Awake()
    { playerInputActions = new PlayerInputActions(); }
    private void Start()
    {
        saver = GetComponent<SaveHandler>();
    }
    private void OnEnable()
    {
        playerInputActions.Player.Save.performed += save;
        playerInputActions.Player.Load.performed += load;
        playerInputActions.Player.Save.Enable();
        playerInputActions.Player.Load.Enable();
    }
    private void OnDisable()
    {
        playerInputActions.Player.Save.Disable();
        playerInputActions.Player.Load.Disable();
    }
    private void save(InputAction.CallbackContext obj) {
        saver.Save();
    }
    private void load(InputAction.CallbackContext obj)
    {
        saver.Load();
    }
}
