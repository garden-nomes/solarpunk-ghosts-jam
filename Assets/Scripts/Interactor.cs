using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Works in conjunction with the Interactible component. Checks for nearby Interactibles and triggers
/// it on a keypress.
/// </summary>
public class Interactor : MonoBehaviour
{
    public float interactionRadius = 2f;
    public KeyCode[] interactActionKeys; // I should really learn how to use the new input system :/

    // maintain a list of Interactibles in the scene
    private List<Interactible> interactibles;

    private void Awake()
    {
        interactibles = new List<Interactible>();
    }

    // allow Interactible components to register/unregister themselves

    public void Register(Interactible interaction)
    {
        if (!interactibles.Contains(interaction))
        {
            interactibles.Add(interaction);
        }
    }

    public void Unregister(Interactible interaction)
    {
        interactibles.Remove(interaction);
    }

    private void Update()
    {
        // focus closest interaction within radius
        var focused = GetFocused();

        // set focus state on interactibles
        foreach (var interaction in interactibles)
        {
            interaction.isFocused = interaction == focused;
        }

        // trigger interaction on key up
        if (interactActionKeys != null && focused != null)
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

    private Interactible GetFocused()
    {
        var result = null as Interactible;

        // select an interactible to focus
        foreach (var interaction in interactibles)
        {
            var toInteraction = interaction.transform.position - transform.position;

            // reject if it's disabled or outside the radius
            if (!interaction.isInteractible ||
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
