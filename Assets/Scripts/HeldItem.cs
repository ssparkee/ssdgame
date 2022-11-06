using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldItem : MonoBehaviour
{
    [SerializeField] GameObject boxGameObject;
    [SerializeField] GameObject boxPlayerObject;
    [SerializeField] GameObject box2GameObject;
    [SerializeField] GameObject box2PlayerObject;
    [SerializeField] GameObject gelatoAPlayerObject;
    [SerializeField] GameObject gelatoAGameObject;
    
    Dictionary<string, GameObject> heldItem = new Dictionary<string, GameObject>();
    
    Dictionary<GameObject, GameObject> playerObjectRef = new Dictionary<GameObject, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        playerObjectRef.Add(boxPlayerObject, boxGameObject);
        playerObjectRef.Add(box2PlayerObject, box2GameObject);
        playerObjectRef.Add(gelatoAPlayerObject, gelatoAGameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Dictionary<string, GameObject> getHeldItem()
    {
        if(heldItem.Count == 1)
        {
            return heldItem;
        } else {
            return null;
        }
    }

    public void setHeldItem(string itemName, GameObject itemObject)
    {
        if(heldItem.Count == 0)
        {
            heldItem.Add(itemName, itemObject);

            if(itemName != "GelatoA")
            {
                getGameObject(itemObject).SetActive(false);
            }

            itemObject.SetActive(true);
            //pick up the item
        }
    }

    public void dropHeldItem()
    {
        if (getHeldItem() is object)
        {
            //Drop item
            foreach (GameObject heldGameObject in heldItem.Values)
            {
                heldGameObject.SetActive(false);

                GameObject gameObject = getGameObject(heldGameObject);

                string heldItemName = "";
                
                foreach (string name in heldItem.Keys)
                {
                    if(heldItem[name] == heldGameObject)
                    {
                        heldItemName = name;
                    }
                }

                if(gameObject is object && heldItemName != "GelatoA")
                {
                    gameObject.transform.position = transform.position;
                    gameObject.transform.rotation = transform.rotation;
                    gameObject.transform.Translate(0, 0, 2);
                    gameObject.SetActive(true);
                }
            }

            heldItem.Clear();
        }
    }

    public GameObject getGameObject(GameObject gameObject)
    {
        return playerObjectRef[gameObject];
    }

    public GameObject getPlayerObject(GameObject gameObject)
    {
        foreach (GameObject envObject in playerObjectRef.Keys)
        {
            if(playerObjectRef[envObject] == gameObject)
            {
                return envObject;
            }
        }
        return null;
    }

}
