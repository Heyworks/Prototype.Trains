using UnityEngine;
using UnityEditor;
using UnityEditor.UI;

[CanEditMultipleObjects, CustomEditor(typeof(NonDrawingGraphic), true)]
public class NonDrawingGraphicEditor : GraphicEditor
{
    public override void OnInspectorGUI()
    {
        base.serializedObject.Update();
        GUI.enabled = false;
        EditorGUILayout.PropertyField(base.m_Script);
        GUI.enabled = true;
        base.RaycastControlsGUI();
        base.serializedObject.ApplyModifiedProperties();
    }
}