using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerInventoryUIController : MonoBehaviour
{
    public VisualTreeAsset itemButtonTemplate;
    public VisualElement PlayerInventoryGUI;
    public VisualElement Inventory;
    public VisualElement InventoryDescription;
    public PlayerStats playerStats;
    public Label ItemName;
    public Label gemNumber;
    public Label keyNumber;
    public Label ItemDescription;
    private PlayerInventory playerInventory;
    public int selectionIndex;
    private PlayerInputActions playerInputActions;
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();


    }
    private void OnEnable()
    {
        playerInputActions.Player.Inventory.started += open;
        playerInputActions.Player.Inventory.canceled += close;
        playerInputActions.Player.Inventory.Enable();
    }

    private void close(InputAction.CallbackContext obj)
    {
        PlayerInventoryGUI.style.display = DisplayStyle.None;
    }

    private void open(InputAction.CallbackContext obj)
    {
        PlayerInventoryGUI.style.display = DisplayStyle.Flex;
    }

    private void OnDisable()
    {
        playerInputActions.Player.Inventory.Disable();
    }
    void Start()
    {
        playerStats = PlayerReference.player.GetComponent<PlayerStats>();
        playerInventory = PlayerReference.player.GetComponent<PlayerInventory>();
        var PlayerInventory = GetComponent<UIDocument>().rootVisualElement;
        PlayerInventoryGUI = PlayerInventory.Q<VisualElement>("Background");
        Inventory = PlayerInventory.Q<VisualElement>("Inventory");
        ItemName = PlayerInventory.Q<Label>("ItemName");
        ItemDescription = PlayerInventory.Q<Label>("ItemDescription");
        InventoryDescription = PlayerInventory.Q<VisualElement>("InventoryDescription");
        keyNumber = PlayerInventory.Q<Label>("KeyNumber");
        gemNumber = PlayerInventory.Q<Label>("GemNumber");



    }
    private void Update()
    {
        gemNumber.text = playerStats.gems.ToString();
        keyNumber.text = playerStats.keys.ToString();
    }
    public void addToInventory(int inventroySlotItem) 
    {
        InventorySlot newSlot = new InventorySlot(playerInventory.PassiveInventory[inventroySlotItem], itemButtonTemplate);
        Inventory.Add(newSlot.button);
    }
}
