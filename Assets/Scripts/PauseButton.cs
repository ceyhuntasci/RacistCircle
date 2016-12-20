using UnityEngine;
using System.Collections;

public class PauseButton : MonoBehaviour {


    public Canvas pauseMenu;
    public GameLogic gamelogic;
	void Start () {
 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Click()
    {
        gamelogic.audioHandler.PlayUISound();

        pauseMenu.enabled = true;
        gamelogic.gamestate = GameLogic.GameState.Paused;
    }
}
