using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private List<Collider> collisions = new List<Collider>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact")) //If E is pressed check if the player is colliding with any triggers
        {
            checkCollisions(collisions);
        }
    }

    void checkCollisions(List<Collider> collisions)
    {
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
        }
    }

    void OnTriggerEnter(Collider collision) 
    {
        if (collision.gameObject.CompareTag("Door")) 
        {
            collisions.Add(collision); //If entering a door trigger add that element to the list
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Door") && collisions.Contains(collision))
        {
            collisions.Remove(collision); //If exiting a door trigger remove that element from the list. triggerenter and exit can probably be removed of if statements
        }
    }
}
