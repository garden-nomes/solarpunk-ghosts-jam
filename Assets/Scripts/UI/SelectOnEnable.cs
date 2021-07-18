using UnityEngine;
using UnityEngine.EventSystems;

public class SelectOnEnable : MonoBehaviour
{
    private bool waitingForSelect = false;

    private void OnEnable()
    {
        // wait until OnGUI is called due to some kind of Unity bug
        // https://forum.unity.com/threads/highlight-newly-assigned-eventsystem-current-firstselectedgameobject.329830/
        waitingForSelect = true;
    }

    private void OnGUI()
    {
        if (waitingForSelect)
        {
            waitingForSelect = false;
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
    }
}
