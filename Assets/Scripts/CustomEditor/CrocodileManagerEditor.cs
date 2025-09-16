using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CrocodileManager))]
public class CrocodileManagerEditor : Editor
{
    private SerializedProperty crocodilePrefabProp;
    private SerializedProperty numberOfCrocodilesProp;
    private SerializedProperty barProp;
    private SerializedProperty playerProp;

    private void OnEnable()
    {
        crocodilePrefabProp = serializedObject.FindProperty("_crocodilePrefab");
        numberOfCrocodilesProp = serializedObject.FindProperty("numberOfCrocodilesAtStart");
        barProp = serializedObject.FindProperty("_bar");
        playerProp = serializedObject.FindProperty("player");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        CrocodileManager manager = (CrocodileManager)target;

        EditorGUILayout.LabelField("Configuration", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(crocodilePrefabProp);
        EditorGUILayout.PropertyField(numberOfCrocodilesProp);
        EditorGUILayout.PropertyField(barProp);
        EditorGUILayout.PropertyField(playerProp);

        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("Spawn Points", EditorStyles.boldLabel);
        DrawSpawnPointsList(manager);

        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("Crocodiles", EditorStyles.boldLabel);
        DrawCrocodilesList(manager);

        serializedObject.ApplyModifiedProperties();
    }

    /// <summary>
    /// Draws the custom UI for the Spawn Points list.
    /// </summary>
    private void DrawSpawnPointsList(CrocodileManager manager)
    {
        int indexToRemove = -1;
        for (int i = 0; i < manager.spawnPoints.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            manager.spawnPoints[i] = (Transform)EditorGUILayout.ObjectField(manager.spawnPoints[i], typeof(Transform), true);
            GUI.backgroundColor = new Color(1f, 0.6f, 0.6f);
            if (GUILayout.Button("-", GUILayout.Width(25))) { indexToRemove = i; }
            GUI.backgroundColor = Color.white;
            EditorGUILayout.EndHorizontal();
        }
        if (indexToRemove != -1) { RemoveSpawnPoint(manager, indexToRemove); }
        if (GUILayout.Button("Add New Spawn Point Object")) { CreateNewSpawnPoint(manager); }
    }

    /// <summary>
    /// Draws the new custom UI for the manually spawned Crocodiles list.
    /// </summary>
    private void DrawCrocodilesList(CrocodileManager manager)
    {
        int indexToRemove = -1;
        for (int i = 0; i < manager.spawnedCrocodiles.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            manager.spawnedCrocodiles[i] = (Crocodile)EditorGUILayout.ObjectField(manager.spawnedCrocodiles[i], typeof(Crocodile), true);
            GUI.backgroundColor = new Color(1f, 0.6f, 0.6f);
            if (GUILayout.Button("-", GUILayout.Width(25))) { indexToRemove = i; }
            GUI.backgroundColor = Color.white;
            EditorGUILayout.EndHorizontal();
        }
        if (indexToRemove != -1) { RemoveCrocodile(manager, indexToRemove); }
        if (GUILayout.Button("Manually Add New Crocodile")) { CreateNewCrocodile(manager); }
    }

    /// <summary>
    /// Instantiates a new crocodile at the next available spawn point and adds it to the list.
    /// </summary>
    private void CreateNewCrocodile(CrocodileManager manager)
    {
        if (manager.CrocodilePrefab == null)
        {
            EditorUtility.DisplayDialog("Error", "Please assign the Crocodile Prefab before adding one.", "OK");
            return;
        }

        if (manager.spawnPoints.Count == 0)
        {
            EditorUtility.DisplayDialog("Error", "You must create at least one Spawn Point before adding a crocodile.", "OK");
            return;
        }

        if (manager.spawnedCrocodiles.Count >= manager.spawnPoints.Count)
        {
            EditorUtility.DisplayDialog("Error", "All available spawn points are already in use. Please add more spawn points.", "OK");
            return;
        }

        Transform targetSpawnPoint = manager.spawnPoints[manager.spawnedCrocodiles.Count];

        if (targetSpawnPoint == null)
        {
            EditorUtility.DisplayDialog("Error", $"The Spawn Point at index {manager.spawnedCrocodiles.Count} is missing. Please review your Spawn Points list.", "OK");
            return;
        }

        Transform crocodilesParent = FindOrCreateParent(manager, "Crocodiles");

        GameObject newCrocObject = (GameObject)PrefabUtility.InstantiatePrefab(manager.CrocodilePrefab, crocodilesParent);
        Undo.RegisterCreatedObjectUndo(newCrocObject, "Create Crocodile");

        newCrocObject.transform.position = targetSpawnPoint.position;
        newCrocObject.transform.rotation = targetSpawnPoint.rotation;

        Crocodile crocComponent = newCrocObject.GetComponent<Crocodile>();
        if (crocComponent != null)
        {
            Undo.RecordObject(crocComponent, "Initialize Crocodile");
            crocComponent.Initialize(manager, targetSpawnPoint);

            Undo.RecordObject(manager, "Add Crocodile to List");
            manager.spawnedCrocodiles.Add(crocComponent);

            EditorUtility.SetDirty(crocComponent);
        }
    }

    /// <summary>
    /// Removes a crocodile from the list and its GameObject from the scene.
    /// </summary>
    private void RemoveCrocodile(CrocodileManager manager, int index)
    {
        Undo.RecordObject(manager, "Remove Crocodile");
        Crocodile crocToRemove = manager.spawnedCrocodiles[index];
        manager.spawnedCrocodiles.RemoveAt(index);
        if (crocToRemove != null)
        {
            Undo.DestroyObjectImmediate(crocToRemove.gameObject);
        }
    }

    private Transform FindOrCreateParent(CrocodileManager manager, string parentName)
    {
        Transform parentTransform = manager.transform.Find(parentName);
        if (parentTransform == null)
        {
            GameObject parentObject = new GameObject(parentName);
            parentTransform = parentObject.transform;
            Undo.RegisterCreatedObjectUndo(parentObject, "Create Parent: " + parentName);
            parentTransform.SetParent(manager.transform);
            parentTransform.localPosition = Vector3.zero;
        }
        return parentTransform;
    }

    private void CreateNewSpawnPoint(CrocodileManager manager)
    {
        Transform spawnPointsParent = FindOrCreateParent(manager, "SpawnPoints");
        GameObject newPointObject = new GameObject($"Spawn Point ({manager.spawnPoints.Count})");
        Undo.RegisterCreatedObjectUndo(newPointObject, "Create Spawn Point");
        newPointObject.transform.SetParent(spawnPointsParent);
        newPointObject.transform.position = manager.transform.position;
        Undo.RecordObject(manager, "Add Spawn Point");
        manager.spawnPoints.Add(newPointObject.transform);
    }

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