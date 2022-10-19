using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlashlight : MonoBehaviour
{
    public Material flashlightOff;
    public Material flashlightOn;
    public bool flashlightState = false;
    private GameObject torchBaseLight;
    private Transform torch;
    private Transform torchLight;
    private Transform playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        torch = transform.Find("Torch");
        torchBaseLight = torch.Find("TorchBase").Find("TorchBaseLight").gameObject;
        playerCamera = transform.Find("Camera");
        torchLight = torch.Find("TorchLight");
        torchBaseLight.GetComponent<Renderer>().material = flashlightOff;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 torchRotation = playerCamera.transform.rotation.eulerAngles;
        if(torchRotation.x < 345 && torchRotation.x > 180) {
            torchRotation.x = 345;
        } else if(torchRotation.x > 15 && torchRotation.x < 181) {
            torchRotation.x = 15;
        }
        torch.rotation = Quaternion.Euler(torchRotation);

        if(Input.GetButtonDown("Flashlight")) //If C is pressed toggle the flashlight state
        {
            if(!flashlightState) 
            {
                torchBaseLight.GetComponent<Renderer>().material = flashlightOn;
                flashlightState = true;
            } else
            {
                torchBaseLight.GetComponent<Renderer>().material = flashlightOff;
                flashlightState = false;
            }
        }
    }
}
