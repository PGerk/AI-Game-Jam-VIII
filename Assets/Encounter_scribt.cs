using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Encounter_scribt : MonoBehaviour
{
    public GameObject thisButton;
    public Button buttona;
    public Button buttonb;
    public Button neighbor;
    public Button neighbor2;
    private GlobalData data;

    void Start()
    {
        if(data ==null)GlobalLoader.Instantiate();
        data = GlobalLoader.GetGlobalData();
        if(data.player.gamiobj == null)
        {
           data.player.gamiobj = GameObject.FindWithTag("Player");
            Debug.Log($"Gameobject found = {data.player.gamiobj != null}");
        }
        //thisButton = gameObject.GetComponent<Button>();
        if (data.activatedButtons.Count > 0 && data.activatedButtons.Contains(thisButton.name))
        {
            styleButtons();
        }
    }
    public void activate()
    {
        if (!data.activatedButtons.Contains(thisButton.name))
        {
            data.activatedButtons.Add(thisButton.name);
            styleButtons();
            switchScene();
        }
    }
    public void switchScene()
    {
        SceneManager.LoadScene(0);
    }
    private void styleButtons()
    {
        data = GlobalLoader.GetGlobalData();
        data.player.playerPosition = thisButton.transform.position;
        data.sceneType = thisButton.tag;
        data.player.gamiobj.transform.position = data.player.playerPosition;
        if (buttona)
        {
            buttona.gameObject.SetActive(true);
        }
        if (buttonb != null)
        {
            buttonb.gameObject.SetActive(true);
        }
        if (neighbor)
            if (neighbor != null)
            {
                neighbor.interactable = false;
            }
        if (neighbor2)
            if (neighbor2 != null)
            {
                neighbor2.interactable = false;
            }

        var spriteState = thisButton.GetComponent<Button>().spriteState;
        spriteState.highlightedSprite = null;
        thisButton.GetComponent<Button>().spriteState = spriteState;
    }
}