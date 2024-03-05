using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class PauseMenuUIController : MonoBehaviour

{
    public static bool isPaused;
    private Button pauseButton;
    private Button optionsButton;
    private Button mainMenuButton;
    PlayerInputActions playerInputActions;
    public VisualElement pauseMenuUI;
    // Start is called before the first frame update
    private void Awake()
    {
        pauseMenuUI = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Background");
        pauseButton = pauseMenuUI.Q<Button>("Resume");
        optionsButton = pauseMenuUI.Q<Button>("Settings");
        mainMenuButton = pauseMenuUI.Q<Button>("MainMenu");
        playerInputActions = new PlayerInputActions();
        pauseButton.clicked += Resume;
        optionsButton.clicked += OpenOptions;
        mainMenuButton.clicked += MainMenu;
    }
    private void OnEnable()
    {
        playerInputActions.Player.Pause.Enable();
    }
    private void OnDisable()
    {
        playerInputActions.Player.Pause.Disable();
    }

    void Update()
    {
            
        if (playerInputActions.Player.Pause.WasPressedThisFrame() ) {
            if (Time.timeScale > 0f)
            {
                Time.timeScale = 0f;
                pauseMenuUI.style.display = DisplayStyle.Flex;
                AudioListener.pause = true;
                isPaused = true;
                pauseButton.Focus();
            }
            else {
                Resume();
            }
        }
    }
    private void Resume()
    {
        Time.timeScale = 1f;
        pauseMenuUI.style.display = DisplayStyle.None;
        AudioListener.pause = false;
        isPaused = false;
    }
    private void OpenOptions() { 
    }
    private void MainMenu() {
        Resume();
        SceneManager.LoadScene("MainMenu");
    }

}
