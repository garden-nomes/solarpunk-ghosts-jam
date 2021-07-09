using UnityEngine;
using Yarn.Unity;

public class NPC : Interactible
{
    public string dialogueNode = null;

    [Header("Optional")]
    public YarnProgram yarnProgram;

    private DialogueRunner dialogueRunner;

    private void Start()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();

        // gracefully handle null dialogueRunner
        if (dialogueRunner == null)
        {
            Debug.LogError("No DialogueRunner in scene");
            return;
        }

        // load yarn program if supplied
        if (yarnProgram != null)
        {
            dialogueRunner.Add(yarnProgram);
        }
    }

    // disable interaction if no dialogue node set
    public override bool isInteractible => !string.IsNullOrWhiteSpace(dialogueNode);

    override public void OnInteract()
    {
        base.OnInteract();

        // open dialogue
        if (dialogueRunner != null)
        {
            dialogueRunner.StartDialogue(dialogueNode);
        }
    }
}
