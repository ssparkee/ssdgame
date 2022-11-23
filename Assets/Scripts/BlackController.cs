using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackController : MonoBehaviour
{
    GameObject blackMan;
    BlackMovement blackScript;
    public int spawnChance = 1000;
    // Start is called before the first frame update
    void Start()
    {
        blackMan = transform.Find("character").gameObject;
        blackScript = blackMan.GetComponent<BlackMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, spawnChance) == spawnChance - 1)
        {
            blackScript.BlackMovementEnabled = true;
            blackMan.SetActive(true);
            blackScript.goToStart(randomStartPoint());
        }
        if (blackScript.BlackMovementEnabled)
        {
            blackScript.move();
        }
    }

    Vector3 randomStartPoint()
    {
        int i = Random.Range(1,2);
        
        switch(i)
        {
            case 1:
                return new Vector3(0,0,0);
            case 2:
                return new Vector3(0,0,1110);
            default:
                return new Vector3(0,0,0);
        }
    }
}
