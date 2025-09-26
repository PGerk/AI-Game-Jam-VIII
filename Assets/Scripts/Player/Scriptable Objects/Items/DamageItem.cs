using UnityEngine;

[CreateAssetMenu(menuName = "Items/Damage Item")]
public class DamageItem : SOItem
{
    [Tooltip("Am Gegner verursachter Schaden bei Benutzung.")]
    public int damage;
    public override void Use(PlayerCharacter player, Target target)
    {
        if(!isMagic && isReady)
        {
            StartCooldown();
            target.takeDamage(damage);
            return;
        }
        if(isMagic)
        {
            if(DeductMP(player))
            {
                target.takeDamage(damage);
            }
            else
            {
                Debug.Log("Not enough MP! Remaining: " + player.stats.mp + " of needed " + mpCost);
            }
            return;
        }
        Debug.Log("Item not ready! Cooldown remaining: " + cooldownTimer);
    }

    public override void OnAdvanceTurn()
    {
        AdvanceCooldown();
    }

}
