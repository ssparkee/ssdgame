using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeSquare : MonoBehaviour
{
    HeldItem heldItem;

    List<gelato> gelatos = new List<gelato>(){
        new gelato(),
        new gelato(),
        new gelato(),
        new gelato()
    };

    // Start is called before the first frame update
    void Start()
    {
        heldItem = GetComponent<HeldItem>();

        gelatos[0].setup("A", gameObject);
        //gelatos[1].setup("B", transform.gameObject);
        //gelatos[2].setup("C", transform.gameObject);
        //gelatos[3].setup("D", transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void placeCone(string coneIdentifier, string coneType)
    {
        switch(coneIdentifier)
        {
            case "A":
                gelatos[0].enableCone(coneType);
                return;
            case "B":
                gelatos[1].enableCone(coneType);
                return;
            case "C":
                gelatos[2].enableCone(coneType);
                return;
            case "D":
                gelatos[3].enableCone(coneType);
                return;
            default:
                gelatos[0].enableCone(coneType);
                return;
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
    }
}
class gelato
{
    GameObject gelatoParent;
    GameObject gelatoConesParent;
    GameObject gelatoScoopsParent;
    GameObject gelatoCone;

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

        gelatoIdentifier = identifier;
    }

    public void enableCone(string coneType)
    {
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

    public void enableScoop(Material material)
    {
        scoopMaterial = material;

        scoopObject.GetComponent<Renderer>().material = scoopMaterial;
        scoopObject.SetActive(true);
    }
}