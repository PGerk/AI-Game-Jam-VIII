using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Encounter_scribt : MonoBehaviour
{
    private Button thisButton;
    public Button buttona;
    public Button buttonb;
    public Button neighbor;
    public Button neighbor2;
    private GlobalData data;

    void Start()
    {
        GlobalLoader.Instantiate(ref data);
        thisButton = gameObject.GetComponent<Button>();
        if (data.activatedButtons.Count > 0 && data.activatedButtons.Contains(thisButton.gameObject))
        {
            styleButtons();
        }
    }

    void Update()
    {

    }
    public void activate()
    {
        if (!data.activatedButtons.Contains(thisButton.gameObject))
        {
            data.activatedButtons.Add(thisButton.gameObject);
            styleButtons();
            switchScene();
        }
    }
    public void switchScene()
    {
         string sceneType = data.player.PlayerPositionButton.tag;
        SceneManager.LoadScene(0);
    }
    private void styleButtons()
    {
        data.player.PlayerPositionButton = thisButton.gameObject;
        data.player.gamiobj.transform.position = data.player.PlayerPositionButton.transform.position;

        buttona.gameObject.SetActive(true);
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

        var spriteState = thisButton.spriteState;
        spriteState.highlightedSprite = null;
        thisButton.spriteState = spriteState;
    }
}