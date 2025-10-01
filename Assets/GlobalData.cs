using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "GlobalData", menuName = "Scriptable Objects/GlobalData")]
public class GlobalData : ScriptableObject
{
    public bool debug = true;
    public Unit[] currentEnemies;
    public Unit player;
    public List<Sprite> enemies = new();
    public List<Sprite> friendlis = new();
    public List<Sprite> potraits = new();
    public List<GameObject> activatedButtons = new();
}
public static class GlobalLoader
{
    private static readonly string dataPath = "Assets/GlobalData.asset";
    public static void Instantiate(ref GlobalData data)
    {
        if (data != null && data.debug) Reset(ref data);
        if (data == null)
        {
            data = AssetDatabase.LoadAssetAtPath<GlobalData>(dataPath);
            if (data == null)
            {
                data = ScriptableObject.CreateInstance<GlobalData>();
                AssetDatabase.CreateAsset(data, dataPath);
                data.enemies = Resources.LoadAll<Sprite>("Enemies").ToList();
                data.friendlis = Resources.LoadAll<Sprite>("Friendly").ToList();
                data.potraits = Resources.LoadAll<Sprite>("Potrait").ToList();
                data.player = new Unit(24, 2, "Player");
                data.player.setupInventory();
                data.player.gamiobj = GameObject.FindWithTag("Player");
                EditorUtility.SetDirty(data);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
    }
    public static GlobalData GetGlobalData()
    {
        var data = AssetDatabase.LoadAssetAtPath<GlobalData>(dataPath);
        return data;
    }
    public static void Reset(ref GlobalData dataToDelete)
    {
        AssetDatabase.DeleteAsset(dataPath);
        Instantiate(ref dataToDelete);
    }
}