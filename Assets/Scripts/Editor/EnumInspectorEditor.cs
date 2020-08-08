#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MeshGenerator))]
public class EnumInspectorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var enumScript = target as MeshGenerator;

        EditorGUILayout.LabelField("Mesh Data", EditorStyles.boldLabel);
        enumScript.MeshType = (MESH_TYPE)EditorGUILayout.EnumPopup(enumScript.MeshType);

        //EditorGUI.BeginChangeCheck();

        switch (enumScript.MeshType)
        {
            case MESH_TYPE.PLANE:

                EditorGUILayout.LabelField("Lenght");
                enumScript.PlaneData.Lenght = EditorGUILayout.Slider(enumScript.PlaneData.Lenght, 1f, 10f);

                EditorGUILayout.LabelField("Width");
                enumScript.PlaneData.Width = EditorGUILayout.Slider(enumScript.PlaneData.Width, 1f, 10f);

                break;

            case MESH_TYPE.CUBE:
                
                EditorGUILayout.LabelField("Width");
                enumScript.CubeData.Width = EditorGUILayout.Slider(enumScript.CubeData.Width, 0.1f, 10f);
                EditorGUILayout.LabelField("Length");
                enumScript.CubeData.Length = EditorGUILayout.Slider(enumScript.CubeData.Length, 0.1f, 10f);
                EditorGUILayout.LabelField("Height");
                enumScript.CubeData.Height = EditorGUILayout.Slider(enumScript.CubeData.Height, 0.1f, 10f);

                break;

            case MESH_TYPE.SPHERE:

                EditorGUILayout.LabelField("Radius");
                enumScript.SphereData.Radius = EditorGUILayout.Slider(enumScript.SphereData.Radius, 0.1f, 5f);
                EditorGUILayout.LabelField("Latitude");
                enumScript.SphereData.Latitude = EditorGUILayout.IntSlider(enumScript.SphereData.Latitude, 4, 100);
                EditorGUILayout.LabelField("Longitude");
                enumScript.SphereData.Longitude = EditorGUILayout.IntSlider(enumScript.SphereData.Longitude, 1, 100);

                break;

            case MESH_TYPE.CONE:

                EditorGUILayout.LabelField("Height");
                enumScript.ConeData.Height = EditorGUILayout.Slider(enumScript.ConeData.Height, 0.1f, 10f);
                EditorGUILayout.LabelField("TopRadius");
                enumScript.ConeData.TopRadius = EditorGUILayout.Slider(enumScript.ConeData.TopRadius, 0f, 10f);
                EditorGUILayout.LabelField("BottomRadius");
                enumScript.ConeData.BottomRadius = EditorGUILayout.Slider(enumScript.ConeData.BottomRadius, 0f, 10f);
                EditorGUILayout.LabelField("NumberOfSides");
                enumScript.ConeData.NumberOfSides = EditorGUILayout.IntSlider(enumScript.ConeData.NumberOfSides, 8, 28);

                break;
        }

        enumScript.UpdateMesh(enumScript.MeshType);
        //if (EditorGUI.EndChangeCheck())
        //    serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("Reset Values"))
        {
            Debug.Log("Reset values!");
            enumScript.PlaneData.ResetValues();
            enumScript.CubeData.ResetValues();
            enumScript.SphereData.ResetValues();
            enumScript.ConeData.ResetValues();
        }

        base.OnInspectorGUI();
    }
}

#endif