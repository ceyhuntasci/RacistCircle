using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class LevelButton : MonoBehaviour {

    public Image checkImage;
    public int Level;
    public UIController ui;
	void Start () {
        string[] completedLevels = Regex.Split(PlayerPrefs.GetString("Levels", ""), "-");

        foreach (string lvl in completedLevels)
        {
            if (Level.ToString() == lvl)
            {
                //checkImage.color = new Color(1f,1f,1f,1f);
                checkImage.enabled = true;
            }
            else if ((Level - 1).ToString() == lvl && Level != 1)
            {
                GetComponent<Button>().interactable = true;
            }

        }

        //foreach (string lvl in completedLevels)
        //{
        //    if (Level.ToString() == lvl)
        //    {
        //        checkImage.color = new Color(1f,1f,1f,1f);
        //        checkImage.enabled = true;
        //    }
         
        //}
        //GetComponent<Button>().interactable = true;
         
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
