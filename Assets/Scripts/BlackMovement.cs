using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackMovement : MonoBehaviour
{
    private GameObject player;
    public float BlackSpeed = 0.5f;
    public float BlackSpeedMultiplier = 0.3f;
    public bool BlackMovementEnabled = false;
    private float velocity;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);

        transform.LookAt(player.transform);

        transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);

        velocity = BlackSpeed * Mathf.Pow(Vector3.Distance(playerPosition, transform.position) * BlackSpeedMultiplier, 2) * Time.deltaTime;

        if(BlackMovementEnabled) {
            this.transform.position = Vector3.MoveTowards(transform.position, playerPosition, velocity);
        }
    }
    void OnTriggerEnter(Collider collision) 
    {
        if (collision.gameObject.CompareTag("Torch"))
        {
            Debug.Log("black guy dead");
        }
    }
}
