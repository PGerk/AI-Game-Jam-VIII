using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using UnityEditor;
using System.Linq;
using System.Text.RegularExpressions;

public class sceneState : MonoBehaviour
{
    private bool isClosed = true;
    private GlobalData data;
    private string sceneType = "menu";
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
    private Vector3 originalButtonposition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        GlobalLoader.Instantiate(ref data);
        //sceneType = data.player.PlayerPositionButton.tag;
        sceneType = "Social"; //Später löschen
        text = textUI.GetComponentInChildren<TextMeshProUGUI>();
        initiateScene();
        continueButton.onClick.AddListener(ClickContinue);
        inventoryButton.onClick.AddListener(showInventory);
    }

    private void initiateScene()
    {
        switch (sceneType)
        {
            case ("Combat"):
                enemies.SetActive(true);
                int enemyNumber = Random.Range(1, 2);
                gegli1.gameObject.SetActive(true);
                gegli1.sprite = Unit.selectRandomSprite(data.enemies).sprite;
                data.currentEnemies.Append(Unit.Enemy[gegli1.sprite.name]);
                if (enemyNumber == 2)
                {
                    gegli2.gameObject.SetActive(true);
                    gegli2.sprite = Unit.selectRandomSprite(data.enemies).sprite;
                    data.currentEnemies.Append(Unit.Enemy[gegli2.sprite.name]);
                }

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
                //dudi.sprite = friend;
                StartCoroutine(waiter());
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
                text.color = Color.black;
                text.text = "Du denkst, du kannst mich aufhalten? Weißt du was? Ich auch. Mit dieser TNT-Stange hier! Und das Kloster nehme ich mit! Klick Clack Klick kli klack!";

                //battle UI
                break;

            default:
                break;
        }
    }
    IEnumerator waiter()
    {
        //Wait for 4 seconds
        yield return new WaitForSeconds(2);
    }

    private void FixedUpdate()
    {
        if(npc.blessingGiven) text.color = Color.black;

        //SceneManager.LoadScene(2);
    }
    public void ClickContinue()
    {
        switch (sceneType)
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
                //else SceneManager.LoadScene(2);
                break;
            case ("Question"):
                continueButton.GetComponentInChildren<TextMeshProUGUI>().text = "Continue";
                text.text = "Du erhälst guten Heiltrank.";
                StartCoroutine(waiter());
                SceneManager.LoadScene(2);
                break;
            default:
                break;
        }
    }
    public void showInventory()
    {
        if (!isClosed)
        {
            inventoryButton.gameObject.transform.position = originalButtonposition;
            inventoryButton.GetComponentInChildren<TextMeshProUGUI>().text = "Inventory";
            inventarUI.SetActive(false);
            isClosed = true;
            return;
        }
        isClosed = false;
        data = GlobalLoader.GetGlobalData();
        char emSpace = '\u2003';
        inventarUI.SetActive(true);
        originalButtonposition = inventoryButton.gameObject.transform.position;
        inventoryButton.gameObject.transform.position = new Vector3(450, 370,0);
        inventoryButton.GetComponentInChildren<TextMeshProUGUI>().text = "Close";
        var inventarItems = inventarUI.GetComponentsInChildren<TextMeshProUGUI>();
        data.player.Inventar.Select((item, index) => new { item, index }).ToList()
            .ForEach(item =>
            {
                var itemTMP = inventarItems[item.index];
                itemTMP.text = Regex.Replace(itemTMP.text, @">(.*?)<", $">{item.item.Name.PadRight(10, emSpace)}<");
            } 
        );
        var itemsInInventory = data.player.Inventar.Count;
        foreach (var slot in inventarItems) {
            var slotNumber = int.Parse(slot.name.Split("Item")[1]);
            var handler = slot.GetComponent<ItemHandler>();
            var itemExists = data.player.Inventar[slotNumber - 1] != null;
            handler.item = itemExists ?
                data.player.Inventar[slotNumber - 1] :
                null;
            var itemName = itemExists ?
               handler.item?.Name:
                " ";
            slot.text = Regex.Replace(slot.text, @">(.*?)<", $">{itemName.PadRight(10, emSpace)}<");
            handler.data = data;
        };
    }
    public void stealItem()
    {
        data.player.Inventar.RemoveAll(item => item.selected);
    }
}