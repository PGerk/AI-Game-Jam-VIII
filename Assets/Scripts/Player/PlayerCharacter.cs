using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public PlayerStats stats;
    public PlayerInventory inventory;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool TakeDamage(int amount)
    {
        stats.hp -= amount;
        return (CheckDeath());
    }

    private bool CheckDeath()
    {
        if (stats.hp <= 0)
        {
            Debug.Log("Game Over!");
            return true;
        }
        return false;
    }
}
