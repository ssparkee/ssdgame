using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomerManager : MonoBehaviour
{
    public List<Customer> currentCustomers = new List<Customer>();
    public GameObject customerPrefab;
    public int customerChance = 700;
    public GameObject textManager; //TODO: ASSIGN THIS
    TextDisplay textdisplay;
    public GameObject moneyManagerGameObject;
    MoneyManager moneyManager;

    // Start is called before the first frame update
    void Start()
    {
        textdisplay = textManager.GetComponent<TextDisplay>();
        moneyManager = moneyManagerGameObject.GetComponent<MoneyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        moveCustomersUp();

        if(Random.Range(0, customerChance) == customerChance - 1 && SceneManager.GetActiveScene().buildIndex == 1)
        {
            addCustomer();
        }

        //generateGelato();
    }

    void moveCustomersUp()
    {
        foreach (Customer customer in currentCustomers)
        {
            customer.moveForwards(getNextSpot(currentCustomers.IndexOf(customer)), currentCustomers.IndexOf(customer));
        }
    }

    public void addCustomer(bool onlyOne = false)
    {
        if (currentCustomers.Count >= 4)
        {
            return;
        }
        Customer i = new Customer();
        i.setup(generateGelato(onlyOne), null, Instantiate(customerPrefab, transform.position, Quaternion.identity), textdisplay);
        i.customerGameObject.transform.parent = transform;
        currentCustomers.Add(i);

        //currentCustomers[currentCustomers.IndexOf(i)].moveForwards(getQueuePosition());
    }

    public void removeFrontCustomer(bool correctIceCream)
    {
        if (correctIceCream) //TODO: good job bad job
        {
            //good job
            textdisplay.displayLine("Thanks!");
            moneyManager.addMoney();

        } else {
            //bad job
            textdisplay.displayLine("What the hell? This isn't what I asked for!");
        }
        currentCustomers[0].leaveStore();

        currentCustomers.Remove(currentCustomers[0]);
    }

    public List<string> getGelatosToMake()
    {
        return currentCustomers[0].gelatoString;
    }

    Vector3 getNextSpot(int index = -1) //If indx is not passed in just give the next available spot
    {
        if (index == -1)
        {
            index = currentCustomers.Count;
        }
        switch (index)
        {
            case 0:
                return new Vector3(-29.82f,0,-822f);
            case 1:
                return new Vector3(-29.82f,0,-775f);
            case 2:
                return new Vector3(-29.82f,0,-740f);
            case 3:
                return new Vector3(-29.82f,0,-710f);
            default:
                return new Vector3(-29.82f,0,-822f);
        }
    }

    List<string> generateGelato(bool onlyOne)
    {
        //Random.Range(0,10) returns a number from 0-9 (inclusive, exclusive)
        List<string> gelatoString = new List<string>();
        if (onlyOne)
        {
            gelatoString.Add(generateGelatoString());
        }
        else
        {
            for (int x = 0; x <= Random.Range(0,2); x++)
            {
                gelatoString.Add(generateGelatoString());
            }
        }
        
        //printList(gelatoString);
        return gelatoString;
        //TODO: test wether this works. Does it do a full thing or nah?
    }

    void printList(List<string> list)
    {
        int i = Random.Range(0,10000);

        foreach (string x in list)
        {
            Debug.Log(i + " " + x);
        }
    }

    string generateGelatoString()
    {
        string cone = "wafer";
        string scoop1 = "gelatoa";
        string scoop2 = "gelatoa";
        string scoop3 = "gelatoa";

        int i = 1;

        i = Random.Range(1, 3);
        if (i == 1)
        {
            cone = "wafer";
        }
        else
        {
            cone = "bowl";
        }

        i = Random.Range(1, 4);
        if (i == 1)
        {
            scoop1 = "gelatoa";
        }
        else if (i == 2)
        {
            scoop1 = "gelatob";
        }
        else if (i == 3)
        {
            scoop1 = "gelatoc";
        } else {
            scoop1 = "gelatoa";
        }

        i = Random.Range(1, 5);
        if (i == 1)
        {
            scoop2 = "gelatoa";
        }
        else if (i == 2)
        {
            scoop2 = "gelatob";
        }
        else if (i == 3)
        {
            scoop1 = "gelatoc";
        }
        else
        {
            return $"{cone}:{scoop1}";
        }

        i = Random.Range(1, 5);
        if (i == 1)
        {
            scoop3 = "gelatoa";
        }
        else if (i == 2)
        {
            scoop3 = "gelatob";
        }
        else if (i == 3)
        {
            scoop1 = "gelatoc";
        }
        else
        {
            return $"{cone}:{scoop1}:{scoop2}";
        }
        return $"{cone}:{scoop1}:{scoop2}:{scoop3}";
    }
}

