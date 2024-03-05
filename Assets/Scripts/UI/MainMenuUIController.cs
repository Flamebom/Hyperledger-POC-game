using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class MainMeunUiController : MonoBehaviour

{
    private Button newGameButton;
    private Button resumeGameButton;
    private Button optionsButton;
    public VisualElement mainMenuUI;
    private void Awake()
    {
        mainMenuUI = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Background");
        newGameButton = mainMenuUI.Q<Button>("NewGame");
        resumeGameButton = mainMenuUI.Q<Button>("ResumeGame");
        optionsButton = mainMenuUI.Q<Button>("Options");
        newGameButton.clicked += newGame;
        resumeGameButton.clicked += resumeGame;
        optionsButton.clicked += openOptions;
    
    }
    private void Start()
    {
        GameObject.Find("APICaller").GetComponent<GetTimer>().intialize();
        newGameButton.Focus();
        
    }
    private void newGame() {
        GlobalVariableUpdater.allValues.Clear();
        WebRequestTest.ResetAssetMatrix();
        SceneManager.LoadScene("ConfigureNPC1");
    }
    private void resumeGame() {
        string saveString = SaveSystem.Load();
        if (saveString != null)
        {
            SaveHandler.loading = true;
            SaveObject loadedObject = JsonUtility.FromJson<SaveObject>(saveString);
            SceneManager.LoadScene(loadedObject.scene);
            
        }
            

    }
    private void openOptions() { 
    }



}
