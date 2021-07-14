using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Works in conjunction with the Interactable component. Checks for nearby Interactables and triggers
/// it on a keypress.
/// </summary>
public class Interactor : MonoBehaviour
{
    public float interactionRadius = 2f;
    public KeyCode[] interactActionKeys; // I should really learn how to use the new input system :/

    // maintain a list of Interactables in the scene
    private List<Interactable> interactables = new List<Interactable>();

    // allow Interactable components to register/unregister themselves
    public void Register(Interactable interaction)
    {
        if (!interactables.Contains(interaction))
        {
            interactables.Add(interaction);
        }
    }

    public void Unregister(Interactable interaction)
    {
        interactables.Remove(interaction);
    }

    private void Update()
    {
        // focus closest interaction within radius
        var focused = GetFocused();

        // set focus state on interactables
        foreach (var interaction in interactables)
        {
            interaction.isFocused = interaction == focused;
        }

        // trigger interaction on key up
        if (focused != null && interactActionKeys != null)
        {
            foreach (var keyCode in interactActionKeys)
            {
                if (Input.GetKeyUp(keyCode))
                {
                    focused.OnInteract();
                }
            }
        }
    }

    private Interactable GetFocused()
    {
        // don't focus anything when in a dialogue
        if (GameManager.current.isInDialogue)
        {
            return null;
        }

        var result = null as Interactable;

        // select an interactable to focus
        foreach (var interaction in interactables)
        {
            var toInteraction = interaction.transform.position - transform.position;

            // reject if it's disabled or outside the radius
            if (!interaction.isInteractable ||
                toInteraction.sqrMagnitude > interactionRadius * interactionRadius)
            {
                continue;
            }

            // check if result is null or farther from the player than this one
            if (result == null ||
                (result.transform.position - transform.position).sqrMagnitude > toInteraction.sqrMagnitude)
            {
                result = interaction;
            }
        }

        return result;
    }
}
