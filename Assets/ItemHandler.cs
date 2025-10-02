using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemHandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    //private TextMeshProUGUI ourText;
    //private Color currentColor;
    public Item item;
    private Color originalColor = NpcTypes.HexToColor("#C9DBCC");
    public Button continueButton;
    public GlobalData data;
    private GameObject clickedObject;
    public GameObject inventarUI;
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        clickedObject = eventData.pointerPress;
        data = GlobalLoader.GetGlobalData();
        selectItem();
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        /*currentColor = gameObject.GetComponent<TextMeshProUGUI>().color;
        gameObject.GetComponent<TextMeshProUGUI>().color = Color.white;*/
        if (gameObject.GetComponent<TextMeshProUGUI>().color != Color.yellow) gameObject.GetComponent<TextMeshProUGUI>().color = Color.white;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if(gameObject.GetComponent<TextMeshProUGUI>().color != Color.yellow) gameObject.GetComponent<TextMeshProUGUI>().color = originalColor;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ourText = gameObject.GetComponent<TextMeshProUGUI>();
    }
    void FixedUpdate()
    {
        if (item == null) return;
        if (item.selected == true)
        {
            clickedObject.GetComponent<TextMeshProUGUI>().color = Color.yellow;
        }
        else
        {
            gameObject.GetComponent<TextMeshProUGUI>().color = originalColor;
        }
    }
    public void selectItem()
    {
        var inventarItems = inventarUI.GetComponentsInChildren<TextMeshProUGUI>();
        sceneState.deselectItems(inventarItems, ref data);
        item.selected = true;
        if (item.selected)
        {
            continueButton.interactable = true;
        }
    }
}
