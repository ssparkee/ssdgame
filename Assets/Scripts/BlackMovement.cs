using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackMovement : MonoBehaviour
{
    private GameObject player;
    private PlayerFlashlight playerFlashlight;
    private Rigidbody blackBody;
    public float BlackSpeed = 0.5f;
    public float BlackSpeedMultiplier = 0.3f;
    public bool BlackMovementEnabled = false;
    private bool flashedByPlayer = false;
    private float velocity;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerFlashlight = player.GetComponent<PlayerFlashlight>();
        blackBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);

        transform.LookAt(player.transform);

        transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);

        velocity = BlackSpeed * Mathf.Pow(Vector3.Distance(playerPosition, transform.position) * BlackSpeedMultiplier, 2) * Time.deltaTime;

        if(BlackMovementEnabled && !(playerFlashlight.flashlightState == true && flashedByPlayer == true)) { //If movement is enabled and it is not being flashed by an active flashlight move
            this.transform.position = Vector3.MoveTowards(transform.position, playerPosition, velocity);
            blackBody.isKinematic = false;
        } else {
            blackBody.isKinematic = true;
        }
    }
    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Torch"))
        {
            flashedByPlayer = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Torch"))
        {
            flashedByPlayer = false;
        }
    }
}
