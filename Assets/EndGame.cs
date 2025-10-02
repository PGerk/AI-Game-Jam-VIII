using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    private static GlobalData data;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        data = GlobalLoader.GetGlobalData();
    }

    // Update is called once per frame

    public static void resetGame()
    {
        GlobalLoader.Reset(ref data);
        SceneManager.LoadScene(2);
    }
}
