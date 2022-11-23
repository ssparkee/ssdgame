using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

class Customer
{
    
    public void setup(string gelato, Vector3 startingPosition, Texture faceTexture)
    {

    }
}
/*

Customer needs to:
Walk up to a spot. This x, y will be passed in from the parent, as well as the starting position.
It will be a preset.
It will have a texture of the face to display.
It will also have a string of the gelato its making

The parent will have functions to generate all of these.
It will have a list of all the customers currently there.
When one customer gets served it will process this in the main script and get it to walk out
*/