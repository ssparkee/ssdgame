using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlashlight : MonoBehaviour
{
    public Material flashlightOff;
    public Material flashlightOn;
    public GameObject torchLight;
    public bool flashlightState = false;

    // Start is called before the first frame update
    void Start()
    {
        torchLight.GetComponent<Renderer>().material = flashlightOff;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Flashlight")) //If C is pressed toggle the flashlight state
        {
            if(!flashlightState) 
            {
                torchLight.GetComponent<Renderer>().material = flashlightOn;
                flashlightState = true;
            } else
            {
                torchLight.GetComponent<Renderer>().material = flashlightOff;
                flashlightState = false;
            }
        }
    }
}
