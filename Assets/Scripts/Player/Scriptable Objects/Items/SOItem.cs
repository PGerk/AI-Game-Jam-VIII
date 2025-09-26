using UnityEngine;

/// <summary>
/// Abstrakte Klasse für ein Item, welches der Spieler benutzen kann.
/// </summary>
public abstract class SOItem : ScriptableObject
{
    public string itemName;
    [TextArea] public string description;
    public Sprite icon;

    //Regular Items
    public int cooldown;
    protected int cooldownTimer;
    public bool isReady;

    //Magic Items
    public bool isMagic;
    public int mpCost = 1;

    ///<summary>
    ///Benutzt das Item.
    ///</summary>
    ///<param name="player">Referenz auf den Spielercharakter.</param>
    ///<param name="target">Das Ziel (im Regelfall Gegner).</param>
    public abstract void Use(PlayerCharacter player, Target target);
    ///<summary>
    ///Sachen, die zwischen den Zügen passieren (vor allem cooldown).
    ///</summary>
    public abstract void OnAdvanceTurn();
    ///<summary>
    ///Startet cooldown.
    ///</summary>
    protected void StartCooldown()
    {
        cooldownTimer = cooldown;
        isReady = false;
    }

    ///<summary>
    ///Sollte in AdvanceTurn, führt Cooldown fort.
    ///</summary>
    protected void AdvanceCooldown()
    {
        if (!isReady && cooldownTimer > 0)
        {
            cooldownTimer--;
            Debug.Log("Cooldown timer down to " + cooldownTimer);
            return;
        }
        if (!isReady && cooldownTimer < 1)
        {
            isReady = true;
            Debug.Log("Item read!");
            return;
        }
        Debug.Log("No cooldown timer to decrease.");
        return;
    }

    ///<summary>
    ///Bezahlt die MP-Kosten, gibt TRUE bei Erfolg, FALSE bei Misserfolg zurück.
    ///</summary>
    ///<param name="player">Referenz auf den Spielercharakter.</param>
    protected bool DeductMP(PlayerCharacter player)
    {
        if (player.stats.mp >= mpCost)
        {
            player.stats.mp -= mpCost;
            return true;
        }
        else
        {
            return false;
        }
    }
}
}
