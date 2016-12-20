using UnityEngine;
using System.Collections;
using DG.Tweening;

public class GemSpawn : MonoBehaviour
{
    public GameObject red;
    public GameObject blue;
    public GameObject green;
    public GameObject purple;
    public GameObject yellow;
   
    public GameLogic gamelogic;
    
    
    public int maxGem;
    private float xBound;
    private float yBound = 6f;
    public float specialGemTimer;
    public float decidedSpecialGemTimer;
    public float levelSpecialGemTime;
    private float timer;
    

    // Use this for initialization
    void Start()
    {
        xBound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f)).x + 0.37f;
        timer = 0f;
        maxGem = 2;
      
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (gamelogic.gamestate == GameLogic.GameState.Ongoing)
        {
            timer += Time.deltaTime;
            if (levelSpecialGemTime != 0)
            {
                specialGemTimer += Time.deltaTime; 
            }
            if (timer > 0.5f)
            {
                SpawnGem();
                timer = 0f;
            }
        }
    }

    void SpawnGem()
    {
        int locationType = Random.Range(1, 5);
        float x = 0f;
        float y = 0f;

        switch (locationType)
        {
            case 1:
                x = -xBound;
                y = Random.Range(-yBound, yBound);
                break;
            case 2:
                x = xBound;
                y = Random.Range(-yBound, yBound);
                break;
            case 3:
                x = Random.Range(-xBound, xBound);
                y = -yBound;
                break;
            case 4:
                x = Random.Range(-xBound, xBound);
                y = yBound;
                break;
        }
        int gemType = Random.Range(1, maxGem + 1);
        GameObject gemColor = red;
        switch (gemType)
        {
            case 1:
                gemColor = red;
                break;
            case 2:
                gemColor = green;
                break;
            case 3:
                gemColor = blue;
                break;
            case 4:
                gemColor = purple;
                break;
            case 5:
                gemColor = yellow;
                break;
        }
        GameObject gemObject;
        Gem gem;
        if (levelSpecialGemTime != 0 && specialGemTimer >= decidedSpecialGemTimer)
        {
            specialGemTimer = 0;
            decidedSpecialGemTimer = Random.RandomRange(levelSpecialGemTime - 2, levelSpecialGemTime + 2);

            gemObject = (GameObject)Instantiate(gemColor, new Vector3(x, y), Quaternion.identity);
            gem = gemObject.GetComponent<Gem>();
            float xSpeedVariation;
            xSpeedVariation = -x + Random.Range(-xBound / 2f, xBound / 2f);
            float ySpeedVariation;
            ySpeedVariation = -y + Random.Range(-yBound / 2f, yBound / 2f);

          
            gem.trailRotation = ySpeedVariation == 0 ? -10f : Vector2.Angle(new Vector2(xSpeedVariation, ySpeedVariation), new Vector2(1, 0));
            gem.trailRotation = ySpeedVariation < 0 ? -gem.trailRotation : gem.trailRotation;
            gem.isSpecial = true;
            gem.transform.rotation = Quaternion.Euler(0f, 0f, gem.trailRotation - 90);
            gem.trail.transform.rotation = Quaternion.Euler(0f, 0f, gem.trailRotation + 170);
            gem.speed = gem.speed * 3;
          
            gem.PlaySpecialAnim();
            if (gamelogic.slowMo)
            {
                gem.rb.AddForce(new Vector2(xSpeedVariation * gem.speed / 3, ySpeedVariation * gem.speed / 3));
                gem.slowed = true;
            }
            else
            {
                gem.rb.AddForce(new Vector2(xSpeedVariation * gem.speed, ySpeedVariation * gem.speed));
            }
        }

        else
        {
            gemObject = (GameObject)Instantiate(gemColor, new Vector3(x, y), Quaternion.identity);
            gem = gemObject.GetComponent<Gem>();
            float xSpeedVariation;
            xSpeedVariation = -x + Random.Range(-xBound / 2f, xBound / 2f);
            float ySpeedVariation;
            ySpeedVariation = -y + Random.Range(-yBound / 2f, yBound / 2f);

            gem.trailRotation = ySpeedVariation == 0 ? -10f : Vector2.Angle(new Vector2(xSpeedVariation, ySpeedVariation), new Vector2(1, 0));
            gem.trailRotation = ySpeedVariation < 0 ? -gem.trailRotation : gem.trailRotation;
            gem.trail.transform.rotation = Quaternion.Euler(0f, 0f, gem.trailRotation + 170);


            if (gamelogic.slowMo)
            {
                gem.rb.AddForce(new Vector2(xSpeedVariation * gem.speed / 3, ySpeedVariation * gem.speed / 3));
                gem.slowed = true;
            }
            else
            {
                gem.rb.AddForce(new Vector2(xSpeedVariation * gem.speed, ySpeedVariation * gem.speed));
            }
        }
     

       

        


    }

    public void ChangeBubbleSpeeds(int speed)
    {
        red.GetComponent<Gem>().speed = speed;
        green.GetComponent<Gem>().speed = speed;
        blue.GetComponent<Gem>().speed = speed;
        purple.GetComponent<Gem>().speed = speed;
        yellow.GetComponent<Gem>().speed = speed;
    }

    public void SetFastBubbleTimer()
    {
        decidedSpecialGemTimer = Random.RandomRange(levelSpecialGemTime - 2, levelSpecialGemTime + 2);
    }
}
