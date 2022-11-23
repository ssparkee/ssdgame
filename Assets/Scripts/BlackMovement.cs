using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackMovement : MonoBehaviour
{
    private GameObject player;
    private PlayerFlashlight playerFlashlight;
    private Rigidbody blackBody;
    public float BlackSpeed = 30f;
    public bool BlackMovementEnabled = false;
    private bool flashedByPlayer = false;
    private float velocity;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        player = GameObject.FindWithTag("Player");
        playerFlashlight = player.GetComponent<PlayerFlashlight>();
        blackBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Torch"))
        {
            flashedByPlayer = true;
            Debug.Log("Damn, I'm dead");
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Torch"))
        {
            flashedByPlayer = false;
        }
    }

    public void move()
    {
        Vector3 playerPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);

        transform.LookAt(player.transform);

        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        velocity = BlackSpeed * Mathf.Pow(Vector3.Distance(playerPosition, transform.position) / 100, 2) * Time.deltaTime;

        if (BlackMovementEnabled && !(playerFlashlight.flashlightState == true && flashedByPlayer == true))
        { //If movement is enabled and it is not being flashed by an active flashlight move
            this.transform.position = Vector3.MoveTowards(transform.position, playerPosition, velocity);
        } else {
            stop();
        }
    }

    public void stop()
    {
        BlackMovementEnabled = false;
        gameObject.SetActive(false);
        goToStart(new Vector3(0,0,0));
    }

    public void goToStart(Vector3 startLocation)
    {
        transform.localPosition = startLocation;
    }
}
