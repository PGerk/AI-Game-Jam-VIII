using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private string name;
    private int attackPower;
    private int defensePower;
    private int healPower;
    private bool consumable;
    private int amount = 0;
    public bool selected;

    public int Amount { get { return amount; } }
    public int AttackPower { get { return attackPower; } }
    public int DefensePower { get { return defensePower; } }
    public string Name { get { return name; } }
    public int HealPower { get { return healPower; } }
    public bool Consumable { get { return consumable; } }

    public Item(string name, int attackPower, int defensePower, int healPower, bool consumable, int amount = 1)
    {
        this.amount = amount;
        this.name = name;
        this.attackPower = attackPower;
        this.defensePower = defensePower;
        this.healPower = healPower;
        this.consumable = consumable;
    }

    public void addToAmount()
    {
        this.amount += 1;
    }
    public void removeAmount(int stolen = 1)
    {
        this.amount -= stolen;
    }
}