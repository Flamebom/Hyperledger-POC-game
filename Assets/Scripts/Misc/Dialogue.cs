using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using TMPro;
public class Dialougue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    private PlayerInputActions playerInputActions;
    bool interacting;
    private float interactCD = 0f;

    private int index;
    // Start is called before the first frame update
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        interactCD = Time.time;

    }

    private void OnEnable()
    {
        playerInputActions.Player.Interact.started += interact;
        playerInputActions.Player.Interact.canceled += stopinteract;
        playerInputActions.Player.Interact.Enable();


    }
    private void OnDisable()
    {
        playerInputActions.Player.Interact.Disable();
    }

    private void stopinteract(InputAction.CallbackContext obj)
    {
        interacting = false;

    }

    private void interact(InputAction.CallbackContext obj)
    {
        interacting = true;

    }

    void Start()
    {
        gameObject.SetActive(false);
        textComponent.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        if (interacting&& Time.time>interactCD+0.1f && gameObject.activeSelf)
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else { StopAllCoroutines();
                textComponent.text = lines[index];
            }
            interactCD = Time.time;
        }
    }
    public void StartDialogue(string[] newlines)
    {
        gameObject.SetActive(true);
        lines =(string[]) newlines.Clone();
        index = 0;
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
