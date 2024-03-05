using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
public class DialogueManager : MonoBehaviour
{
    private DialogueVariables dialogueVariables;
    private PlayerInputActions playerInputActions;
    private static DialogueManager instance;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject[] choices;
    [SerializeField] private TextAsset loadGlobalJSON;
    private TextMeshProUGUI[] choicesText;
    private Story currentStory;
    public bool isPlaying { get; private set; }
    private void OnEnable()
    {
        playerInputActions.Player.Interact.Enable();
    }
    private void OnDisable()
    {
        playerInputActions.Player.Interact.Disable();
    }

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        if (instance != null)
        {
            Debug.Log("Found more than one static dialogue manager");
        }
        dialogueVariables = new DialogueVariables(loadGlobalJSON);
        instance = this;
    }
    void Start()
    {
        isPlaying = false;
        dialoguePanel.SetActive(false);
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
        foreach (KeyValuePair<string,Ink.Runtime.Object> i  in GlobalVariableUpdater.allValues) {
            SetVariableState(i.Key, i.Value);
        }

    }
    public static DialogueManager GetInstance()
    {
        return instance;
    }
    private void Update()
    {
        if (!isPlaying)
        {
            return;
        }
        if (currentStory.currentChoices.Count == 0 && playerInputActions.Player.Interact.WasPressedThisFrame())
        {
            ContinueStory();
        }
    }
    public void EnterDialogue(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        isPlaying = true;
        dialoguePanel.SetActive(true);
        dialogueVariables.StartListening(currentStory);
        currentStory.BindExternalFunction("TradePlayerKey", () => { GameObject.FindGameObjectWithTag("Helper").GetComponent<Transaction>().buyStoryKey(1); });
        currentStory.BindExternalFunction("Save", () => { SaveHandler.GetInstance().Save(); });

    }
    private void endDialogue()
    {
        isPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueVariables.StopListening(currentStory);
        dialogueText.text = "";
        currentStory.UnbindExternalFunction("TradePlayerKey");
        currentStory.UnbindExternalFunction("Save");
    }
    private void ContinueStory()
    {
        transform.GetChild(0).GetComponent<AudioSource>().Play();
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
            DisplayChoices();
        }
        else
        {
            endDialogue();
        }
    }
    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given then the UI can support. Number of chocise given:" + currentChoices.Count);
        }
        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
        StartCoroutine(SelectFirstChoice());
    }
    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }
    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);

    }
    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        if (variableValue == null)
        {
            Debug.LogWarning("Ink Variable DNE" + variableName);
        }
        return variableValue;
    }
    public void SetVariableState(string variableName, Ink.Runtime.Object variableValue)
    {
        if (dialogueVariables.variables.ContainsKey(variableName))
        {
            dialogueVariables.variables.Remove(variableName);
            dialogueVariables.variables.Add(variableName, variableValue);
        }
        else
        {
            Debug.LogWarning("Tried to update variable that wasn't initialized by globals.ink: " + variableName);
        }
    }
}
