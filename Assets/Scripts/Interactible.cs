using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Works in conjunction with the Interactor behaviour. Allows the player character to interact
/// with a GameObject when nearby. Can be inherited from or used by itself.
/// </summary>
public class Interactible : MonoBehaviour
{
    [Header("Interaction Events")]
    public UnityEvent onInteract;

    // focus events to allow for highlighting or whatever
    public UnityEvent onFocus;
    public UnityEvent onBlur; // in web programming an input is "blurred" when it loses focus

    // can be overridden by child classes
    public virtual bool isInteractible => true;

    public virtual void OnInteract()
    {
        onInteract.Invoke();
    }

    // trigger onFocus/onBlur when isFocused is flipped
    private bool _isFocused = false;
    public bool isFocused
    {
        get => _isFocused;
        set
        {
            if (value && !_isFocused)
            {
                onFocus.Invoke();
            }
            else if (!value && _isFocused)
            {
                onBlur.Invoke();
            }

            _isFocused = value;
        }
    }

    // register/unregister with the Interactor behaviour

    protected void OnEnable()
    {
        foreach (var interactor in GameObject.FindObjectsOfType<Interactor>())
        {
            interactor.Register(this);
        }
    }

    protected void OnDisable()
    {
        foreach (var interactor in GameObject.FindObjectsOfType<Interactor>())
        {
            interactor.Unregister(this);
        }

        if (_isFocused)
        {
            onBlur.Invoke();
            _isFocused = false;
        }
    }
}
