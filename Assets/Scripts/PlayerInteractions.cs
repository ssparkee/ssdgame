using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private List<Collider> collisions = new List<Collider>();

    HeldItem heldItem;

    // Start is called before the first frame update
    void Start()
    {
        heldItem = GetComponent<HeldItem>();
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
        List<Collider> collisionsToRemove = new List<Collider>();
        foreach (Collider collision in collisions) //Loop through all different elements being collided with
        {
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
            if(collision.gameObject.CompareTag("Box"))
            {
                Transform boxParent = collision.transform.parent;
                
                heldItem.setHeldItem("Box", heldItem.getPlayerObject(boxParent.gameObject));

                collisionsToRemove.Add(collision);
            }
            if(collision.gameObject.CompareTag("Box2"))
            {
                Transform boxParent = collision.transform.parent;
                
                heldItem.setHeldItem("Box2", heldItem.getPlayerObject(boxParent.gameObject));

                collisionsToRemove.Add(collision);
            }
            if(collision.gameObject.CompareTag("GelatoTubA"))
            {
                Transform boxParent = collision.transform.parent;
                
                heldItem.setHeldItem("GelatoA", heldItem.getPlayerObject(boxParent.gameObject));
            }
        }
        foreach(Collider collision in collisionsToRemove)
        {
            collisions.Remove(collision);
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
