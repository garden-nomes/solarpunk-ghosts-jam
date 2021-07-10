using UnityEngine;
using Yarn.Unity;

public class GameManager : Singleton<GameManager>
{
    public DialogueRunner dialogueRunner;

    private void Awake()
    {
        if (dialogueRunner == null)
        {
            dialogueRunner = GameObject.FindObjectOfType<DialogueRunner>();
        }
    }

    public bool isInDialogue => dialogueRunner != null && dialogueRunner.IsDialogueRunning;
}
