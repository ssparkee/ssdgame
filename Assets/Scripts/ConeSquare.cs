using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeSquare : MonoBehaviour
{

    List<gelato> gelatos = new List<gelato>(){
        new gelato(),
        new gelato(),
        new gelato(),
        new gelato()
    };

    // Start is called before the first frame update
    void Start()
    {

        gelatos[0].setup("A", gameObject);
        gelatos[1].setup("B", transform.gameObject);
        //gelatos[2].setup("C", transform.gameObject);
        //gelatos[3].setup("D", transform.gameObject);
    }

        /*
        Maybe just set a cone already on the square as active. gelato hierarchy is:

        ConeSquare
            GelatoA
                GelatoConesA
                    Wafer
                    Bowl
                GelatoScoopsA
                    ScoopA1
                    ScoopA2
                    ScoopA3
                GelatoColliderA
            GelatoB
                ~~
            GelatoC
                ~~
            GelatoD
                ~~

        This script is on the cone square. Dont know the name of it so i put it down as ConeSquare

        If cone type is bowl subtract from y position
        If cone type is wafer set y position to 0

        Maybe make another class for the gelato itself
        Conesquare has a list of 4 different gelato classes
        Conesquare has a createGelato function that finds a gelato that is not active and calls setCone to set the cone for that particular gelato.
        
        All gelatos have a different collider, which are all proccessed by ConeSquare which runs a function on the gelato script.
        
        Gelato has a list of all 3 scoops. The letter of the gelato is passed in (A, B, C, D).
        With the letter gelato does searches for the gameobjects of the different scoops.
        */

    // Update is called once per frame
    void Update()
    {
        
    }

    int getIndex(string identifier)
    {
        switch(identifier)
        {
            case "A":
                return 0;
            case "B":
                return 1;
            case "C":
                return 2;
            case "D":
                return 3;
            default:
                return -1;
        }
    }


    public void placeGelato(string coneIdentifier, Material scoopMaterial, string scoopName, HeldItem heldItem)
    {
        int index = getIndex(coneIdentifier);

        if(gelatos[index].numberOfScoops == 3) {return;}

        gelatos[index].enableScoop(scoopMaterial, scoopName);

        heldItem.removeHeldItem();
    }

    public void placeCone(string coneIdentifier, string coneType, HeldItem heldItem)
    {
        int index = getIndex(coneIdentifier);
        
        if(gelatos[index].hasGelato) {return;}

        gelatos[index].enableCone(coneType);

        heldItem.removeHeldItem();
    }
}
class gelato
{
    GameObject gelatoParent;
    GameObject gelatoConesParent;
    GameObject gelatoScoopsParent;
    GameObject gelatoCone;
    public bool hasGelato;
    public int numberOfScoops;

    string gelatoIdentifier; //String of either A, B, C or D

    List<gelatoScoop> gelatoScoops;

    public void setup(string identifier, GameObject coneSquare) //Pass in the identifier (ABCD) and this.
    {
        gelatoParent = coneSquare.transform.Find("Gelato" + identifier).gameObject;
        gelatoConesParent = gelatoParent.transform.Find("GelatoCones" + identifier).gameObject;
        gelatoScoopsParent = gelatoParent.transform.Find("GelatoScoops" + identifier).gameObject;

        gelatoScoops = new List<gelatoScoop>(){
            new gelatoScoop(),
            new gelatoScoop(),
            new gelatoScoop(),
        };

        gelatoScoops[0].setupScoop(gelatoScoopsParent.transform.Find("Scoop" + identifier + "1").gameObject);
        gelatoScoops[1].setupScoop(gelatoScoopsParent.transform.Find("Scoop" + identifier + "2").gameObject);
        gelatoScoops[2].setupScoop(gelatoScoopsParent.transform.Find("Scoop" + identifier + "3").gameObject);

        numberOfScoops = 0;
        gelatoIdentifier = identifier;
        hasGelato = false;
    }

    public void enableCone(string coneType)
    {
        hasGelato = true;
        numberOfScoops = 0;

        switch(coneType.ToLower())
        {
            case "wafer":
                gelatoCone = gelatoConesParent.transform.Find("Wafer").gameObject;
                gelatoCone.SetActive(true);
                return;
            case "bowl":
                gelatoCone = gelatoConesParent.transform.Find("Bowl").gameObject;
                gelatoCone.SetActive(true);
                return;
            default:
                gelatoCone = gelatoConesParent.transform.Find("Bowl").gameObject;
                gelatoCone.SetActive(true);
                return;
        }
    }

    public void enableScoop(Material scoopMaterial, string scoopName)
    {
        numberOfScoops += 1;

        gelatoScoops[numberOfScoops - 1].enableScoop(scoopMaterial, scoopName);
    }

    public void pickupCone()
    {

    }

    public void destroyCone()
    {
        gelatoScoopsParent.transform.Find("Scoop" + gelatoIdentifier + "1").gameObject.SetActive(false);
        gelatoScoopsParent.transform.Find("Scoop" + gelatoIdentifier + "2").gameObject.SetActive(false);
        gelatoScoopsParent.transform.Find("Scoop" + gelatoIdentifier + "3").gameObject.SetActive(false);
        gelatoCone.SetActive(false);

        gelatoScoops = null;
    }
}

class gelatoScoop
{
    string scoopName;
    Material scoopMaterial;
    GameObject scoopObject;

    public void setupScoop(GameObject gameScoopObject)
    {
        scoopObject = gameScoopObject;
    }

    public void enableScoop(Material material, string name)
    {
        scoopMaterial = material;
        scoopName = name;

        scoopObject.GetComponent<Renderer>().material = scoopMaterial;
        scoopObject.SetActive(true);
    }
}