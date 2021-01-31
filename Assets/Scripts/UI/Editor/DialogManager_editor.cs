using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(DialogManager))]
public class DialogManager_editor : Editor {
    private SerializedProperty _property;
    private SerializedProperty _display;
    private ReorderableList _list;

    private void OnEnable() {
        _property = serializedObject.FindProperty("dialogs");
        _display = serializedObject.FindProperty("display");
        _list = new ReorderableList(serializedObject, _property, true, true, true, true) {
            drawHeaderCallback = DrawListHeader,
            drawElementCallback = DrawListElement,
            elementHeightCallback = ElementHeightCallback
        };
    }

    private void DrawListHeader(Rect rect) {
        GUI.Label(rect, "Dialogs:");
    }

    private void DrawListElement(Rect rect, int index, bool isActive, bool isFocused) {
        var item = _property.GetArrayElementAtIndex(index);
        rect.x += 10;
        rect.width -= 10;
        EditorGUI.PropertyField(rect, item,true);
    }
    private float ElementHeightCallback(int index){
        //Gets the height of the element. This also accounts for properties that can be expanded, like structs.
        float propertyHeight = EditorGUI.GetPropertyHeight(_property.GetArrayElementAtIndex(index), true);

        float spacing = EditorGUIUtility.singleLineHeight / 2;

        return propertyHeight + spacing;
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();
        EditorGUILayout.PropertyField(_display,new GUIContent("Text Display"));
        EditorGUILayout.Space();
        _list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}