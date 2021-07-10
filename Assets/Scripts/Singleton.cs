using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // note: a lot of singleton tutorials use "instance", but I find "current" more readable
    protected static T _current;
    public static T current
    {
        get
        {
            if (_current == null)
            {
                _current = GameObject.FindObjectOfType<T>();

                if (_current == null)
                {
                    Debug.LogError($"No {typeof(T)} found in the current scene");
                }
            }

            return _current;
        }
    }

    protected void OnDisable()
    {
        if (_current == this)
        {
            _current = null;
        }
    }
}
