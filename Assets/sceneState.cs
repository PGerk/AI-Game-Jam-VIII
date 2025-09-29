using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class sceneState : MonoBehaviour
{
    private GlobalData data;
    private string sceneType = "menu";
    public GameObject gegli1;
    public GameObject gegli2;
    public GameObject dudi;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GlobalLoader.Instantiate(ref data);
        string sceneType = data.player.PlayerPositionButton.tag;
    }
    private void FixedUpdate()
    {
        switch (sceneType)
        {
            case ("Combat"):
                int enemyNumber = Random.Range(1,2);
                gegli1.SetActive(true);
                if(enemyNumber == 2) gegli2.SetActive(true);
                //battle UI
                break;
            case ("Social"):
                dudi.SetActive(true);
                Sprite friendSprite = dudi.GetComponent<Sprite>();
                Sprite friend = data.friendlis[Random.Range(0,data.friendlis.Count-1)];
                friendSprite = friend;

                //Implement me
                data.friendlis.Remove(friend);
                //Social UI
                break;

            case ("Questiom"):
                //Question UI
                break;

            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void resetGame()
    {
        GlobalLoader.Reset(ref data);
        SceneManager.LoadScene(2);
    }
}