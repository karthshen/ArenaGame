using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(InvertCollider))]
public class AddInvertedMeshColliderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        InvertCollider script = (InvertCollider)target;
        if (GUILayout.Button("Create Inverted Mesh Collider"))
            script.CreateInvertedMeshCollider();
    }
}
