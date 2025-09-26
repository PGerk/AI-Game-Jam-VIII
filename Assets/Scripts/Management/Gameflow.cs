using UnityEngine;

public class Gameflow : MonoBehaviour
{
    public PlayerCharacter player;
    public Target obstacle;
    public DropdownScript dropdown;
    public GameObject encounter;

    public enum GameState
    {
        Loading,
        Map,
        Encounter,
        Success,
        GameOver
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialize();
        EncounterStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize()
    {
        if (!player)
        {
            player = GameObject.Find("Player").GetComponent<PlayerCharacter>();
        }
        if (!obstacle)
        {
            obstacle = GameObject.Find("Obstacle").GetComponent<Target>();
        }
        if(!dropdown)
        {
            dropdown = GameObject.Find("Action Selection").GetComponent<DropdownScript>();
        }
        if(!encounter)
        {
            encounter = GameObject.Find("Encounter");
        }

        encounter.SetActive(false);
    }

    public void EncounterStart()
    {
        encounter.SetActive(true);
        if (!player)
        {
            player = GameObject.Find("Player").GetComponent<PlayerCharacter>();
        }
        if (!obstacle)
        {
            obstacle = GameObject.Find("Obstacle").GetComponent<Target>();
        }
        if (!dropdown)
        {
            dropdown = GameObject.Find("Action Selection").GetComponent<DropdownScript>();
        }
        if (!encounter)
        {
            encounter = GameObject.Find("Encounter");
        }
        dropdown.FullInitialize(player);
        
    }
}
