using UnityEngine;
using UnityEngine.UI;

public class Encounter_scribt : MonoBehaviour
{
    private Button thisButton;
    private bool activated;
    public Button buttona;
    public Button buttonb;
    public Button neighbor;
    public Button neighbor2;
    public GameObject player;
    /*public Button neighbor3;
    public Button neighbor4;*/
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //thisButton = this.gameObject;
        player = GameObject.FindWithTag("Player");
        activated = false;
        thisButton = gameObject.GetComponent<Button>();
        //buttonb = gameObject.GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (activated && thisButton)
        {
            thisButton.transition.none;
        }*/
    }
    public void activate()
    {
        /*buttona.enabled(true);
        if(buttonb != null)
        {
            buttonb.SetActive(true);
        }*/
        if (!activated)
        {
            player.transform.position = thisButton.transform.position;
            buttona.gameObject.SetActive(true);
            if (buttonb != null)
            {
                buttonb.gameObject.SetActive(true);
            }
            //neighbor.interactable = false;
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
            //thisButton.highlightedSprite = null;
            var spriteState = thisButton.spriteState;
            spriteState.highlightedSprite = null;
            thisButton.spriteState = spriteState;
            activated = true;
        }
        /*if (neighbor3 != null)
        {
            neighbor3.interactable = false;
        }
        if (neighbor4 != null)
        {
            neighbor4.interactable = false;
        }*/
    }
}