using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextDisplay : MonoBehaviour
{
    public GameObject textObject;
    public GameObject panelObject;
    
    TextMeshProUGUI textGui;
    RectTransform panelTransform;

    public double displayInterval = 0.01;
    public double lineInterval = 0.5;

    public bool displayingText = false;
    // Start is called before the first frame update
    void Start()
    {
        textGui = textObject.GetComponent<TextMeshProUGUI>();
        panelTransform = panelObject.GetComponent<RectTransform>();

        //displayLine("Hey can i I get a wafer cone with red scoop and green scoop Hey can i I get a wafer cone with red scoop and green scoop Hey can i I get a wafer cone with red scoop and green scoop");
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
        

        waitForDisplay(line, (float)interval);
    }

    IEnumerator waitForDisplay(string line, float interval)
    {
        while (displayingText == true)
        {
            yield return null;
        }

        StartCoroutine(display(line, interval));
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

        if(line.Length >= 120)
        {
            panelTransform.offsetMin = new Vector2(panelTransform.offsetMin.x, 1030); //520, 1030
        } else {
            panelTransform.offsetMin = new Vector2(panelTransform.offsetMin.x, 1010); //540, 1010
        }
        foreach (char character in line)
        {
            textGui.text += character;
            yield return new WaitForSeconds(interval);
        }
        yield return new WaitForSeconds(3);

        displayingText = false;
        yield break;
    }


}
