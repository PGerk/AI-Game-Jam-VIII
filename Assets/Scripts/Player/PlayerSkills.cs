using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    public List<SOSkill> skills;
    private PlayerCharacter player;
    void Start()
    {
        player = gameObject.GetComponent<PlayerCharacter>();
        LocateHerbert();
    }

    void LocateHerbert()
    {
        GameObject herbert = GameObject.Find("herbert");
        if (herbert)
        {
            Debug.Log("Herbert found!");
        }
    }
}
