using UnityEngine;
using UnityEngine.SceneManagement;

public class levelLoader : MonoBehaviour
{
    public void OnTriggerEnter()
    {
        SceneManager.LoadScene(2);
    }
}
