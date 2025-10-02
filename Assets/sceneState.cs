using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using UnityEditor;
using System.Linq;
using System.Text.RegularExpressions;
using System;

public class sceneState : MonoBehaviour
{
    private bool isClosed = true;
    private GlobalData data;
    public GameObject enemies;
    public Image gegli1;
    public Image gegli2;
    public Image dudi;
    public GameObject textUI;
    public GameObject questionBaeume;
    public GameObject talkingpotrait;
    public Image potrait;
    private NpcTypes.NPC npc;
    public Button continueButton;
    public Button inventoryButton;
    public GameObject inventarUI;
    private TextMeshProUGUI text;
    public GameObject background;
    private Vector3 originalButtonposition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        data = GlobalLoader.GetGlobalData();
        Debug.Log(data.player != null);
        Debug.Log(data.player.playerPosition);
        Debug.Log(data.sceneType);
        text = textUI.GetComponentInChildren<TextMeshProUGUI>();
        initiateScene();
        continueButton.onClick.AddListener(ClickContinue);
        inventoryButton.onClick.AddListener(showInventory);
    }
    private void Update()
    {
        if (data.firstFight)
        {
            data.player.setHP();
            data.firstFight = false;
        }
    }
    private void initiateScene()
    {
        switch (data.sceneType)
        {
            case ("Combat"):
                enemies.SetActive(true);
                int enemyNumber = UnityEngine.Random.Range(1, 2);
                gegli1.gameObject.SetActive(true);
                gegli1.sprite = Unit.selectRandomSprite(data.enemies, 17).sprite;
                data.currentEnemies.Append(Unit.Enemy[gegli1.sprite.name]);
                data.currentEnemies[0].setHP();
                if (enemyNumber == 2)
                {
                    gegli2.gameObject.SetActive(true);
                    gegli2.sprite = Unit.selectRandomSprite(data.enemies).sprite;
                    data.currentEnemies.Append(Unit.Enemy[gegli2.sprite.name]);
                    data.currentEnemies[1].setHP();
                }
                startBattle();
                //battle UI
                break;
            case ("Social"):
                textUI.SetActive(true);
                talkingpotrait.SetActive(true);
                dudi.gameObject.SetActive(true);
                continueButton.gameObject.SetActive(true);
                continueButton.GetComponentInChildren<TextMeshProUGUI>().text = "Give Item";
                continueButton.interactable = false;


                int spriteIndex;
                (dudi.sprite,  spriteIndex) = Unit.selectRandomSprite(data.friendlis);
                potrait.sprite = Unit.selectRandomSprite(data.potraits, spriteIndex).sprite;

                npc = NpcTypes.friendly[dudi.sprite.name];
                text.gameObject.SetActive(true);
                textUI.SetActive(true);
                potrait.gameObject.SetActive(true);
                text.color = npc.color;
                text.text = npc.text1;
                data.friendlis.Remove(dudi.sprite);
                data.potraits.Remove(potrait.sprite);
                break;

            case ("Questiom"):
                questionBaeume.SetActive(true);
                textUI.SetActive(true);
                text.color = Color.black;
                text.text = "Gebe dem Wald ein Opfer dar, und er wird sich dir dankbar zeigen.";
                break;
            case ("Boss"):
                textUI.SetActive(true);
                gegli1.gameObject.SetActive(true);
                gegli1.sprite = Unit.selectRandomSprite(data.enemies,3).sprite;
                text.color = Color.black;
                text.text = "Du denkst, du kannst mich aufhalten? Weiﬂt du was? Ich auch. Mit dieser TNT-Stange hier! Und das Kloster nehme ich mit! Klick Clack Klick kli klack!";
                continueButton.gameObject.SetActive(true);

                //battle UI
                break;

            default:
                break;
        }
    }

    private void FixedUpdate()
    {
        if(data.sceneType == "Social" && npc.blessingGiven) text.color = Color.black;
        if (data.sceneType == "Combat" && data.currentEnemies.Length == 0) continueButton.gameObject.SetActive(true);
        if (data.sceneType == "Boss" && data.currentEnemies.Length == 0) {
            textUI.SetActive(true);
            textUI.GetComponent<TextMeshProUGUI>().text = "Du hast gewonnen. [Bitte Spiel manuel verlassen]";
            }
    }
    public void ClickContinue()
    {
        switch (data.sceneType)
        {
            case ("Combat"):
                SceneManager.LoadScene(2);
                break;
            case ("Social"):
                if (!npc.blessingGiven && !npc.itemStolen)
                {
                    continueButton.GetComponentInChildren<TextMeshProUGUI>().text = "Continue";
                    text.text = npc.text2;
                    stealItem();
                    npc.itemStolen = true;
                    showInventory();
                }
                else if (!npc.blessingGiven)
                {
                   text.text = npc.blessingText;
                   npc.GiveBlessing(data.player);
                }
                else SceneManager.LoadScene(2);
                break;
            case ("Question"):
                continueButton.GetComponentInChildren<TextMeshProUGUI>().text = "Continue";
                text.text = "Du wirst voll geheilt.";
                data.player.Heal();
                SceneManager.LoadScene(2);
                break;
            case ("Boss"):
                textUI.SetActive(false);
                startBattle();
                break;
            default:
                break;
        }
    }
    public void showInventory()
    {
        data = GlobalLoader.GetGlobalData();
        var inventarItems = inventarUI.GetComponentsInChildren<TextMeshProUGUI>();
        if (!isClosed)
        {
            inventoryButton.gameObject.transform.position = originalButtonposition;
            inventoryButton.GetComponentInChildren<TextMeshProUGUI>().text = "Inventory";
            inventarUI.SetActive(false);
            isClosed = true;
            deselectItems(inventarItems, ref data);
            return;
        }
        isClosed = false;
        char emSpace = '\u2003';
        inventarUI.SetActive(true);
        originalButtonposition = inventoryButton.gameObject.transform.position;
        inventoryButton.gameObject.transform.position = new Vector3(450, 370,0);
        inventoryButton.GetComponentInChildren<TextMeshProUGUI>().text = "Close";
        int currentPosition = 0;
        try
        {
            foreach (var slot in inventarItems)
            {
                var slotNumber = int.Parse(slot.name.Split("Item")[1]);
                currentPosition = slotNumber;
                var handler = slot.GetComponent<ItemHandler>();
                var itemExists = data.player.Inventar.Count > slotNumber - 1;
                handler.item = itemExists ?
                    data.player.Inventar[slotNumber - 1] :
                    null;
                var itemName = itemExists ?
                   handler.item?.Name :
                    " ";
                slot.text = Regex.Replace(slot.text, @">(.*?)<", $">{itemName.PadRight(10, emSpace)}<");
                handler.data = data;
            };
        }
        catch (Exception e) 
        {
            Debug.LogError(e);
            Debug.Log(currentPosition.ToString());
        }
    }
    public void stealItem()
    {
        data.player.Inventar.RemoveAll(item => item.selected);
    }
    public static void deselectItems(TextMeshProUGUI[] inventarItems, ref GlobalData data)
    {
        int currentPosition = 0;
        try
        {
            foreach (var slot in inventarItems)
            {
                var slotNumber = int.Parse(slot.name.Split("Item")[1]);
                currentPosition = slotNumber;
                var itemExists = data.player.Inventar.Count > slotNumber - 1;
                if (itemExists && data.player.Inventar[slotNumber - 1].selected)
                {
                    data.player.Inventar[slotNumber - 1].selected = false;
                }
            };
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            Debug.Log(currentPosition.ToString());
        }
    }
    public void startBattle()
    {
        foreach (var enemy in data.currentEnemies)
        {
            enemy.HpText = enemy.gamiobj.GetComponentInChildren<TextMeshProUGUI>();
            enemy.HpLeiste = enemy.gamiobj.GetComponentInChildren<Slider>();
        }
    }
}