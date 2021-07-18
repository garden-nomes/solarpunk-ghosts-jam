using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueOptionGroup : MonoBehaviour
{
    private Button[] optionButtons;

    private void Awake()
    {
        // find all child buttons
        optionButtons = GetComponentsInChildren<Button>();

        // wire up cyclical keyboard navigation for buttons
        for (int i = 0; i < optionButtons.Length; i++)
        {
            // get the next and previous index, wrapping
            var nextIndex = (i + 1) % optionButtons.Length;
            var prevIndex = (i + optionButtons.Length - 1) % optionButtons.Length;

            // set explicit up/down/left/right navigation
            optionButtons[i].navigation = new Navigation
            {
                mode = Navigation.Mode.Explicit,
                selectOnUp = optionButtons[prevIndex],
                selectOnLeft = optionButtons[prevIndex],
                selectOnDown = optionButtons[nextIndex],
                selectOnRight = optionButtons[nextIndex],
            };
        }
    }
}
