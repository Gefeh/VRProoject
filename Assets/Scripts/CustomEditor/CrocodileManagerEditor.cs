using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CrocodileManager))]
public class CrocodileManagerEditor : Editor
{
    private SerializedProperty crocodilePrefabProp;
    private SerializedProperty numberOfCrocodilesProp;
    private SerializedProperty spawnPointsProp;

    /// <summary>
    /// This is called when the editor for this component is enabled.
    /// </summary>
    private void OnEnable()
    {
        crocodilePrefabProp = serializedObject.FindProperty("crocodilePrefab");
        numberOfCrocodilesProp = serializedObject.FindProperty("numberOfCrocodiles");
        spawnPointsProp = serializedObject.FindProperty("spawnPoints");
    }

    /// <summary>
    /// This method is called by Unity to draw the custom UI in the Inspector.
    /// </summary>
    public override void OnInspectorGUI()
    {
        serializedObject.ApplyModifiedProperties();
        CrocodileManager manager = (CrocodileManager)target;
        EditorGUILayout.PropertyField(crocodilePrefabProp);
        EditorGUILayout.PropertyField(numberOfCrocodilesProp);

        EditorGUILayout.Space(10);
        
        EditorGUILayout.LabelField("Spawn Points", EditorStyles.boldLabel);

        int indexToRemove = -1;

        for (int i = 0; i < manager.spawnPoints.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();

            manager.spawnPoints[i] = (Transform)EditorGUILayout.ObjectField(manager.spawnPoints[i], typeof(Transform), true);

            GUI.backgroundColor = new Color(1f, 0.6f, 0.6f);
            if (GUILayout.Button("-", GUILayout.Width(25)))
            {
                indexToRemove = i;
            }
            GUI.backgroundColor = Color.white;

            EditorGUILayout.EndHorizontal();
        }

        if (indexToRemove != -1)
        {
            RemoveSpawnPoint(manager, indexToRemove);
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Add New Spawn Point Object"))
        {
            CreateNewSpawnPoint(manager);
        }

        serializedObject.ApplyModifiedProperties();
    }

    /// <summary>
    /// Removes a spawn point from the list and safely destroys its GameObject.
    /// </summary>
    private void RemoveSpawnPoint(CrocodileManager manager, int index)
    {
        Undo.RecordObject(manager, "Remove Spawn Point");

        Transform pointToRemove = manager.spawnPoints[index];

        manager.spawnPoints.RemoveAt(index);

        if (pointToRemove != null)
        {
            Undo.DestroyObjectImmediate(pointToRemove.gameObject);
        }
    }

    private void CreateNewSpawnPoint(CrocodileManager manager)
    {
        string parentName = "SpawnPoints";
        Transform spawnPointsParent = manager.transform.Find(parentName);
        if (spawnPointsParent == null)
        {
            GameObject parentObject = new GameObject(parentName);
            spawnPointsParent = parentObject.transform;
            Undo.RegisterCreatedObjectUndo(parentObject, "Create SpawnPoints Parent");
            spawnPointsParent.SetParent(manager.transform);
            spawnPointsParent.localPosition = Vector3.zero;
        }

        GameObject newPointObject = new GameObject($"Spawn Point ({manager.spawnPoints.Count})");
        Undo.RegisterCreatedObjectUndo(newPointObject, "Create Spawn Point");
        newPointObject.transform.SetParent(spawnPointsParent);
        newPointObject.transform.position = manager.transform.position;

        Undo.RecordObject(manager, "Add Spawn Point");
        manager.spawnPoints.Add(newPointObject.transform);
    }

    void OnSceneGUI()
    {
        CrocodileManager manager = (CrocodileManager)target;
        Handles.color = Color.white;
        Vector3 buttonPosition = manager.transform.position + Vector3.up * 2f;
        float buttonSize = 0.5f;
        if (Handles.Button(buttonPosition, Quaternion.identity, buttonSize, buttonSize, Handles.CubeHandleCap))
        {
            CreateNewSpawnPoint(manager);
        }
    }
}