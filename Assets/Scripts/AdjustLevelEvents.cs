using UnityEngine;
using System.Collections;
using com.adjust.sdk;

public class AdjustLevelEvents : MonoBehaviour {

    public string Level1_Token = "u4d31k";
    public string Level2_Token = "";
    public string Level3_Token = "";
    public string Level4_Token = "";
    public string Level5_Token = "";
    public string Level6_Token = "";
    public string Level7_Token = "";
    public string Level8_Token = "";
    public string Level9_Token = "";
    public string Level10_Token = "";
    public string Level11_Token = "";
    public string Level12_Token = "";
    public string Level13_Token = "";
    public string Level14_Token = "";
    public string Level15_Token = "";
    public string Level16_Token = "";
    public string Level17_Token = "";
    public string Level18_Token = "";
    public string Level19_Token = "";
    public string Level20_Token = "";
    public string Level21_Token = "";
    public string Level22_Token = "";
    public string Level23_Token = "";
    public string Level24_Token = "";
    public string Level25_Token = "";



    public Hashtable levelHash = new Hashtable();


	void Start () {
        levelHash.Add(1, Level1_Token);
        levelHash.Add(2, Level2_Token);
        levelHash.Add(3, Level3_Token);
        levelHash.Add(4, Level4_Token);
        levelHash.Add(5, Level5_Token);
        levelHash.Add(6, Level6_Token);
        levelHash.Add(7, Level7_Token);
        levelHash.Add(8, Level8_Token);
        levelHash.Add(9, Level9_Token);
        levelHash.Add(10, Level10_Token);
        levelHash.Add(11, Level11_Token);
        levelHash.Add(12, Level12_Token);
        levelHash.Add(13, Level13_Token);
        levelHash.Add(14, Level14_Token);
        levelHash.Add(15, Level15_Token);
        levelHash.Add(16, Level16_Token);
        levelHash.Add(17, Level17_Token);
        levelHash.Add(18, Level18_Token);
        levelHash.Add(19, Level19_Token);
        levelHash.Add(20, Level20_Token);
        levelHash.Add(21, Level21_Token);
        levelHash.Add(22, Level22_Token);
        levelHash.Add(23, Level23_Token);
        levelHash.Add(24, Level24_Token);
        levelHash.Add(25, Level25_Token);
    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void SendLevelEvent(int i)
    {
        AdjustEvent adjustEvent = new AdjustEvent(levelHash[i].ToString());
        Adjust.trackEvent(adjustEvent);
    }
}
