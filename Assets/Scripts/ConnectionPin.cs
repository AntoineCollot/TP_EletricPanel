using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ConnectionPin : MonoBehaviour
{
    public bool _isOn;
    public bool IsOn
    {
        get
        {
            if (connectedWire != null)
                return connectedWire.connectedLever.isOn;
            return _isOn;
        }
    }
    [SerializeField] ConnectionWire connectedWire;
    Collider _collider;

    private void Start()
    {
        _collider = GetComponent<Collider>();
    }

    private void Update()
    {
        _collider.enabled = connectedWire == null;
    }

    public void ConnectWire(ConnectionWire wire)
    {
        connectedWire = wire;
    }

    public void ReleaseWire()
    {
        connectedWire = null;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ConnectionPin))]
[CanEditMultipleObjects]
public class InteractionTaskEditor : Editor
{
    SerializedProperty _isOn;
    SerializedProperty connectedWire;

    void OnEnable()
    {
        _isOn = serializedObject.FindProperty("_isOn");
        connectedWire = serializedObject.FindProperty("connectedWire");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        if (connectedWire.objectReferenceValue == null)
            EditorGUILayout.PropertyField(_isOn);
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Connections");
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.PropertyField(connectedWire);
        EditorGUI.EndDisabledGroup();
        serializedObject.ApplyModifiedProperties();
    }
}
#endif