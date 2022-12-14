using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteractions : MonoBehaviour
{
    public Material gelatoAMaterial;
    public Material gelatoBMaterial;
    public Material gelatoCMaterial;
    private List<Collider> collisions = new List<Collider>();

    HeldItem heldItem;
    ConeSquare coneSquare;
    public GameObject coneSquareMain;
    public bool enteredBench;

    // Start is called before the first frame update
    void Start()
    {
        enteredBench = false;
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
            dropItem(collisions);
        }
    }

    void dropItem(List<Collider> collisions)
    {
        Collider collision = checkClosestCollider(collisions);

        if(collision is null)
        {
            heldItem.dropHeldItem();
        } else {
            if(collision.gameObject.CompareTag("GelatoA"))
            {
                if(!coneSquare.removeGelato("A"))
                {
                    heldItem.dropHeldItem();
                }
            } else if(collision.gameObject.CompareTag("GelatoB"))
            {
                if(!coneSquare.removeGelato("B"))
                {
                    heldItem.dropHeldItem();
                }
            } else if (collision.gameObject.CompareTag("GelatoC"))
            {
                if (!coneSquare.removeGelato("C"))
                {
                    heldItem.dropHeldItem();
                }
            } else
            {
                heldItem.dropHeldItem();
            }
        }
    }

    
    void checkInteractions(List<Collider> collisions)
    {
        if (collisions.Count == 0) {
            return;
        }

        List<Collider> collisionsToRemove = new List<Collider>();
        
        Collider collision = checkClosestCollider(collisions);
        
        //DOOR COLLISION
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
        } else if (collision.gameObject.CompareTag("GelatoButton"))
        {
            Transform buttonRef = collision.transform.parent.Find("GelatoButton");
            Animator buttonAnimator = buttonRef.GetComponent<Animator>();

            buttonAnimator.Play("GelatoButtonPress");

            coneSquare.buttonPressed();
        }
        //GELATO TABLE COLLISION
        else if(collision.gameObject.CompareTag("GelatoA") && heldItem.holdingItem)
        {
            if (heldItem.heldItemType.ToLower() == "cone")
            {
                //Create a new gelato element on the cone square. Make sure to check that there isnt already a cone placed
                coneSquare.placeCone("A", heldItem.heldItemName.ToLower(), heldItem);
                return;
            } 
            if (heldItem.heldItemType.ToLower() == "gelato" && coneSquare.squareHasCone("A"))
            {
                coneSquare.placeGelato("A", heldItem.gelatoMaterial, heldItem.heldItemName.ToLower(), heldItem);
                return;
            }
        } else if(collision.gameObject.CompareTag("GelatoB") && heldItem.holdingItem)
        {
            if (heldItem.heldItemType.ToLower() == "cone")
            {
                //Create a new gelato element on the cone square. Make sure to check that there isnt already a cone placed
                coneSquare.placeCone("B", heldItem.heldItemName.ToLower(), heldItem);
                return;
            } 
            if (heldItem.heldItemType.ToLower() == "gelato" && coneSquare.squareHasCone("B"))
            {
                coneSquare.placeGelato("B", heldItem.gelatoMaterial, heldItem.heldItemName.ToLower(), heldItem);
                return;
            }
        } else if (collision.gameObject.CompareTag("GelatoC") && heldItem.holdingItem)
        {
            if (heldItem.heldItemType.ToLower() == "cone")
            {
                //Create a new gelato element on the cone square. Make sure to check that there isnt already a cone placed
                coneSquare.placeCone("C", heldItem.heldItemName.ToLower(), heldItem);
                return;
            }
            if (heldItem.heldItemType.ToLower() == "gelato" && coneSquare.squareHasCone("C"))
            {
                coneSquare.placeGelato("C", heldItem.gelatoMaterial, heldItem.heldItemName.ToLower(), heldItem);
                return;
            }
        }
        //ITEM COLLISIONS
        else if(!heldItem.holdingItem)
        {
            if(collision.gameObject.CompareTag("Box"))
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
                
                heldItem.setHeldItem("GelatoA", heldItem.getPlayerObject(boxParent.gameObject, itemType: "gelato"), material: gelatoAMaterial);
            }
            else if(collision.gameObject.CompareTag("GelatoTubB"))
            {
                Transform boxParent = collision.transform.parent;
                
                heldItem.setHeldItem("GelatoB", heldItem.getPlayerObject(boxParent.gameObject, itemType: "gelato"), material: gelatoBMaterial);
            }
            else if (collision.gameObject.CompareTag("GelatoTubC"))
            {
                Transform boxParent = collision.transform.parent;

                heldItem.setHeldItem("GelatoC", heldItem.getPlayerObject(boxParent.gameObject, itemType: "gelato"), material: gelatoCMaterial);
            }
            else if(collision.gameObject.CompareTag("Wafer"))
            {
                Transform boxParent = collision.transform.parent;
                
                heldItem.setHeldItem("Wafer", heldItem.getPlayerObject(boxParent.gameObject, itemType: "cone"));
            }
            else if(collision.gameObject.CompareTag("Bowl"))
            {
                Transform boxParent = collision.transform.parent;
                
                heldItem.setHeldItem("Bowl", heldItem.getPlayerObject(boxParent.gameObject, itemType: "cone"));
            }
        }
        

        foreach(Collider collision2 in collisionsToRemove)
        {
            collisions.Remove(collision2);
        }
        collisionsToRemove.Clear();
        
    }

    Collider checkClosestCollider(List<Collider> collisions)
    {
        int smallestDistance = 1000;

        Collider collision;
        try
        {
            collision = collisions[0];
        } catch {
            return null;
        }

        foreach (Collider collision2 in collisions)
        {
            if ((int)Vector3.Distance(transform.position * 10, collision2.transform.position * 10) < smallestDistance)
            {
                smallestDistance = (int)Vector3.Distance(transform.position * 10, collision2.transform.position * 10);
                collision = collision2;
            }
        }
        return collision;
    }

    void checkCollisions(List<Collider> collisions)
    {
        foreach (Collider collision in collisions)
        {
            if (collision.gameObject.CompareTag("Black"))
            {
                //Debug.Log("You are dead");
            }
            if (collision.gameObject.CompareTag("BenchCheck"))
            {
                enteredBench = true;
            }
            if (collision.gameObject.CompareTag("BadDoor"))
            {
                SceneManager.LoadScene(4);
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
