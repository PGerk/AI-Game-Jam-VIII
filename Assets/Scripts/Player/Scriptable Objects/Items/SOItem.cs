using UnityEngine;

///<summary>
///Abstrakte Klasse für ein Item, welches der Spieler benutzen kann.
///</summary>
public abstract class SOItem : ScriptableObject
{
    [Tooltip("Name des Items.")]
    public string itemName = "Name hier";
    [Tooltip("Beschreibung des Items.")]
    [TextArea] public string description = "Beschreibung hier";
    [Tooltip("Icon des Items.")]
    public Sprite icon;

    //Regular Items
    [Tooltip("Cooldown in Runden. Wird bei magischen Items standardmäßig ignoriert.")]
    public int cooldown = 1;
    [HideInInspector]public int cooldownTimer;
    [HideInInspector]public bool isReady = true;

    //Magic Items
    [Tooltip("Aktiviert MP-Kosten, deaktiviert Cooldown.")]
    public bool isMagic = false;
    [Tooltip("MP-Kosten für einmalige Aktivierung.")]
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
