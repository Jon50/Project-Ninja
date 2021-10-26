using System;
using UnityEditor;
using UnityEngine;

public class FlipObjectNormals : EditorWindow
{
    [SerializeField] private GameObject[] _objects;
    [SerializeField] private Vector2 _UVDivision;

    private static FlipObjectNormals _window;
    private SerializedObject _thisFile = default;
    private SerializedProperty Objects;
    private SerializedProperty UVDivision;

    private Vector2 _scrollPosition;


    [MenuItem("Tools/Mesh Tools")]
    public static void Awake()
    {
        _window = GetWindow<FlipObjectNormals>();
        _window.Show();
        _window.autoRepaintOnSceneChange = true;
    }

    private void OnEnable()
    {
        _thisFile = new SerializedObject(this);
        Objects = _thisFile.FindProperty(nameof(_objects));
        UVDivision = _thisFile.FindProperty(nameof(_UVDivision));
    }

    private void OnGUI()
    {
        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, false, true);

        EditorGUILayout.Separator();

        GUILayout.BeginHorizontal();

        if (GUILayout.Button(new GUIContent { text = "Flip Normals" }))
        {
            PrepareToFlip();
        }

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        if (GUILayout.Button(new GUIContent { text = "Mesh Recalculation and Optimization" }))
        {
            PrepareToRecalculateNormals();
        }

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        if (GUILayout.Button(new GUIContent { text = "Unwrap UVs" }))
        {
            PrepareToUnwrap();
        }

        GUILayout.EndHorizontal();

        EditorGUILayout.PropertyField(UVDivision, new GUIContent { text = "UV Division" });

        EditorGUILayout.PropertyField(Objects, new GUIContent { text = "Game Objects" });

        EditorGUILayout.EndScrollView();

        _thisFile.ApplyModifiedProperties();
    }


    private void PrepareToFlip()
    {
        GameObject theObject = null;

        for (int i = 0; i < Objects.arraySize; i++)
        {
            theObject = Objects.GetArrayElementAtIndex(i).objectReferenceValue as GameObject;
            FlipNormals(theObject);
        }
    }


    private void PrepareToRecalculateNormals()
    {
        GameObject theObject = null;

        for (int i = 0; i < Objects.arraySize; i++)
        {
            theObject = Objects.GetArrayElementAtIndex(i).objectReferenceValue as GameObject;
            RecalculateNormals(theObject);
        }
    }


    private void PrepareToUnwrap()
    {
        GameObject theObject = null;

        for (int i = 0; i < Objects.arraySize; i++)
        {
            theObject = Objects.GetArrayElementAtIndex(i).objectReferenceValue as GameObject;
            UnwrapUVs(theObject);
        }
    }


    private void FlipNormals(GameObject theObject)
    {
        if (theObject == null)
        {
            Debug.Log("No object referenced");
            return;
        }

        Vector3[] normals = theObject.GetComponent<MeshFilter>().sharedMesh.normals;
        for (int i = 0; i < normals.Length; i++)
        {
            normals[i] = -normals[i];
        }
        theObject.GetComponent<MeshFilter>().sharedMesh.normals = normals;

        int[] triangles = theObject.GetComponent<MeshFilter>().sharedMesh.triangles;
        for (int i = 0; i < triangles.Length; i += 3)
        {
            int t = triangles[i];
            triangles[i] = triangles[i + 2];
            triangles[i + 2] = t;
        }

        theObject.GetComponent<MeshFilter>().sharedMesh.triangles = triangles;

        Debug.Log("Normals Fliped");
    }


    private void RecalculateNormals(GameObject theObject)
    {
        if (theObject == null)
        {
            Debug.Log("No object referenced");
            return;
        }

        var mesh = theObject.GetComponent<MeshFilter>().sharedMesh;

        if (mesh == null)
            return;

        Unwrapping.GeneratePerTriangleUV(mesh);
        Unwrapping.GenerateSecondaryUVSet(mesh);

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        mesh.Optimize();
        mesh.OptimizeIndexBuffers();
        mesh.OptimizeReorderVertexBuffer();

        Debug.Log("Mesh Recalculated and Optimized");
    }

    private void UnwrapUVs(GameObject theObject)
    {
        if (theObject == null)
        {
            Debug.Log("No object referenced");
            return;
        }

        var filter = theObject.GetComponent<MeshFilter>();

        if (filter == null)
            return;

        var mesh = filter.sharedMesh;

        if (mesh == null)
            return;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();

        mesh.Optimize();
        mesh.OptimizeIndexBuffers();
        mesh.OptimizeReorderVertexBuffer();

        var settings = new UnwrapParam();
        settings.hardAngle = 88;
        settings.angleError = 8;
        settings.areaError = 15;
        settings.packMargin = 4;

        Unwrapping.GeneratePerTriangleUV(mesh, settings);
        Unwrapping.GenerateSecondaryUVSet(mesh);

        mesh.MarkModified();
    }
}
