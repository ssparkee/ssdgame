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

    public List<string> gelatosToMake;
    /*
    This list is set by the customer at the front of the line.

    When the button is pressed this string is checked to match to the current gelatos.

    Each gelato is one string.

    string format is 

    cone:scoop1:scoop2:scoop3
    If there is not 3 scoops do something like
    cone:scoop1:scoop2

    gelatoa - Green
    gelatob - Red
    */

    // Start is called before the first frame update
    void Start()
    {
        
        gelatosToMake.Add("wafer:gelatoa:gelatob:gelatoa");
        gelatosToMake.Add("wafer:gelatob:gelatob");

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

    public void buttonPressed()
    {
        bool allMatch = true;
        //Get the customer currently at the front. If there is none still delete the ice cream maybe
        foreach (gelato finishedGelato in gelatos)
        {
            if (finishedGelato.hasGelato) {
                string[] scoopNames = setStringListLength(finishedGelato.scoopStringNames(), 3);
                string coneName = finishedGelato.coneName;

                (bool, string) gealtoMatch = checkForGelatoMatch(gelatosToMake, scoopNames, coneName);
                if(gealtoMatch.Item1)
                {
                    //gelato do match
                    //Debug.Log("does match");
                    //One bug to note. If there is only one ice cream on the table but multiple ones to make, it still returns true
                    gelatosToMake.Remove(gealtoMatch.Item2);
                    
                } else {
                    //Debug.Log("does not match");
                    allMatch = false;
                    //gelato does not match. Do something
                }
            }
        }
        if (gelatosToMake.Count != 0)
        {
            allMatch = false;
        }
        //all gelatos do match. Do something
        removeAllGelatos();
        gelatosToMake = new List<string>();

        Debug.Log("removed them all");

        if(allMatch)
        {
            Debug.Log("All match!");
        } else {
            Debug.Log("dont match");
        }
    }

    

    string[] setStringListLength(string[] stringList, int length)
    {
        if (stringList.Length == length)
        {
            return stringList;
        }
        string[] stringList2 = new string[length];
        int i = 0;
        foreach (string string1 in stringList)
        {
            stringList2[i] = string1;

            i += 1;

            if (i == length)
            {
                return stringList2;
            }
        }
        return stringList2;
    }

    void logList(string[] stringList, string index = "")
    {
        foreach (string stringToLog in stringList)
        {
            Debug.Log(index + stringToLog);
        }
    }

    (bool, string) checkForGelatoMatch(List<string> gelatosToMake, string[] scoopNames, string coneName)
    {
        (bool, string) gelatoMatches = (true, null);
        foreach (string gelatoToMake in gelatosToMake)
        {
            string[] gelatoString = setStringListLength(gelatoToMake.Split(":"), 4);
            //Debug this in case there are errors

            //logList(gelatoString, index:"gesltosn - ");
            //logList(scoopNames, index:"fkeajfkajkl - ");

            if (gelatoString[0] == coneName) { //If the cone is the right one loop through all scoops and make sure they all match
                int numberOfScoops = 2;

                gelatoMatches = (true, gelatoToMake);

                for (int i = 0; i <= numberOfScoops; i++)
                {
                    if(gelatoString[i + 1] != scoopNames[i]) 
                    {
                        //Debug.Log(gelatoString[i+1] + " is not " + scoopNames[i]);
                        gelatoMatches = (false, null);
                    }
                }
            }
            if(gelatoMatches.Item1)
            {
                //Debug.Log("1. feiafea It mateches the ststeis");
                return gelatoMatches;
            }
        }
        return gelatoMatches;
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
        
        if(gelatos[index].hasGelato) {
            return;
        }

        gelatos[index].enableCone(coneType);

        heldItem.removeHeldItem();
    }

    public bool removeGelato(string coneIdentifier)
    {
        int index = getIndex(coneIdentifier);

        if (gelatos[index].hasGelato)
        {
            gelatos[index].destroyCone();
            return true;
        }
        return false;
    }

    public void removeAllGelatos()
    {
        foreach (gelato gelato in gelatos)
        {
            if(gelato.hasGelato)
            {
                gelato.destroyCone();
            }
        }
    }
}
class gelato
{
    GameObject gelatoParent;
    GameObject gelatoConesParent;
    GameObject gelatoScoopsParent;
    GameObject gelatoCone;
    GameObject coneSquare;
    public bool hasGelato = false;
    public int numberOfScoops;
    public string coneName;

    string gelatoIdentifier; //String of either A, B, C or D

    List<gelatoScoop> gelatoScoops;

    public void setup(string identifier, GameObject conesquare) //Pass in the identifier (ABCD) and this.
    {
        coneSquare = conesquare;
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

    public string[] scoopStringNames()
    {
        string[] scoopNames = new string[3];
        int i = 0;
        foreach (gelatoScoop scoop in gelatoScoops)
        {
            if(scoop.scoopName != "")
            {
                scoopNames[i] = scoop.scoopName.ToLower();
            }
            i += 1;
        }
        return scoopNames;
    }

    public void enableCone(string coneType)
    {
        hasGelato = true;
        numberOfScoops = 0;
        coneName = coneType.ToLower();

        switch(coneType.ToLower())
        {
            case "wafer":
                gelatoCone = gelatoConesParent.transform.Find("Wafer").gameObject;
                gelatoScoopsParent.transform.position = gelatoScoopsParent.transform.parent.TransformPoint(0,0,0);
                gelatoCone.SetActive(true);
                return;
            case "bowl":
                gelatoCone = gelatoConesParent.transform.Find("Bowl").gameObject;
                gelatoScoopsParent.transform.position = gelatoScoopsParent.transform.parent.TransformPoint(0,-0.09f,0);
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
        if (!hasGelato)
        {
            return;
        }
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

        setup(gelatoIdentifier, coneSquare);
    }
}

class gelatoScoop
{
    public string scoopName = "";
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

/*
The customer needs to be able to request a gelato.

What happens is that:
They have a string list - either made beforehand or can be randomly made on creation
This string is shown to the character in some form.

The character then has to make a gelato with the requested scoops, cone, etc.
When the ice cream is finished the player has to press some sort of button (maybe a bell) to indicate to the 
customer the ice cream is made.

Then the customer does a check by getting the scoopName and coneType etc. and matching that to the string 
list of the requested gelato.


*/

/*class playerGelato
{
    bool isCone = false;
    string coneType;
    bool isScoop = false;
    string scoopType;
    Texture scoopTexture;

    public void setup(string gelatoType, Texture texture = null)
    {
        switch (gelatoType)
        {
            case "wafer":
                isCone = true;
                coneType = gelatoType;
                return;
            case "bowl":
                isCone = true;
                coneType = gelatoType;
                return;
            default:
                isScoop = false;
                coneType = "scoop";
                return;
        }
    }
}*/