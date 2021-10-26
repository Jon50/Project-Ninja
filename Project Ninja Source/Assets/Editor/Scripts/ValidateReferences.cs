using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;

// Expand idea
// Use a string field to specify a folder location and get all scripts/types
// Use a button to get all scripts and populate the list for further management

public class ValidateReferences : EditorWindow
{
    [SerializeField] private List<MonoScript> _monoBehaviours = new List<MonoScript>();

    private float _checkTimer = 2f;
    private float _drainRate = 0.005f;
    private const string _path = @"C:\GitHub\Project-Ninja\Project Ninja Source\Assets\ValidateReferences.file";

    private static ValidateReferences _window;
    private SerializedObject _this;
    private SerializedProperty MonoBehaviours;

    [MenuItem("Tools/Check")]
    public static void Awake()
    {
        _window = GetWindow<ValidateReferences>();
        _window.Show();
        _window.autoRepaintOnSceneChange = true;
    }

    private void OnEnable()
    {
        Load();
        _this = new SerializedObject(this);
        MonoBehaviours = _this.FindProperty(nameof(_monoBehaviours));
        EditorApplication.update += Check;
    }

    private void OnDisable()
    {
        EditorApplication.update -= Check;
    }

    private void OnGUI()
    {
        EditorGUILayout.PropertyField(MonoBehaviours, new GUIContent { text = "Components" }, true);
        _this.ApplyModifiedProperties();
        Save();
    }

    private void Save()
    {
        if(_monoBehaviours.IsAnyNull())
            return;

        var formatter = new DataContractSerializer(typeof(List<MonoScript>));
        using(var stream = new FileStream(_path, FileMode.OpenOrCreate))
        {
            formatter.WriteObject(stream, _monoBehaviours);
        }
    }

    private void Load()
    {
        if(!File.Exists(_path))
            return;

        var formatter = new DataContractSerializer(typeof(List<MonoScript>));
        using(var stream = new FileStream(_path, FileMode.Open))
        {
            _monoBehaviours = (List<MonoScript>)formatter.ReadObject(stream);
        }
    }

    public void Check()
    {
        _checkTimer -= _drainRate;
        if(_checkTimer > 0)
            return;

        _checkTimer = 2f;

        if(_monoBehaviours.IsAnyNull() || _monoBehaviours.Count < 1)
            return;

        foreach(var mono in _monoBehaviours)
        {
            var type = Type.GetType(mono.name);
            var objects = FindObjectsOfType(type).ToList();
            objects.ForEach(async ( component ) =>
            {
                SerializedObject serialized = new SerializedObject(component);
                var prop = serialized.GetIterator();

                if(prop.NextVisible(true))
                    do
                    {
                        if(prop.propertyType == SerializedPropertyType.ObjectReference)
                        {
                            if(prop.displayName == "Script")
                                continue;
                            if(prop.objectReferenceValue == null)
                                Debug.Log($"<color=black>=></color> Field: <color=black>=<color=cyan>{prop.displayName}</color>=</color> in: <color=black>=<color=red>{prop.serializedObject.targetObject}</color>=</color>");
                        }
                        await Task.Delay(10);
                    } while(prop.NextVisible(false));
            });
        }
    }
}

public static class ListExtension
{
    public static bool IsAnyNull<T>( this List<T> list )
    {
        if(list == null || list.Count < 1)
            return true;

        foreach(var item in list)
            if(item.Equals(null))
                return true;
        return false;
    }
}
