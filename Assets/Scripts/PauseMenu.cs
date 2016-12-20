using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public Canvas pauseCanvas;
    public GameLogic gamelogic;

    public Image mute;
    public Sprite SoundImage;
    public Sprite MutedImage;

    void Start()
    {
        CheckMusicSetting();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HomeButton()
    {
        gamelogic.audioHandler.PlayUISound();
        Application.LoadLevel(Application.loadedLevel);
    }
    public void ResumeButton()
    {
        gamelogic.audioHandler.PlayUISound();

        if (gamelogic.ui.tapToResumeButton.activeInHierarchy)
        {
            pauseCanvas.enabled = false;
        }
        else
        {
            DOVirtual.DelayedCall(0.1f, () => pauseCanvas.enabled = false);
            DOVirtual.DelayedCall(0.4f, () => gamelogic.gamestate = GameLogic.GameState.Ongoing);
            //pauseCanvas.enabled = false;
            //gamelogic.gamestate = GameLogic.GameState.Ongoing;
            GameObject[] bubbles = GameObject.FindGameObjectsWithTag("Bubble");
            foreach (GameObject y in bubbles)
            {
                y.GetComponent<Gem>().UnFreeze();

            }
        }

    }
    public void RestartLevelButton()
    {
        gamelogic.audioHandler.PlayUISound();
        pauseCanvas.enabled = false;
    }

    public void MuteMusic()
    {
        if (PlayerPrefs.GetInt("Mute", 0) == 1)
        {
            PlayerPrefs.SetInt("Mute", 0);
            gamelogic.audioHandler.music.volume = 1;
            mute.sprite = SoundImage;
        }
        else
        {
            PlayerPrefs.SetInt("Mute" , 1);
            gamelogic.audioHandler.music.volume = 0;
            mute.sprite = MutedImage;
        }
       
        
    }
    public void CheckMusicSetting()
    {
        if (PlayerPrefs.GetInt("Mute", 0) == 1)
        {
            gamelogic.audioHandler.music.volume = 0;
            mute.sprite = MutedImage;
        }
    }

}
