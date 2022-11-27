using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public GameObject textBoxManagerObject;
    public GameObject customerManagerObject;
    public GameObject moneyManagerObject;
    public GameObject playerObject;
    public GameObject coneSquareObject;
    TextDisplay textDisplay;
    CustomerManager customerManager;
    MoneyManager moneyManager;
    PlayerInteractions playerInteractions;
    ConeSquare coneSquare;
    AudioSource audioSource;
    public AudioClip zoltanTalking1;
    public AudioClip zoltanTalking2;
    public AudioClip zoltanTalking3;
    public AudioClip zoltanTalking4;
    public AudioClip zoltanTalking5;
    public AudioClip zoltanTalking6;
    public AudioClip zoltanTalking7;
    public AudioClip zoltanTalking8;
    public AudioClip zoltanTalking9;
    public AudioClip zoltanTalking10;
    public AudioClip zoltanTalking11;

    // Start is called before the first frame update
    void Start()
    {
        textDisplay = textBoxManagerObject.GetComponent<TextDisplay>();
        customerManager = customerManagerObject.GetComponent<CustomerManager>();
        moneyManager = moneyManagerObject.GetComponent<MoneyManager>();
        playerInteractions = playerObject.GetComponent<PlayerInteractions>();
        coneSquare = coneSquareObject.GetComponent<ConeSquare>();
        audioSource = playerObject.GetComponent<AudioSource>();

        StartCoroutine(tutorial());
        //customerManager.addCustomer(onlyOne:true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator tutorial()
    {
        textDisplay.displayLine("Hello! You must be the new employee. Welcome to Pure Gelato!");
        audioSource.PlayOneShot(zoltanTalking1);
        if (textDisplay.displayingText)
        {
            yield return new WaitUntil(notDisplaying);
        }
        audioSource.PlayOneShot(zoltanTalking2);
        textDisplay.displayLine("We've been struggling getting anyone brave enough for a night shift, so thank god you're here!");
        if (textDisplay.displayingText)
        {
            yield return new WaitUntil(notDisplaying);
        }
        audioSource.PlayOneShot(zoltanTalking3);
        textDisplay.displayLine("When you're ready, head over to the bench.");
        yield return new WaitUntil(enteredBench);
        audioSource.PlayOneShot(zoltanTalking4);
        textDisplay.displayLine("Start off by grabbing a wafer cone and putting it on the gelato square. Press E to pick items up, and press G to remove a gelato or to drop something.");
        yield return new WaitUntil(waferOnBench);
        audioSource.PlayOneShot(zoltanTalking5);
        textDisplay.displayLine("Put a scoop of ice cream on it. Grab any scoop you want from the bench and put it on the cone.");
        yield return new WaitUntil(oneScoopOnIceCream);
        audioSource.PlayOneShot(zoltanTalking6);
        textDisplay.displayLine("Good job! Finish it off by putting a strawberry scoop on the top.");
        yield return new WaitUntil(strawberryScoopOnSecond);
        audioSource.PlayOneShot(zoltanTalking7);
        textDisplay.displayLine("Congrats on making your first ice cream!");
        yield return new WaitUntil(notDisplaying);
        audioSource.PlayOneShot(zoltanTalking8);
        textDisplay.displayLine("I'll be taking this one, but I will still pay you - don't worry!");
        coneSquare.removeAllGelatos();
        moneyManager.addMoney();
        yield return new WaitUntil(notDisplaying);
        customerManager.addCustomer(onlyOne:true);
        audioSource.PlayOneShot(zoltanTalking9);
        textDisplay.displayLine("Here is your first customer. Make their order!");
        yield return new WaitUntil(noCustomers);
        yield return new WaitUntil(notDisplaying);
        if(moneyManager.money == 2)
        {
            audioSource.PlayOneShot(zoltanTalking10);
            textDisplay.displayLine("Congratulations on completing your training! You are all ready to start your first night shift.", waitAfter:4);
        } else {
            audioSource.PlayOneShot(zoltanTalking11);
            textDisplay.displayLine("Congratulations on failing your first real order! This finishes your training, but know that money will be deducted from your payslip for doing that.", waitAfter:4);
        }
        yield return new WaitUntil(notDisplaying);
        SceneManager.LoadScene(3);
    }

    bool notDisplaying()
    {
        return !textDisplay.displayingText;
    }

    bool enteredBench()
    {
        return playerInteractions.enteredBench;
    }
    bool waferOnBench()
    {
        if(coneSquare.gelatos[0].coneName == "wafer" || coneSquare.gelatos[1].coneName == "wafer")
        {
            return true;
        }
        return false;
    }
    bool oneScoopOnIceCream()
    {
        if(coneSquare.gelatos[0].numberOfScoops == 1 || coneSquare.gelatos[1].numberOfScoops == 1)
        {
            return true;
        }
        return false;
    }
    bool strawberryScoopOnSecond()
    {
        if(coneSquare.gelatos[0].gelatoScoops[1].scoopName == "gelatob" || coneSquare.gelatos[1].gelatoScoops[1].scoopName == "gelatob")
        {
            return true;
        }
        return false;
    }
    bool noCustomers()
    {
        if(customerManager.currentCustomers.Count == 0)
        {
            return true;
        }
        return false;
    }
}
