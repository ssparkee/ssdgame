using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public Material gelatoAMaterial;
    public Material gelatoBMaterial;
    private List<Collider> collisions = new List<Collider>();

    HeldItem heldItem;
    ConeSquare coneSquare;
    public GameObject coneSquareMain;

    // Start is called before the first frame update
    void Start()
    {
        heldItem = GetComponent<HeldItem>();
        coneSquare = coneSquareMain.GetComponent<ConeSquare>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact")) //If E is pressed check if the player is colliding with any triggers
        {
            checkInteractions(collisions);
        }
        checkCollisions(collisions);
        if (Input.GetButtonDown("Drop"))
        {
            //drop current item
            heldItem.dropHeldItem();
        }
    }

    void checkInteractions(List<Collider> collisions)
    {
        if (collisions.Count == 0) {
            return;
        }

        List<Collider> collisionsToRemove = new List<Collider>();
        
        int smallestDistance = 1000;

        Collider collision = collisions[0];

        foreach (Collider collision2 in collisions)
        {
            if ((int)Vector3.Distance(transform.position * 10, collision2.transform.position * 10) < smallestDistance)
            {
                smallestDistance = (int)Vector3.Distance(transform.position * 10, collision2.transform.position * 10);
                collision = collision2;
            }
        }

        if (collision.gameObject.CompareTag("Door")) //If colliidng with a door
        {
            Transform doorRef = collision.transform.parent.Find("DoorA");
            Animator doorAnimator = doorRef.GetComponent<Animator>(); //Get the animation element of the door
            
            if(doorAnimator.GetBool("IsOpen") == false) //If closed open it and vice versa
            {
                doorAnimator.SetBool("IsOpen", true);
            } else
            {
                doorAnimator.SetBool("IsOpen", false);
            }
        } 
        else if(collision.gameObject.CompareTag("GelatoA") && (heldItem.heldItemType == "cone"))
        {
            //Create a new gelato element on the cone square. Make sure to check that there isnt already a cone placed
            coneSquare.placeCone("A", heldItem.heldItemName.ToLower());
            heldItem.removeHeldItem();
        }
        else if(collision.gameObject.CompareTag("Box"))
        {
            Transform boxParent = collision.transform.parent;
            
            heldItem.setHeldItem("Box", heldItem.getPlayerObject(boxParent.gameObject));

            collisionsToRemove.Add(collision);
        }
        else if(collision.gameObject.CompareTag("Box2"))
        {
            Transform boxParent = collision.transform.parent;
            
            heldItem.setHeldItem("Box2", heldItem.getPlayerObject(boxParent.gameObject));

            collisionsToRemove.Add(collision);
        }
        else if(collision.gameObject.CompareTag("GelatoTubA"))
        {
            Transform boxParent = collision.transform.parent;
            
            heldItem.setHeldItem("GelatoA", heldItem.getPlayerObject(boxParent.gameObject, itemType: "gelato"), gelatoMaterial: gelatoAMaterial);
        }
        else if(collision.gameObject.CompareTag("GelatoTubB"))
        {
            Transform boxParent = collision.transform.parent;
            
            heldItem.setHeldItem("GelatoB", heldItem.getPlayerObject(boxParent.gameObject, itemType: "gelato"), gelatoMaterial: gelatoBMaterial);
        }
        else if(collision.gameObject.CompareTag("Wafer"))
        {
            Transform boxParent = collision.transform.parent;
            
            heldItem.setHeldItem("Wafer", heldItem.getPlayerObject(boxParent.gameObject, itemType: "cone"));
        }

        foreach(Collider collision2 in collisionsToRemove)
        {
            collisions.Remove(collision2);
        }
        collisionsToRemove.Clear();
        
    }

    void checkCollisions(List<Collider> collisions)
    {
        foreach (Collider collision in collisions)
        {
            if (collision.gameObject.CompareTag("Black"))
            {
                //Debug.Log("You are dead");
            }
        }
    }

    void OnTriggerEnter(Collider collision) 
    {
        collisions.Add(collision); //Add the collision to the list
    }

    void OnTriggerExit(Collider collision)
    {
        if (collisions.Contains(collision))
        {
            collisions.Remove(collision); //Remove collision from the list if its on there
        }
    }
}
