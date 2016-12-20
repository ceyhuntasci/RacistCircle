using UnityEngine;
using System.Collections;

public class TouchController : MonoBehaviour
{
    public Circle circle;
    public GameLogic gamelogic;
    Vector3 clickPos;
    Vector3 touchPos;
    private bool mouseDrag;
    private bool touchUp;

    void Start()
    {
        touchUp = false;
        mouseDrag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gamelogic.gamestate == GameLogic.GameState.Ongoing)
        {

#if UNITY_EDITOR
            MouseClickController();
#else

            FingerTouchController();
#endif
        }
        else
        {
            mouseDrag = false;
            touchUp = false;
            circle.transform.position = new Vector3(30f, 30f, -1f);
            circle.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            gamelogic.slowMo = false;

       
        }

    }

    void MouseClickController()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            circle.transform.position = new Vector3(clickPos.x, clickPos.y, 0);
            mouseDrag = true;

        }
        if (mouseDrag)
        {
            clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //circle.transform.Translate ((clickPos.x - circle.transform.position.x) * circle.speed * Time.deltaTime, (clickPos.y - circle.transform.position.y) * circle.speed * Time.deltaTime, 0f);	
            circle.transform.position = new Vector3(clickPos.x, clickPos.y, 0f);
            if (circle.transform.localScale.x < 3f/4f)
            {
                circle.transform.localScale = new Vector3(circle.transform.localScale.x + circle.scaleRate * Time.deltaTime, circle.transform.localScale.y + circle.scaleRate * Time.deltaTime, circle.transform.localScale.z + circle.scaleRate * Time.deltaTime);
            }
            gamelogic.slowMo = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            mouseDrag = false;
            gamelogic.CheckGems();
            circle.transform.position = new Vector3(30f, 30f, -1f);
            circle.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            gamelogic.slowMo = false;

        }
    }
    void FingerTouchController()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            circle.transform.position = new Vector3(touchPos.x, touchPos.y, 0f);
            Debug.Log("X POSITON:  " + circle.transform.position.x);
            if (circle.transform.localScale.x < 3/4f)
            {
                circle.transform.localScale = new Vector3(circle.transform.localScale.x + circle.scaleRate * Time.deltaTime, circle.transform.localScale.y + circle.scaleRate * Time.deltaTime, circle.transform.localScale.z + circle.scaleRate * Time.deltaTime);
            }
            gamelogic.slowMo = true;
            touchUp = true;
        }
        else if (Input.touchCount == 0)
        {
            if (touchUp)
            {
                gamelogic.CheckGems();
                circle.transform.position = new Vector3(30f, 30f, -1f);
                circle.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                gamelogic.slowMo = false;

                touchUp = false;
            }

        }
    }

}
