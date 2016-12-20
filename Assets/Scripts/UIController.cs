using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using System.Text.RegularExpressions;

public class UIController : MonoBehaviour
{

    public Canvas levelCanvas;
    public Canvas hud;
    public Text headerText;
    public Text footerText;
    public Text levelText;
    public Text timerText;
    public Text scoreText;
    public Image redFlashImage;
    public Image victoryImage;
    public Image loseImage;
    public RectTransform flashText;

    public GameObject mainMenuButton;
    public GameObject nextLevelButton;
    public GameObject restartLevelButton;
    public GameObject tapToResumeButton;

    public GameLogic gamelogic;

    public string[] completedLevels;

    public Sequence scoreTextTween;
    void Start()
    {
        completedLevels = Regex.Split(PlayerPrefs.GetString("Levels", ""), "-");
        hud.enabled = false;
        mainMenuButton.SetActive(false);
        nextLevelButton.SetActive(false);
        restartLevelButton.SetActive(false);
        timerText.text = "Time: " + Mathf.Ceil(gamelogic.timer).ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void FlashText(Text text, string textValue, float time, float alphaValue)
    {
        text.text = textValue;
        text.DOFade(alphaValue, time);

    }
    public void SetButtons(bool main, bool next, bool restart)
    {
        if (main)
        {
            DOVirtual.DelayedCall(1.8f, () => mainMenuButton.SetActive(main));
            DOVirtual.DelayedCall(1.8f, () => mainMenuButton.GetComponent<Animator>().Play("MainMenuButton"));
        }
        else
        {
            mainMenuButton.SetActive(main);
        }
        if (next)
        {
            DOVirtual.DelayedCall(1.5f, () =>  nextLevelButton.SetActive(next));
            DOVirtual.DelayedCall(1.5f, () => nextLevelButton.GetComponent<Animator>().Play("NextLevelButton"));
        }
        else
        {
            nextLevelButton.SetActive(next);
        }
        if (restart)
        {
            DOVirtual.DelayedCall(1.5f, () =>  restartLevelButton.SetActive(restart));
            DOVirtual.DelayedCall(1.5f, () => restartLevelButton.GetComponent<Animator>().Play("RestartLevelButton"));
        }
        else
        {
            restartLevelButton.SetActive(restart);
        }
       
    }
    public void TapToResume()
    {

        tapToResumeButton.SetActive(false);

        if (footerText.text == gamelogic.tutorialText)
        {
            footerText.DOFade(0f, 1f);
            headerText.DOFade(0f, 1f);
            DOVirtual.DelayedCall(1f, () => gamelogic.gamestate = GameLogic.GameState.Ongoing);
        }
        else
        {

            FlashText(levelText, "Level " + gamelogic.levelID.ToString(), 1f, 0f);
            FlashText(headerText, gamelogic.objectiveHeader, 1f, 0f);
            FlashText(footerText, gamelogic.objectiveInfo, 1f, 0f);

            if (gamelogic.tutorialText == "")
            {
                DOVirtual.DelayedCall(1f, () => gamelogic.gamestate = GameLogic.GameState.Ongoing);
            }
            else
            {
                Sequence mySeq = DOTween.Sequence();
                mySeq.Append(footerText.DOFade(0f, 1f));
                mySeq.Join(headerText.DOFade(0f, 1f));
                mySeq.Append(footerText.GetComponent<Text>().DOText(gamelogic.tutorialText, 0f));
                mySeq.Join(headerText.GetComponent<Text>().DOText(gamelogic.tutorialTitle, 0f));
                mySeq.Append(footerText.DOFade(1f, 1f));
                mySeq.Join(headerText.DOFade(1f, 1f));
                mySeq.AppendCallback(() => tapToResumeButton.SetActive(true));

            }
        }



    }
    public void ShowVictoryImage()
    {
        DOVirtual.DelayedCall(0.75f, () => victoryImage.enabled = true);
        DOVirtual.DelayedCall(0.75f, () => victoryImage.gameObject.GetComponent<Animator>().Play("Victory"));
    }

    public void ShowLoseImage()
    {
        DOVirtual.DelayedCall(0.75f, () => loseImage.enabled = true);
        DOVirtual.DelayedCall(0.75f, () => loseImage.DOColor(new Color(1f,1f,1f,1f) , 1f));
    }

}
