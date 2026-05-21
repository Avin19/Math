using UnityEngine;
using UnityEditor;
using System.IO;

public class LevelJsonToSOImporter : EditorWindow
{
    private TextAsset jsonFile;

    [MenuItem("Tools/Import Level SOs")]
    public static void ShowWindow()
    {
        GetWindow<LevelJsonToSOImporter>("Level SO Importer");
    }

    private void OnGUI()
    {
        GUILayout.Label("JSON To ScriptableObject Importer", EditorStyles.boldLabel);

        jsonFile = (TextAsset)EditorGUILayout.ObjectField(
            "JSON File",
            jsonFile,
            typeof(TextAsset),
            false);

        if (GUILayout.Button("Generate ScriptableObjects"))
        {
            GenerateLevelSOs();
        }
    }

    private void GenerateLevelSOs()
    {
        if (jsonFile == null)
        {
            Debug.LogError("Assign JSON file first.");
            return;
        }

        string json = jsonFile.text;

        // Unity JsonUtility needs wrapper object
        string wrappedJson = "{ \"Levels\": " + json + "}";

        LevelJsonWrapper wrapper =
            JsonUtility.FromJson<LevelJsonWrapper>(wrappedJson);

        if (wrapper == null || wrapper.Levels == null)
        {
            Debug.LogError("Failed to parse JSON.");
            return;
        }

        string folderPath = "Assets/Resources/Levels";

        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            Directory.CreateDirectory(folderPath);
            AssetDatabase.Refresh();
        }

        foreach (var level in wrapper.Levels)
        {
            LevelDataSO levelSO =
                ScriptableObject.CreateInstance<LevelDataSO>();

            levelSO.Level = level.Level;
            levelSO.Question = level.Question;
            levelSO.Answer = level.Answer;
            levelSO.timelimit = level.timelimit;
            levelSO.Category = level.Category;
            levelSO.difficulty = level.difficulty;

            levelSO.unlocked = level.Level == 1;
            levelSO.starEarned = 0;

            string assetPath =
                $"{folderPath}/Level_{level.Level}.asset";

            AssetDatabase.CreateAsset(levelSO, assetPath);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"Generated {wrapper.Levels.Count} Level ScriptableObjects.");
    }
}