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

    // Update is called once per frame
    public void selectItem()
    {
        item.selected = true;
        /* 
        var slotNumber = int.Parse(clickedObject.name.Split("Item")[1]);
        data.player.Inventar.Select((item, index) => new { item, index }).ToList()
         .ForEach(item =>
         {
             var currentItemNumber = item.index + 1;
             if (currentItemNumber == slotNumber)
             {
                 Debug.Log(currentItemNumber + "=" + slotNumber+ " " + item.item.Name);
                 ourText.color = Color.yellow;
                 item.item.selected = true;
             }
             else
             {
                 item.item.selected = false;
                 ourText.color = originalColor;
             }
         }
     );*/
        if (item.selected)
        {
            continueButton.interactable = true;
        }
    }


}
