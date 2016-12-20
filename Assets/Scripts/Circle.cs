using UnityEngine;
using System.Collections;

public class Circle : MonoBehaviour
{
    public GameLogic gameLogic;
    public float speed;
	public float scaleRate;

    float xBound;
    float yBound;
    float yTop;
	// Use this for initialization
	void Start ()
	{
        xBound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f)).x + 0.37f;
        yBound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f)).y + 0.37f;
        yTop = yBound - 1f;

        Debug.Log(yTop);
        Debug.Log(yBound);
        
	}
	
	// Update is called once per frame
	void Update ()
	{
	    
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        Gem gem = col.gameObject.GetComponent<Gem>();
        if(!gameLogic.gemsInside.Contains(gem)){
            if (Mathf.Abs(col.gameObject.transform.position.x) > xBound || (col.gameObject.transform.position.y < yBound && col.gameObject.transform.position.y > yTop))
            {
                
            }
            else
            {
                gem.PlayGlowAnim();
                gameLogic.gemsInside.Add(gem);
            }
        
        }
        

    }

    void OnTriggerExit2D(Collider2D col)
    {
        Gem gem = col.gameObject.GetComponent<Gem>();
        if (gameLogic.gemsInside.Contains(gem))
        {
            gem.PlayIdleAnim();
            gameLogic.gemsInside.Remove(gem);
        }

    }

    void OnTriggerStay2D(Collider2D col)
    {
        OnTriggerEnter2D(col);
    }
}
