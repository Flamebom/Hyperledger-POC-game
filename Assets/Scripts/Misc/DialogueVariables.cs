
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;


public class DialogueVariables 
{
    public Dictionary<string, Ink.Runtime.Object> variables { get; private set; }
    public DialogueVariables(TextAsset loadGlobalsJson) {
        Story globalVariablesStory = new Story(loadGlobalsJson.text);
        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach (string name in globalVariablesStory.variablesState) {
            Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
        }
    }
    private void VariableChanged(string name, Ink.Runtime.Object value) {
        if (variables.ContainsKey(name)) {
            variables.Remove(name);
            variables.Add(name, value);
        }
    }
    public void StartListening(Story story) {
        UpdateGemCount(PlayerReference.player.GetComponent<PlayerStats>().gems);
        VariablesToStory(story);
        
        story.variablesState.variableChangedEvent+= VariableChanged;
    }
    public void StopListening(Story story) {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }
    private void VariablesToStory(Story story) {
        foreach (KeyValuePair<string, Ink.Runtime.Object> variable in variables) {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }
    private void UpdateGemCount(int gems) {
        variables.Remove("gemCount");
        variables.Add("gemCount",new Ink.Runtime.IntValue (gems));
    }

}
