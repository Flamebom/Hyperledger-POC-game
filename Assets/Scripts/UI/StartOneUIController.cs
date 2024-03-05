using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Ink.Runtime;
using System;

public class StartOneUIController : MonoBehaviour
{
    [SerializeField] int NPCIndex;
    private Button nextButton;
    private Label NPC;
    private VisualElement mainUI;
    private DropdownField knowledge1;
    private DropdownField knowledge2;
    private DropdownField knowledge3;
    private DropdownField knowledge4;
    void Awake()
    {
        NPC = GetComponent<UIDocument>().rootVisualElement.Q<Label>("NPC1");
        NPC.text = String.Format("Choose NPC{0}!", NPCIndex);
        mainUI = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Background");
        nextButton = mainUI.Q<Button>("Next");
        knowledge1 = mainUI.Q<DropdownField>("NPC1Knowledge1");
        knowledge2 = mainUI.Q<DropdownField>("NPC1Knowledge2");
        knowledge3 = mainUI.Q<DropdownField>("NPC1Knowledge3");
        knowledge4 = mainUI.Q<DropdownField>("NPC1Knowledge4");
        knowledge1.label = String.Format("NPC{0} Knowledge 1", NPCIndex);
        knowledge2.label = String.Format("NPC{0} Knowledge 2", NPCIndex);
        knowledge3.label = String.Format("NPC{0} Knowledge 3", NPCIndex);
        knowledge4.label = String.Format("NPC{0} Knowledge 4", NPCIndex);
        nextButton.clicked += nextScene;

    }

    void Start()
    {
        knowledge1.Focus();
    }
    private void nextScene()
    {
        GlobalVariableUpdater.allValues.Add(String.Format("npc{0}Knowledge1", NPCIndex), new Ink.Runtime.IntValue(knowledge1.index + 1));
        GlobalVariableUpdater.allValues.Add(String.Format("npc{0}Knowledge2", NPCIndex), new Ink.Runtime.IntValue(knowledge2.index + 1));
        GlobalVariableUpdater.allValues.Add(String.Format("npc{0}Knowledge3", NPCIndex), new Ink.Runtime.IntValue(knowledge3.index + 1));
        GlobalVariableUpdater.allValues.Add(String.Format("npc{0}Knowledge4", NPCIndex), new Ink.Runtime.IntValue(knowledge4.index + 1));
        if (NPCIndex == 3)
        {
            PersistentData.lastPosition = new Vector2(0, 120);
            SceneManager.LoadScene("Level1");
        }
        else {
            SceneManager.LoadScene(String.Format("ConfigureNPC{0}", NPCIndex + 1));
        }
    }
}
