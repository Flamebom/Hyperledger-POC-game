using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class WinScreenUIController : MonoBehaviour

{
    private Button mainMenuButton;
    public VisualElement WinUI;
    private void Awake()
    {
        WinUI = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Background");
        mainMenuButton = WinUI.Q<Button>("MainMenu");
        mainMenuButton.clicked += mainMenu;


    }
    private void Start()
    {
        mainMenuButton.Focus();

    }
    private void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
  


}