public class Customer
{
    public GameObject customerGameObject;
    Animator customerAnimator;
    public List<string> gelatoString;

    public bool currentlyMoving;
    public Vector3 movingTo;

    public bool hasOrdered;
    public int positionInQueue;
    TextDisplay textdisplay;

    public void setup(List<string> gelato, Texture faceTexture, GameObject customer, TextDisplay text)
    {
        gelatoString = gelato;
        customerGameObject = customer;
        textdisplay = text;
        currentlyMoving = false;
        hasOrdered = false;

        customerGameObject.transform.RotateAround(customerGameObject.transform.position, customerGameObject.transform.up, 180f);

        customerAnimator = customerGameObject.GetComponent<Animator>();
    }
    public void moveForwards(Vector3 target, int position)
    {
        positionInQueue = position;
        currentlyMoving = true;

        customerAnimator.SetBool("IsWalking", true);

        if (customerGameObject.transform.localPosition == target)
        {
            currentlyMoving = false;

            customerAnimator.SetBool("IsWalking", false);

            if (!hasOrdered && (target == new Vector3(-29.82f, 0, -822f)))
            {
                textdisplay.displayLine(createString(gelatoString));
                hasOrdered = true;
            }

            return;
        }

        

        customerGameObject.transform.localPosition = Vector3.MoveTowards(customerGameObject.transform.localPosition, target, Time.deltaTime * 100);
    }
    public void leaveStore()
    {
        customerGameObject.transform.localPosition = Vector3.MoveTowards(customerGameObject.transform.localPosition, new Vector3(0,0,0), Time.deltaTime * 100);

        customerGameObject.SetActive(false);
    }
    string createString(List<string> list)
    {
        if (list.Count == 1)
        {
            string[] stringFormatted = list[0].Split(":");
            switch (stringFormatted.Length)
            {
                case 2:
                    return $"Can i get a {getScoopName(stringFormatted[1])} on a {getScoopName(stringFormatted[0])}";
                case 3:
                    return $"Can i get a {getScoopName(stringFormatted[1])} and a {getScoopName(stringFormatted[2])} on a {getScoopName(stringFormatted[0])}";
                case 4:
                    return $"Can i get a {getScoopName(stringFormatted[1])}, {getScoopName(stringFormatted[2])} and a {getScoopName(stringFormatted[3])} on a {getScoopName(stringFormatted[0])}";
            }
            
        } if (list.Count == 2)
        {
            string[] stringFormatted1 = list[0].Split(":");
            string[] stringFormatted2 = list[1].Split(":");
            
            string stringToReturn = "";
            if (stringFormatted1.Length == 2)
            {
                stringToReturn = $"Can i get a {getScoopName(stringFormatted1[1])} on a {stringFormatted1[0]}";
            } else if (stringFormatted1.Length == 3)
            {
                stringToReturn = $"Can i get a {getScoopName(stringFormatted1[1])} and a {getScoopName(stringFormatted1[2])} on a {stringFormatted1[0]}";
            } else if (stringFormatted1.Length == 4)
            {
                stringToReturn = $"Can i get a {getScoopName(stringFormatted1[1])}, {getScoopName(stringFormatted1[2])} and a {getScoopName(stringFormatted1[3])} on a {stringFormatted1[0]}";
            }

            if (stringFormatted2.Length == 2)
            {
                stringToReturn += $" as well as a {getScoopName(stringFormatted2[1])} on a {stringFormatted2[0]}";
            } else if (stringFormatted2.Length == 3)
            {
                stringToReturn += $" as well as a {getScoopName(stringFormatted2[1])} and a {getScoopName(stringFormatted2[2])} on a {stringFormatted2[0]}";
            } else if (stringFormatted2.Length == 4)
            {
                stringToReturn += $" as well as a {getScoopName(stringFormatted2[1])}, {getScoopName(stringFormatted2[2])} and a {getScoopName(stringFormatted2[3])} on a {stringFormatted2[0]}";
            } 

            return stringToReturn;
        }
        return $"Can I get a {list[0]} and a {list[1]}";
    }
    string getScoopName(string scoop)
    {
        switch (scoop)
        {
            case "gelatoa":
                return "mint chocolate scoop";
            case "gelatob":
                return "strawberry scoop";
            case "gelatoc":
                return "vanilla scoop";
            default:
                return scoop;
        }
    }
}
/*

Customer needs to:
Walk up to a spot. This x, y will be passed in from the parent, as well as the starting position.
It will be a preset.
It will have a texture of the face to display.
It will also have a string of the gelato its making

The parent will have functions to generate all of these.
It will have a list of all the customers currently there.
When one customer gets served it will process this in the main script and get it to walk out
*/