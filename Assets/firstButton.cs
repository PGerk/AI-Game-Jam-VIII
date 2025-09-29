using UnityEngine;
using UnityEngine.UI;

public class firstButton : MonoBehaviour
{
    public Button button;
    public GameObject text;
    public Button thisButton;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void startefirst()
    {
        button.gameObject.SetActive(true);
        text.gameObject.SetActive(true);
        thisButton.gameObject.SetActive(false);
    }
}
