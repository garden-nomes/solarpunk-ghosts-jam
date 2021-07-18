using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomInputModule : StandaloneInputModule
{
    // force the input system to *always* have a selected input
    public override void Process()
    {
        var previouslySelectedGameObject = eventSystem.currentSelectedGameObject;

        base.Process();

        if (eventSystem.currentSelectedGameObject == null &&
            previouslySelectedGameObject != null &&
            previouslySelectedGameObject.activeInHierarchy)
        {
            eventSystem.SetSelectedGameObject(previouslySelectedGameObject);
        }
    }
}
