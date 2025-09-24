using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int hp, mp, attack, defense, speed, magic;
    private PlayerCharacter player;

    void Start()
    {
        player = gameObject.GetComponent<PlayerCharacter>();
    }

}
