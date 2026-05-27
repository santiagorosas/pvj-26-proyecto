using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AnimatedSquare))]
public class AnimatedSquareEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.Space();

        if (GUILayout.Button("Do Something"))
        {
            Debug.Log("pressed do something");
        }
    }

}