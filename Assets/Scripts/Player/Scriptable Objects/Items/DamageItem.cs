using UnityEngine;

[CreateAssetMenu(menuName = "Items/Damage Item")]
public class DamageItem : SOItem
{
    public int damage;
    public override void Use(PlayerCharacter player, Target target)
    {
        if(isReady)
        {
            StartCooldown();
            target.takeDamage(damage);
        }
    }

    public override void OnAdvanceTurn()
    {
        AdvanceCooldown();
    }

}
