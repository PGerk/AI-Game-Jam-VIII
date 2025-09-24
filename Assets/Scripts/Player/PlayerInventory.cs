using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<SOItem> items;
    private PlayerCharacter player;
    void Start()
    {
        player = gameObject.GetComponent<PlayerCharacter>();
    }
}
