using UnityEngine;

public class Talk : MonoBehaviour
{
    [SerializeField] string[] dialogue;

    private bool onTrigger;
    void Update()
    {
        if (onTrigger
        && Player.instance.playerState == Player.PlayerState.Moving 
        && Player.instance.PlayerAction)
        {
            DialogueSystem.instance.StartDialogue(dialogue);
        }
    }

    void OnTriggerEnter2D()
    {
        onTrigger = true;
    }
    void OnTriggerExit2D()
    {
        onTrigger = false;
    }
}
