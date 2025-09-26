using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DropdownScript : MonoBehaviour
{
    private TMP_Dropdown dropdown;
    private PlayerCharacter player;

    private List<SOItem> items = new List<SOItem>();
    private List<string> itemLabels = new List<string>();
    private TMP_Text actionInfo;

    private void Start()
    {
        FullInitialize();
    }

    public void FullInitialize()
    {
        dropdown = GetComponent<TMP_Dropdown>();

        player = GameObject.Find("Player").GetComponent<PlayerCharacter>();
        if (player)
        {
            Debug.Log("Player found: " + player + player.name);
        }
        else
        {
            Debug.Log("No player found!");
        }

        actionInfo = GameObject.Find("Action Info").GetComponent<TMP_Text>();
        if (actionInfo)
        {
            Debug.Log("Description box found: " + actionInfo + actionInfo.name);
        }
        else
        {
            Debug.Log("No Description Box found!");
        }

        items = player.inventory.items;

        PopulateDropdown();
        dropdown.onValueChanged.AddListener(OnDropdownChanged);
        FillText(0);
    }

    public void PopulateDropdown()
    {
        dropdown.ClearOptions();

        foreach (SOItem item in items)
        {
            Debug.Log("Found item " + item.itemName);
            itemLabels.Add(item.itemName);
        }

        dropdown.AddOptions(itemLabels);
    }

    private void OnDropdownChanged(int index)
    {
        FillText(index);
    }

    private void FillText(int index)
    {
        Debug.Log("Item selected!");
        Debug.Log("Item Index: " + index);
        if (index < 0 || index >= items.Count) return;
        Debug.Log("Item found within range!");

        SOItem selectedItem = items[index];
        Debug.Log("Selected Item: " + selectedItem.itemName);

        //Text zusammenbauen
        string info = $"Name: {selectedItem.itemName}\n\n";

        //Unterscheidung Item oder Skill
        if (selectedItem.isMagic)
        {
            info += $"Manakosten: {selectedItem.mpCost}\n\n";
        }
        else
        {
            if (selectedItem.isReady)
            {
                info += $"Einsatzbereit!\n";
                info += $"Cooldown nach Einsatz: {selectedItem.cooldown}\n\n";
            }
            else
            {
                info += $"Einsatzbereit in: {selectedItem.cooldownTimer}\n\n";
            }
        }
        
        info += selectedItem.description;

        actionInfo.text = info;
    }
}
