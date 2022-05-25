using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem instance;
    [SerializeField] GameObject dialogueBox;
    [SerializeField] TextMeshProUGUI textComponent;
    public float textSpeed = 2f;
    private int lineIndex;
    private bool controls;
    public string[] lines;
    
    void Start()
    {
        instance = this;
        lineIndex = 0;
    }

    void Update()
    {
        if (dialogueBox.activeSelf && controls)
        {
            Debug.Log(textComponent.text == lines[lineIndex]);
            if (Player.instance.PlayerAction && textComponent.text == lines[lineIndex])
            {
                NextLine();
            }
        }
    }
    void NextLine()
    {
        Debug.Log(lineIndex + " " + lines.Length);
        if (lineIndex == lines.Length - 1)
        {
            dialogueBox.SetActive(false);
            controls = false;
            
            IEnumerator EnablePlayerControls()
            {
                //yield return new WaitForEndOfFrame();
                yield return new WaitForSeconds(0.1f);
                Player.instance.playerState = Player.PlayerState.Moving;
            }
            StartCoroutine(EnablePlayerControls());           
        } else {
            lineIndex++;
            StartCoroutine(TypeLine());
        }
    }
    IEnumerator TypeLine()
    {
        textComponent.text = "";
        foreach (char c in lines[lineIndex].ToCharArray())
        {
            Debug.Log(textComponent.text);
            yield return new WaitForSeconds(textSpeed);
            textComponent.text += c;
        }
    }
    public void StartDialogue(string[] dialogue)
    {
        lineIndex = 0;
        lines = dialogue;
        Player.instance.playerState = Player.PlayerState.Talking;
        dialogueBox.SetActive(true);
        controls = true;
        StartCoroutine(TypeLine());
        Debug.Log("start");
    }
}
