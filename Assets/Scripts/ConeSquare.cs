using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeSquare : MonoBehaviour
{
    public List<Gelato> gelatosOnSquare;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //publci void create gelato
}

public class Gelato
{
    public string coneType;
    public int numberOfScoops;
    public Dictionary<string, GameObject> gelatoScoops = new Dictionary<string, GameObject>();

    public void createGelato(string cone, Dictionary<string, GameObject> scoops) //Maybe get the name of the gelato and set the texture of one scoop and hide the rest, spawn a new gelato preset
    {
        coneType = cone;
        gelatoScoops = scoops;
        numberOfScoops = scoops.Count;


    }
    public void addScoop(string scoopName, GameObject scoopObject)
    {
        if (numberOfScoops != 3)
        {
            gelatoScoops.Add(scoopName, scoopObject);
            numberOfScoops += 1;
        }
    }
}