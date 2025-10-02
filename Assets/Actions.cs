using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Actions : MonoBehaviour
{// Start is called once before the first execution of Update after the MonoBehaviour is created
    public Unit unit;
    public TextMeshProUGUI hptext;
    public Slider hpLeiste;
    void Start()
    {
        var data = GlobalLoader.GetGlobalData();
        data.player.HpText = hptext;
        data.player.HpLeiste = hpLeiste;

        /*HpText = gamiobj.GetComponentInChildren<TextMeshProUGUI>();
        HpLeiste = gamiobj.GetComponentInChildren<Slider>();*/
    }
    public void showActions()
    {
        var myButton = GetComponentInChildren<Button>();
        myButton.gameObject.SetActive(true);
        GlobalData data = GlobalLoader.GetGlobalData();
        myButton.onClick.AddListener(async()=> {
            myButton.gameObject.SetActive(false);
            unit.gamiobj.GetComponent<Button>().interactable = false;
            data.player.gamiobj.transform.position += new Vector3(2, 0, 0);
            data.player.Attack(unit);
            await Task.Delay(1000);
            data.player.gamiobj.transform.position -= new Vector3(2, 0, 0);
            await Task.Delay(2000);
            var unitAttacker = data.currentEnemies[Random.Range(0, 2)];
            unitAttacker.gamiobj.transform.position -= new Vector3(2, 0, 0);
            unitAttacker.Attack(data.player);
            await Task.Delay(1000);
            unitAttacker.gamiobj.transform.position += new Vector3(2, 0, 0);
            unit.gamiobj.GetComponent<Button>().interactable = true;
        });
    }
}
