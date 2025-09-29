using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class Unit
{
    private List<Item> inventar;
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

    public Unit(int maxHp, int attackPower, string unitName)
    {
        this.maxHp = maxHp;
        this.currentHp = maxHp;
        this.attackPower = attackPower;
        this.unitName = unitName;
    }

    public void Attack(Unit unitToAttack, int damage)
    {
        int extraDamage = 0;
        foreach(var item in inventar)
        {
            extraDamage += item.AttackPower;
        }
        int finDamage = (int)(attackPower + damage + extraDamage);
        unitToAttack.TakeDamage(finDamage);
        Console.WriteLine(unitName + " attacks " + unitToAttack.unitName + " and deals " + finDamage + " damage!");
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;

        if (IsDead)
            Console.WriteLine(unitName + " has been defeated!");
    }

    public void Heal(Item item)
    {
        if (!item.Consumable) return;
        currentHp = item.HealPower + currentHp > maxHp ? maxHp : currentHp+ item.HealPower;
        Console.WriteLine(UnitName + " heals ");
    }
    public void setSprite(Sprite sprite)
    {
        Image image = gamiobj.GetComponent<Image>();
        if(image != null)
        {
            image.sprite = sprite;
        }
    }
}