using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonKeyTrigger : MonoBehaviour
{
    public KeyCode[] keys;

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
    }

    void Update()
    {
        if (button.onClick == null)
        {
            return;
        }

        foreach (var key in keys)
        {
            if (Input.GetKeyUp(key))
            {
                button.onClick.Invoke();
            }
        }
    }
}
