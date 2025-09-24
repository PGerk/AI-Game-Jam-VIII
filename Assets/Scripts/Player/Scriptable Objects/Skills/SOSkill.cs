using UnityEngine;

/// <summary>
/// Abstrakte Klasse für einen Skill (z.B. Magie), welchen der Spieler benutzen kann.
/// </summary>
public abstract class SOSkill : ScriptableObject
{
    public string skillName;
    [TextArea] public string description;
    public Sprite icon;

    public int mp_cost;
    
    /// <summary>
    /// Benutzt den Skill.
    /// </summary>
    /// <param name="player">Referenz auf den Spielercharakter.</param>
    /// <param name="target">Das Ziel (im Regelfall Gegner). Soll Spieler betroffen werden, einfach NULL übergeben.</param>
    public abstract void Use(PlayerCharacter player, Target target);

    /// <summary>
    /// Bezahlt die MP-Kosten, gibt TRUE bei Erfolg, FALSE bei Misserfolg zurück.
    /// </summary>
    /// /// <param name="player">Referenz auf den Spielercharakter.</param>
    protected bool DeductMP(PlayerCharacter player)
    {
        if (player.stats.mp >= mp_cost)
        {
            player.stats.mp -= mp_cost;
            return true;
        }
        else
        {
            return false;
        }
    }
}
