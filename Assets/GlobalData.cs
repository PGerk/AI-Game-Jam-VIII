using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "GlobalData", menuName = "Scriptable Objects/GlobalData")]
public class GlobalData : ScriptableObject
{
    public Unit[] currentEnemies;
    public Unit player;
    public Sprite[] enemies;
    public Sprite[] friendlis;
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
                data.enemies = Resources.LoadAll<Sprite>("Assets/Pictures/Enemies");
                data.friendlis = Resources.LoadAll<Sprite>("Assets/Pictures/Friendly");
                data.player = new Unit(24, 2, "Player");
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