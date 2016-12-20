using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ObjectiveImage : MonoBehaviour
{

    public Sprite white;
    public Sprite blue;
    public Sprite green;
    public Sprite pink;
    public Sprite red;
    public Sprite yellow;

    public Sprite whiteFour;
    public Sprite blueFour;
    public Sprite greenFour;
    public Sprite pinkFour;
    public Sprite redFour;
    public Sprite yellowFour;

    public Sprite whiteThree;
    public Sprite blueThree;
    public Sprite greenThree;
    public Sprite pinkThree;
    public Sprite redThree;
    public Sprite yellowThree;

    public Sprite whiteFive;
    public Sprite blueFive;
    public Sprite greenFive;
    public Sprite pinkFive;
    public Sprite redFive;
    public Sprite yellowFive;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetImage(string color, int objectiveID, int objectiveCount)
    {
        if (objectiveID == 1)
        {
            if (color == "RED")
            {
                GetComponent<Image>().sprite = red;
            }
            else if(color == "WHITE"){
                GetComponent<Image>().sprite = white;
            }
            else if (color == "BLUE")
            {
                GetComponent<Image>().sprite = blue;
            }
            else if (color == "GREEN")
            {
                GetComponent<Image>().sprite = green;
            }
            else if (color == "PINK")
            {
                GetComponent<Image>().sprite = pink;
            }
            else if (color == "YELLOW")
            {
                GetComponent<Image>().sprite = yellow;
            }
        }
        else if (objectiveID == 2)
        {

            if (objectiveCount == 4)
            {
                if (color != null)
                {
                    if (color == "RED")
                    {
                        GetComponent<Image>().sprite = redFour;
                    }
                    else if (color == "WHITE")
                    {
                        GetComponent<Image>().sprite = whiteFour;
                    }
                    else if (color == "BLUE")
                    {
                        GetComponent<Image>().sprite = blueFour;
                    }
                    else if (color == "GREEN")
                    {
                        GetComponent<Image>().sprite = greenFour;
                    }
                    else if (color == "PINK")
                    {
                        GetComponent<Image>().sprite = pinkFour;
                    }
                    else if (color == "YELLOW")
                    {
                        GetComponent<Image>().sprite = yellowFour;
                    }
                } 
            }
            else if (objectiveCount == 3)
            {
                if (color != null)
                {
                    if (color == "RED")
                    {
                        GetComponent<Image>().sprite = redThree;
                    }
                    else if (color == "WHITE")
                    {
                        GetComponent<Image>().sprite = whiteThree;
                    }
                    else if (color == "BLUE")
                    {
                        GetComponent<Image>().sprite = blueThree;
                    }
                    else if (color == "GREEN")
                    {
                        GetComponent<Image>().sprite = greenThree;
                    }
                    else if (color == "PINK")
                    {
                        GetComponent<Image>().sprite = pinkThree;
                    }
                    else if (color == "YELLOW")
                    {
                        GetComponent<Image>().sprite = yellowThree;
                    }
                }
            }
            if (objectiveCount == 5)
            {
                if (color != null)
                {
                    if (color == "RED")
                    {
                        GetComponent<Image>().sprite = redFive;
                    }
                    else if (color == "WHITE")
                    {
                        GetComponent<Image>().sprite = whiteFive;
                    }
                    else if (color == "BLUE")
                    {
                        GetComponent<Image>().sprite = blueFive;
                    }
                    else if (color == "GREEN")
                    {
                        GetComponent<Image>().sprite = greenFive;
                    }
                    else if (color == "PINK")
                    {
                        GetComponent<Image>().sprite = pinkFive;
                    }
                    else if (color == "YELLOW")
                    {
                        GetComponent<Image>().sprite = yellowFive;
                    }
                }
            }
        }
    }
}
