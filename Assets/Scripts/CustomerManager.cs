using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public List<Customer> currentCustomers = new List<Customer>();
    public GameObject customerPrefab;
    public int customerChance = 700;
    public GameObject textManager; //TODO: ASSIGN THIS
    TextDisplay textdisplay;

    // Start is called before the first frame update
    void Start()
    {
        textdisplay = textManager.GetComponent<TextDisplay>();
    }

    // Update is called once per frame
    void Update()
    {
        moveCustomersUp();

        if(Random.Range(0, customerChance) == customerChance - 1)
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

    void addCustomer()
    {
        if (currentCustomers.Count >= 4)
        {
            return;
        }
        Customer i = new Customer();
        i.setup(generateGelato(), null, Instantiate(customerPrefab, transform.position, Quaternion.identity), textdisplay);
        i.customerGameObject.transform.parent = transform;
        currentCustomers.Add(i);

        //currentCustomers[currentCustomers.IndexOf(i)].moveForwards(getQueuePosition());
    }

    public void removeFrontCustomer(bool correctIceCream)
    {
        currentCustomers.Remove(currentCustomers[0]);

        if (correctIceCream) //TODO: good job bad job
        {
            //good job
            textdisplay.displayLine("Thanks!");

        } else {
            //bad job
            textdisplay.displayLine("What the hell? This isn't what I asked for!");
        }
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

    List<string> generateGelato()
    {
        //Random.Range(0,10) returns a number from 0-9 (inclusive, exclusive)
        List<string> gelatoString = new List<string>();
        for (int x = 0; x <= Random.Range(0,2); x++)
        {
            gelatoString.Add(generateGelatoString());
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
        string cone;
        string scoop1 = "";
        string scoop2 = "";
        string scoop3 = "";

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

        i = Random.Range(1, 2);
        if (i == 1)
        {
            scoop1 = "scoopA";
        }
        else if (i == 2)
        {
            scoop1 = "scoopB";
        }

        i = Random.Range(1, 3);
        if (i == 1)
        {
            scoop2 = "scoopA";
        }
        else if (i == 2)
        {
            scoop2 = "scoopB";
        }
        else
        {
            return $"{cone}:{scoop1}";
        }

        i = Random.Range(1, 3);
        if (i == 1)
        {
            scoop3 = "scoopA";
        }
        else if (i == 2)
        {
            scoop3 = "scoopB";
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
    }
    public void moveForwards(Vector3 target, int position)
    {
        positionInQueue = position;
        currentlyMoving = true;
        if (customerGameObject.transform.localPosition == target)
        {
            currentlyMoving = false;

            if (!hasOrdered && (target == new Vector3(-29.82f, 0, -822f)))
            {
                textdisplay.displayLine(createString(gelatoString));
                hasOrdered = true;
            }

            return;
        }

        customerGameObject.transform.localPosition = Vector3.MoveTowards(customerGameObject.transform.localPosition, target, Time.deltaTime * 100);
    }
    string createString(List<string> list)
    {
        if (list.Count == 1)
        {
            return $"Can i get a {list[0]}";
        }
        return $"Can I get a {list[0]} and a {list[1]}";
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