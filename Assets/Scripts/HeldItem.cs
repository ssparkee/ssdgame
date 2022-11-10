using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldItem : MonoBehaviour
{
    GameObject playerHeldItemObject;
    [SerializeField] GameObject boxGameObject;
    GameObject boxPlayerObject;
    [SerializeField] GameObject box2GameObject;
    GameObject box2PlayerObject;
    GameObject gelatoPlayerObject;
    [SerializeField] GameObject gelatoAGameObject;
    [SerializeField] GameObject gelatoBGameObject;
    [SerializeField] GameObject waferGameObject;
    [SerializeField] GameObject bowlGameObject;
    GameObject waferPlayerObject;
    GameObject bowlPlayerObject;
    
    //Dictionary<string, GameObject> heldItem = new Dictionary<string, GameObject>();
    
    Dictionary<GameObject, GameObject> playerObjectRef = new Dictionary<GameObject, GameObject>();

    public string heldItemName = "";
    public GameObject heldItem = null;
    public bool holdingItem = false;
    public string heldItemType = null;
    public Material gelatoMaterial;

    // Start is called before the first frame update
    void Start()
    {
        playerHeldItemObject = transform.Find("Camera").Find("HeldItem").gameObject;

        boxPlayerObject = playerHeldItemObject.transform.Find("Box").gameObject;
        box2PlayerObject = playerHeldItemObject.transform.Find("OtherBox").gameObject;
        gelatoPlayerObject = playerHeldItemObject.transform.Find("Gelato").gameObject;
        waferPlayerObject = playerHeldItemObject.transform.Find("Wafer").gameObject;
        bowlPlayerObject = playerHeldItemObject.transform.Find("Bowl").gameObject;

        playerObjectRef.Add(boxGameObject, boxPlayerObject);
        playerObjectRef.Add(box2GameObject, box2PlayerObject);
        playerObjectRef.Add(gelatoAGameObject, gelatoPlayerObject);
        playerObjectRef.Add(gelatoBGameObject, gelatoPlayerObject);
        playerObjectRef.Add(waferGameObject, waferPlayerObject);
        playerObjectRef.Add(bowlGameObject, bowlPlayerObject);
    }
    
    public void dropHeldItem()
    {
        if (holdingItem)
        {
            switch (getItemType(heldItemName)) //If not a box, dont put the game object in front of the player
            {
                case "box":
                    GameObject gameObject = getGameObject(heldItem);
                    gameObject.transform.position = transform.position;
                    gameObject.transform.rotation = transform.rotation;
                    gameObject.transform.Translate(0, 0, 2);
                    gameObject.SetActive(true);
                    break;
                default:
                    break;
            }

            removeHeldItem();
        }
    }
    public void removeHeldItem() //Different to drop held item as it does not put the box in front of the player
    {
        if (holdingItem) 
        {
            heldItem.SetActive(false);

            heldItemName = "";
            heldItem = null;
            holdingItem = false;
            heldItemType = null;
        }
    }
    public void setHeldItem(string itemName, GameObject itemObject, Material material = null, List<string> gelatoInfo = null)
    {
        if (holdingItem) {
            return;
        }
        heldItemName = itemName;
        heldItem = itemObject;
        holdingItem = true;
        heldItemType = getItemType(itemName);

        switch (heldItemType)
        {
            case "gelato":
                if (material is null) {
                    break;
                }
                gelatoMaterial = material;
                itemObject.GetComponent<Renderer>().material = material;
                
                itemObject.SetActive(true);
                break; //gelato and cone might not be used
            case "box":
                getGameObject(itemObject).SetActive(false);
                itemObject.SetActive(true);
                break;
            case "cone":
                itemObject.SetActive(true);
                break;
            default:
                itemObject.SetActive(true);
                break;
        }

    }

    List<string> gelatoTypes = new List<string>(){
        "GelatoA",
        "GelatoB"
    };
    List<string> coneTypes = new List<string>(){
        "Bowl",
        "Wafer"
    };
    string getItemType(string itemName)
    {

        foreach (string item in gelatoTypes) 
        {
            if (item == itemName) 
            {
                return "gelato";
            }
        }
        foreach (string item in coneTypes)
        {
            if (item == itemName)
            {
                return "cone";
            }
        }
        return "box";
    }

    public GameObject getGameObject(GameObject playerObject)
    {
        switch (heldItemType)
        {
            case "box":
                foreach (GameObject envObject in playerObjectRef.Keys)
                {
                    if(playerObjectRef[envObject] == playerObject)
                    {
                        return envObject;
                    }
                }
                return null;
            default:
                return null; //Should not be called for a gelato because there is no need to find the game object
        }
        
    }

    public GameObject getPlayerObject(GameObject gameObject, string itemType = "box")
    {
        switch (itemType)
        {
            case "box":
                return playerObjectRef[gameObject];
            case "gelato":
                return gelatoPlayerObject;
            case "cone":
                return playerObjectRef[gameObject];
            default:
                return null;
        }
    }
}