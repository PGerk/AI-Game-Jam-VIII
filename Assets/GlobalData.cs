using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "GlobalData", menuName = "Scriptable Objects/GlobalData")]
public class GlobalData : ScriptableObject
{
    public Unit[] currentEnemies;
    public Unit player;
    public List<Sprite> enemies = new();
    public List<Sprite> friendlis = new();
    public List<GameObject> activatedButtons = new();
}
public static class GlobalLoader
{
    private static readonly string dataPath = "Assets/GlobalData.asset";
    public static void Instantiate(ref GlobalData data)
    {
        if (data == null)
        {
            data = AssetDatabase.LoadAssetAtPath<GlobalData>(dataPath);
            if (data == null)
            {
                data = ScriptableObject.CreateInstance<GlobalData>();
                AssetDatabase.CreateAsset(data, dataPath);
                data.enemies = Resources.LoadAll<Sprite>("Assets/Pictures/Enemies").ToList();
                data.friendlis = Resources.LoadAll<Sprite>("Assets/Pictures/Friendly").ToList();
                data.player = new Unit(24, 2, "Player");
                data.player.gamiobj = GameObject.FindWithTag("Player");
                AssetDatabase.SaveAssets();
            }
        }
    }
    public static GlobalData GetGlobalData()
    {
        var data = AssetDatabase.LoadAssetAtPath<GlobalData>(dataPath);

        return data;
    }
}