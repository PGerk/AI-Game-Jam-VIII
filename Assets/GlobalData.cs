using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "GlobalData", menuName = "Scriptable Objects/GlobalData")]
public class GlobalData : ScriptableObject
{
    public bool debug = false;
    public Unit[] currentEnemies;
    public Unit player;
    public string sceneType;
    public List<Sprite> enemies = new();
    public List<Sprite> friendlis = new();
    public List<Sprite> potraits = new();
    public List<string> activatedButtons = new();
    public bool firstFight = true;
}
public static class GlobalLoader
{
    private static readonly string dataPath = "Assets/GlobalData.asset";
    public static void Instantiate()
    {
            GlobalData data = AssetDatabase.LoadAssetAtPath<GlobalData>(dataPath);
            if (data == null)
            {
                data = ScriptableObject.CreateInstance<GlobalData>();
                AssetDatabase.CreateAsset(data, dataPath);
                data.enemies = Resources.LoadAll<Sprite>("Enemies").ToList();
                data.friendlis = Resources.LoadAll<Sprite>("Friendly").ToList();
                data.potraits = Resources.LoadAll<Sprite>("Potrait").ToList();
                data.player = new Unit(24, 2, "Player");
                data.player.setupInventory();
                EditorUtility.SetDirty(data);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
    }
    public static GlobalData GetGlobalData()
    {
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        var data = AssetDatabase.LoadAssetAtPath<GlobalData>(dataPath);
        return data;
    }
    public static void Reset(ref GlobalData dataToDelete)
    {
        AssetDatabase.DeleteAsset(dataPath);
        Instantiate();
    }
}