using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(UnityCam))]
public class HUGVRCamEditor : Editor
{

    SerializedProperty _flipImage;
    SerializedProperty _isRecording;
    void OnEnable()
    {
        _flipImage = serializedObject.FindProperty("Flip");
        _isRecording = serializedObject.FindProperty("IsRecording");
    }


    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(_flipImage);
        EditorGUILayout.PropertyField(_isRecording);

        serializedObject.ApplyModifiedProperties();
    }
}
