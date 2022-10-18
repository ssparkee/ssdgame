using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackMovement : MonoBehaviour
{
    public GameObject player;
    public float speed = 0.5f;
    public float speedMultiplier = 0.3f;
    public bool movementEnabled = false;
    private float velocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Transform playerTransform;

        playerTransform = player.transform;

        playerTransform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);

        transform.LookAt(playerTransform);

        velocity = speed * Mathf.Pow(Vector3.Distance(playerTransform.position, transform.position) * speedMultiplier, 2) * Time.deltaTime;

        if(movementEnabled) {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, velocity);
        }
    }
}
