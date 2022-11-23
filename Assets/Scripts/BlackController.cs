using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackController : MonoBehaviour
{
    GameObject blackMan;
    BlackMovement blackScript;
    // Start is called before the first frame update
    void Start()
    {
        blackMan = transform.Find("character").gameObject;
        blackScript = blackMan.GetComponent<BlackMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, 1000) == 500)
        {
            blackScript.BlackMovementEnabled = true;
            gameObject.SetActive(true);
            blackScript.goToStart();
        }
        if (blackScript.BlackMovementEnabled)
        {
            blackScript.move();
        }
    }
}
