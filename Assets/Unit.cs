using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Unit
{
    private List<Item> inventar = new List<Item>();
    private List<Item> blessings = new List<Item>();
    private int currentHp;
    private int maxHp;
    private int attackPower;
    private string unitName;
    public GameObject gamiobj;
    private GameObject playerPositionButton;

    public int Hp { get { return currentHp; } }
    public string UnitName { get { return unitName; } }
    public bool IsDead { get { return currentHp <= 0; } }
    public GameObject PlayerPositionButton { get { return playerPositionButton; } set { playerPositionButton = value; } }
    public List<Item> Inventar { get { return inventar; } set { } }
    public List<Item> Blessings { get { return blessings; } }

    public Unit(int maxHp, int attackPower, string unitName)
    {
        this.maxHp = maxHp;
        this.currentHp = maxHp;
        this.attackPower = attackPower;
        this.unitName = unitName;
    }
    public int GetAttackPower()
    {
        int extraDamage = 0;
        foreach (var item in inventar)
        {
            extraDamage += item.AttackPower;
        }
        foreach (var item in blessings)
        {
            extraDamage += item.AttackPower;
        }
        extraDamage += attackPower;
        return extraDamage;
    }
    public int GetDefensePower()
    {
        int defence = 0;
        foreach (var item in inventar)
        {
            defence += item.DefensePower;
        }
        foreach (var item in blessings)
        {
            defence += item.DefensePower;
        }
        return defence;
    }
    public void Attack(Unit unitToAttack, int damage)
    {
        int finDamage = (int)(damage + GetAttackPower());
        unitToAttack.TakeDamage(finDamage);
        Console.WriteLine(unitName + " attacks " + unitToAttack.unitName + " and deals " + finDamage + " damage!");
    }

    public void TakeDamage(int damage)
    {

        currentHp -= damage;

        if (IsDead)
        {
            Console.WriteLine(unitName + " has been defeated!");
            if (unitName == "Player")
            {
                EndGame.resetGame();
            }
        }
    }

    public void Heal(Item item)
    {
        if (!item.Consumable) return;
        currentHp = item.HealPower + currentHp > maxHp ? maxHp : currentHp + item.HealPower;
        Console.WriteLine(UnitName + " heals ");
    }
    public void setSprite(Sprite sprite)
    {
        Image image = gamiobj.GetComponent<Image>();
        if (image != null)
        {
            image.sprite = sprite;
        }
    }
    public void setupInventory()
    {
        inventar.Add(new Item("Schwert", 2, 0, 0, false));
        inventar.Add(new Item("Schild", 0, 3, 0, false));
        inventar.Add(new Item("Rüstung", 1, 2, 0, false));
        inventar.Add(new Item("Handschuh", 1, 1, 0, false));
        inventar.Add(new Item("Dolch", 3, -1, 0, false));
    }
    public static void givePotion(Unit unit)
    {
        unit.inventar.Add(new Item("Heiltrank", 0, 0, 8, true));
    }
    public static (Sprite sprite, int index) selectRandomSprite(List<Sprite> spriteList, int givenIndex = -1)
    {
        int index = givenIndex < 0 ? Random.Range(0, spriteList.Count) : givenIndex;
        if (index == -1) Debug.Log("Index ist -1");
        Sprite sprite = spriteList[index];
        return (sprite, index);
    }
    public static Dictionary<string, Unit> Enemy = new Dictionary<string, Unit>
    {
        ["Alexa"] = new Unit(10, 1, "Alexa"),
        ["KnifeCrab"] = new Unit(8, 2, "KnifeCrab"),
        ["SquidArcher"] = new Unit(6, 4, "SquidArcher"),
    };
}