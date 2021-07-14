using UnityEngine;

public class NPC : MonoBehaviour
{
    public string dialogueNode = null;

    [Header("Optional")]
    public YarnProgram yarnProgram;

    private void Start()
    {
        // load yarn program if supplied
        if (yarnProgram != null && GameManager.current.dialogueRunner != null)
        {
            GameManager.current.dialogueRunner.Add(yarnProgram);
        }
    }

    public void Talk()
    {
        // open dialogue
        if (GameManager.current.dialogueRunner != null)
        {
            GameManager.current.dialogueRunner.StartDialogue(dialogueNode);
        }
    }
}
