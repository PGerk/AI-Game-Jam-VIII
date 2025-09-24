using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Damage Skill")]
public class DamageSkill : SOSkill
{
    public int damage;
    public override void Use(PlayerCharacter player, Target target)
    {
        if (DeductMP(player))
        {
            target.takeDamage(damage);
        }
    }

}
