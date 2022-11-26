using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlashlight : MonoBehaviour
{
    public Material flashlightOff;
    public Material flashlightOn;

    public bool flashlightState = false;
    public AudioClip torchOn;
    public AudioClip torchOff;
    private Transform torch;
    private Light torchSpotLight;
    private Transform playerCamera;
    private GameObject flashlightModel;
    private AudioSource playerAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = transform.Find("Camera");
        torch = playerCamera.Find("Torch");
        torchSpotLight = torch.Find("TorchLight").Find("TorchSpotLight").gameObject.GetComponent<Light>();
        playerAudioSource = GetComponent<AudioSource>();
        flashlightModel = torch.Find("Flashlight").gameObject;

        
        torchSpotLight.enabled = false;   
        flashlightModel.SetActive(false);     
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Flashlight")) //If C is pressed toggle the flashlight state
        {
            if(!flashlightState) 
            {
                flashlightModel.SetActive(true);
                torchSpotLight.enabled = true;
                playerAudioSource.PlayOneShot(torchOn);        
                flashlightState = true;
            } else
            {
                flashlightModel.SetActive(false);
                torchSpotLight.enabled = false;   
                playerAudioSource.PlayOneShot(torchOff);          
                flashlightState = false;
            }
        }
    }
}
