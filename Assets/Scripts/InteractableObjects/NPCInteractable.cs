using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : Interactable
{
    [SerializeField]  private TextAsset inkJSON;
    private SpriteRenderer sprite;
    private SpriteRenderer visual;
    public NPCScriptableObject nPCScriptableObject;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = nPCScriptableObject.sprite;
        visual = transform.GetChild(0).GetComponent<SpriteRenderer>();
        visual.enabled = false;

    }
    public override void EnableVisual()
    {
        visual.enabled = true;
    }
    public override void DisableVisual()
    {
        visual.enabled = false;
    }
    public override void Interact()
    {
        if (!DialogueManager.GetInstance().isPlaying)
        {
            DialogueManager.GetInstance().EnterDialogue(inkJSON);
            visual.enabled = false;
        }
    }

}
