using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(UnityCam))]
public class HUGVRCamEditor : Editor
{

    SerializedProperty _flipImage;
    void OnEnable()
    {
        _flipImage = serializedObject.FindProperty("Flip");
    }


    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(_flipImage);

        serializedObject.ApplyModifiedProperties();
    }
}
