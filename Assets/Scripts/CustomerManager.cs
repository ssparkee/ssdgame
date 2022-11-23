using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public List<Customer> currentCustomers = new List<Customer>();
    public GameObject customerPrefab;
    public int customerChance = 700;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        moveCustomersUp();

        if(Random.Range(0, customerChance) == customerChance - 1)
        {
            //addCustomer();
        }

        foreach (string gelato in generateGelato())
        {
            Debug.Log(gelato);
        }
    }

    void moveCustomersUp()
    {
        foreach (Customer customer in currentCustomers)
        {
            customer.moveForwards(getNextSpot(currentCustomers.IndexOf(customer)));
        }
    }

    void addCustomer()
    {
        if (currentCustomers.Count >= 4)
        {
            return;
        }
        Customer i = new Customer();
        i.setup(generateGelato(), null, Instantiate(customerPrefab, transform.position, Quaternion.identity));
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
        } else {
            //bad job
        }
    }

    public List<string> getGelatosToMake()
    {
        return currentCustomers[0].gelatoString;
    }

    Vector3 getQueueStart()
    {
        switch (currentCustomers.Count)
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

    Vector3 getNextSpot(int index)
    {
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

    List<string> generateGelato(string cone = "", string scoop1 = "", string scoop2 = "", string scoop3 = "")
    {
        List<string> gelatoString = new List<string>();
        for (int x = 0; x <= Random.Range(1,3); x++)
        {
            int i = 1;
            if (cone == "")
            {
                i = Random.Range(1,3);
                if (i == 1)
                {
                    cone = "wafer";
                } else {
                    cone = "bowl";
                }
            }
            if (scoop1 == "")
            {
                i = Random.Range(1,3);
                if (i == 1)
                {
                    scoop1 = "scoopA";
                } else if (i == 2) {
                    scoop1 = "scoopB";
                }
            }
            if (scoop2 == "")
            {
                i = Random.Range(1,4);
                if (i == 1)
                {
                    scoop2 = "scoopA";
                } else if (i == 2) {
                    scoop2 = "scoopB";
                } else {
                    gelatoString.Add($"{cone}:{scoop1}");
                }
            }
            if (scoop3 == "")
            {
                i = Random.Range(1,4);
                if (i == 1)
                {
                    scoop3 = "scoopA";
                } else if (i == 2) {
                    scoop3 = "scoopB";
                } else {
                    gelatoString.Add($"{cone}:{scoop1}:{scoop2}");
                }
            }
            gelatoString.Add($"{cone}:{scoop1}:{scoop2}:{scoop3}");
        }

        return gelatoString;
        //TODO: test wether this works. Does it do a full thing or nah?
    }
}

public class Customer
{
    public GameObject customerGameObject;
    public List<string> gelatoString;

    public bool currentlyMoving;
    public Vector3 movingTo;

    public void setup(List<string> gelato, Texture faceTexture, GameObject customer)
    {
        gelatoString = gelato;
        customerGameObject = customer;
        currentlyMoving = false;
    }
    public void moveForwards(Vector3 target)
    {
        currentlyMoving = true;
        if(customerGameObject.transform.localPosition == target)
        {
            currentlyMoving = false;
            return;
        }

        customerGameObject.transform.localPosition = Vector3.MoveTowards(customerGameObject.transform.localPosition, target, Time.deltaTime * 100);
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