#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UI;
#endif
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#if UNITY_EDITOR
[CustomEditor(typeof(DialogueOptionButton), true)]
[CanEditMultipleObjects]
public class DialogueOptionButtonEditor : ButtonEditor
{
    SerializedProperty onSelectProperty;
    SerializedProperty onDeselectProperty;

    protected override void OnEnable()
    {
        base.OnEnable();
        onSelectProperty = serializedObject.FindProperty("onSelect");
        onDeselectProperty = serializedObject.FindProperty("onDeselect");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space();
        serializedObject.Update();
        EditorGUILayout.PropertyField(onSelectProperty);
        EditorGUILayout.PropertyField(onDeselectProperty);
        serializedObject.ApplyModifiedProperties();
    }
}
#endif

// adds public OnSelect/OnDeselect events, and selects itself on mouseover
public class DialogueOptionButton : Button
{
    public UnityEvent onSelect;
    public UnityEvent onDeselect;

    public override void OnSelect(BaseEventData eventData)
    {
        onSelect?.Invoke();
        base.OnSelect(eventData);
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        onDeselect?.Invoke();
        base.OnDeselect(eventData);
    }

    // select on mouseover
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        EventSystem.current.SetSelectedGameObject(gameObject);
    }
}
