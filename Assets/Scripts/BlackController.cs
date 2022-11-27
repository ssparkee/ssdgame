using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlackController : MonoBehaviour
{
    GameObject blackMan;
    BlackMovement blackScript;
    public int spawnChance = 1000;
    public float startingIntensity;
    public GameObject storeLight1;
    public GameObject storeLight2;
    public GameObject storeLight3;
    // Start is called before the first frame update

    public float minIntensity = 0f;
    public float maxIntensity = 1f;

    [Range(1, 50)]
    public int smoothing = 5;

    // Continuous average calculation via FIFO queue
    // Saves us iterating every time we update, we just change by the delta
    Queue<float> smoothQueue;
    float lastSum = 0;

    public GameObject moneyManagerObject;
    MoneyManager moneyManager;

    public bool lightsFlickering;
    
    public GameObject player;
    AudioSource audioListener;
    public AudioClip whisper;
    public AudioClip knock;
    public AudioClip flickering;

    void Start()
    {
        startingIntensity = storeLight1.GetComponent<Light>().intensity;
        blackMan = transform.Find("character").gameObject;
        blackScript = blackMan.GetComponent<BlackMovement>();
        moneyManager = moneyManagerObject.GetComponent<MoneyManager>();
        audioListener = player.GetComponent<AudioSource>();
        lightsFlickering = false;

        smoothQueue = new Queue<float>(smoothing);

        audioListener.loop = true;
        audioListener.clip = flickering;
        audioListener.Play();
    }

    // Update is called once per frame
    void Update()
    {

        if (moneyManager.money >= 5)
        {
            if(lightsFlickering == false)
            {
                audioListener.PlayOneShot(knock);
            }
            lightsFlickering = true;
        }
        if (lightsFlickering)
        {
            flickerLights();
        }

        if (Random.Range(0, spawnChance) == spawnChance - 1 && SceneManager.GetActiveScene().buildIndex == 1 && lightsFlickering == false && !blackScript.gameObject.activeSelf)
        {
            blackScript.BlackMovementEnabled = true;
            blackMan.SetActive(true);
            blackScript.goToStart(randomStartPoint());
            audioListener.PlayOneShot(whisper);
        }
        if (blackScript.BlackMovementEnabled)
        {
            blackScript.move();
        }

        
    }

    void flickerLights()
    {
        while (smoothQueue.Count >= smoothing) {
            lastSum -= smoothQueue.Dequeue();
        }

        // Generate random new item, calculate new average
        float newVal = Random.Range(minIntensity, maxIntensity);
        smoothQueue.Enqueue(newVal);
        lastSum += newVal;

        // Calculate new smoothed average
        storeLight1.GetComponent<Light>().intensity = lastSum / (float)smoothQueue.Count;
        storeLight2.GetComponent<Light>().intensity = lastSum / (float)smoothQueue.Count;
        storeLight3.GetComponent<Light>().intensity = lastSum / (float)smoothQueue.Count;
    }

    Vector3 randomStartPoint()
    {
        int i = Random.Range(1,3);
        
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
