using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class sceneState : MonoBehaviour
{
    public Sprite[] enemies;
    private GlobalData data;
    private Unit player;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GlobalLoader.Instantiate(ref data);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}