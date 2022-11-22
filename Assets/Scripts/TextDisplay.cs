using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextDisplay : MonoBehaviour
{
    public GameObject textObject;
    
    TextMeshProUGUI textGui;

    public double displayInterval = 0.01;
    public double lineInterval = 0.5;
    // Start is called before the first frame update
    void Start()
    {
        textGui = textObject.GetComponent<TextMeshProUGUI>();

        displayLine("hello this is a test hey jamie does this look good thearjekatlrek atkleajtkjtkleajtkejakl");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void displayLine(string line, double interval = -1)
    {
        if (interval == -1)
        {
            interval = displayInterval;
        }
        
        StartCoroutine(display(line, (float)interval));
    }

    public void displayMultipleLines(string[] lines, double interval = -1, double timeBetweenLines = -1)
    {
        if (interval == -1)
        {
            interval = displayInterval;
        }
        if (timeBetweenLines == -1)
        {
            timeBetweenLines = lineInterval;
        }

        StartCoroutine(loopThroughLines(lines, interval: (float)interval, timeBetweenLines: (float)timeBetweenLines));
    }

    IEnumerator loopThroughLines(string[] lines, float interval, float timeBetweenLines)
    {
        foreach (string line in lines)
        {
            StartCoroutine(display(line, interval:interval));
            yield return new WaitForSeconds(timeBetweenLines);
        }
        yield break;
    }
    IEnumerator display(string line, float interval)
    {
        textGui.text = "";
        foreach (char character in line)
        {
            textGui.text += character;
            yield return new WaitForSeconds(interval);
        }
        yield break;
    }


}
