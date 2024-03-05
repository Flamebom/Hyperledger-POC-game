using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class InventorySlot 
{
    public ItemScriptableObject item;
    public Button button;
    public InventorySlot(ItemScriptableObject item, VisualTreeAsset template) {
        TemplateContainer itemButtonContainer = template.Instantiate();
        this.item = item;
        button = itemButtonContainer.Q<Button>();
        button.style.backgroundImage = new StyleBackground(item.sprite);
        button.RegisterCallback<ClickEvent>(OnClick);
    }
    public void OnClick(ClickEvent evt) {
   
       PlayerInventoryUIController pUIController = GameObject.FindGameObjectWithTag("PlayerInventoryUI").GetComponent<PlayerInventoryUIController>();
        pUIController.ItemName.text = item.name;
        pUIController.ItemDescription.text = item.description;
    }
}
