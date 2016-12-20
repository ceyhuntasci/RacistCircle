using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using com.adjust.sdk;




public class GameLogic : MonoBehaviour
{
    public enum GameState
    {
        Paused, Ongoing, Ended
    }
    public AdjustLevelEvents adjustLevelEvents;
    public int maxLevels;
    public GameState gamestate;

    AdjustEvent LevelCompleteEvent = new AdjustEvent("u4d31k");
    
    public AudioHandler audioHandler;
    public GemSpawn gemspawn;
    public UIController ui;
    public Circle circle;
    public ObjectiveImage objectiveImage;
    [HideInInspector]
    public List<Gem> gemsInside;
    public int score;
    public bool slowMo;
    public float timer;

    private Tweener scoreTween;
    /// <summary>
    public int objectiveID;
    public int objectiveScore;
    private int objectiveCount;
    private int currentObjectiveCount;
    protected bool fourPopped;
    public string objectiveColor;
    public int objectiveTime;
    public int levelID;
    public string objectiveHeader;
    public string objectiveInfo;
    public string tutorialText;
    public string tutorialTitle;
    private LevelObjectiveData lastLevelData;
    public List<string> objectiveColorList;
    public int ObjectiveBubbleSpeed;
    private bool scoreGained;
    /// </summary>

    void Start()
    {
        Application.targetFrameRate = 60;
        AdjustAdapter.Start();
        GetProgress();
        gamestate = GameState.Paused;
        fourPopped = false;
        slowMo = false;
        score = 0;
        gemsInside = new List<Gem>();
    }


    void Update()
    {
        if (gamestate == GameState.Ongoing)
        {
            CheckWinCondition();
        }


    }

    public void CheckGems()
    {
        scoreGained = false;
        List<Gem> bubbles = gemsInside;
        bool oneColored = true;
        if (bubbles.Count > 0)
        {
            string firstColor = bubbles[0].Color;
            foreach (Gem x in bubbles)
            {
                if (x.Color != firstColor)
                {
                    oneColored = false;
                }
            }

        }

        if (oneColored && bubbles.Count > 1)
        {
            int plus = 0;
            if (objectiveColorList.Count > 0)
            {
                bool colorChecked = false;
                foreach (string color in objectiveColorList)
                {
                    if (bubbles[0].Color.ToUpper() == color)
                    {
                        colorChecked = true;
                    }
                }

                if (colorChecked)
                {
                    if (objectiveID == 1)
                    {
                        if (bubbles.Count == 2)
                        {
                            if (objectiveID == 1)
                            {
                                plus = 2;
                                IncrementScore(plus, score);
                                score += 2;
                                scoreGained = true;
                            }

                        }
                        else
                        {
                            plus = (bubbles.Count * (bubbles.Count + 1)) / 2;
                            IncrementScore(plus, score);
                            score += plus;
                            scoreGained = true;

                        }
                    }
                    else if (objectiveID == 2)
                    {
                        if (bubbles.Count >= objectiveCount)
                        {
                            plus = 1;
                            IncrementScore(plus, score);
                            score += plus;
                            scoreGained = true;
                        }
                    }
                    else if (objectiveID == 3)
                    {
                        bool specialCatched = false;
                        foreach (Gem bubble in bubbles)
                        {
                            if (bubble.isSpecial)
                            {
                                specialCatched = true;
                            }
                        }
                        if (specialCatched)
                        {
                            plus = 1;
                            IncrementScore(plus, score);
                            score += plus;
                            scoreGained = true;
                        }
                    }
                }
            }
            else
            {
                if (objectiveID == 1)
                {
                    if (bubbles.Count == 2)
                    {
                        plus = 2;
                        IncrementScore(plus, score);
                        score += 2;
                        scoreGained = true;

                    }
                    else
                    {
                        //if (bubbles.Count >= objectiveCount)
                        //{
                        //    fourPopped = true;
                        //}
                        plus = (bubbles.Count * (bubbles.Count + 1)) / 2;
                        IncrementScore(plus, score);
                        score += plus;
                        scoreGained = true;

                    }
                }
                else if (objectiveID == 2)
                {
                    if (bubbles.Count >= objectiveCount)
                    {
                        plus = 1;
                        IncrementScore(plus, score);
                        score += plus;
                        scoreGained = true;
                    }
                }
                else if (objectiveID == 3)
                {
                    bool specialCatched = false;
                    foreach (Gem bubble in bubbles)
                    {
                        if (bubble.isSpecial)
                        {
                            specialCatched = true;
                        }
                    }
                    if (specialCatched)
                    {
                        plus = 1;
                        IncrementScore(plus, score);
                        score += plus;
                        scoreGained = true;
                    }
                }
            }



            if (scoreGained)
            {
                FlashScore(plus, "+");
            }

            float delay = 0;


            foreach (Gem y in bubbles)
            {
                PopSound();
                y.PopBubble(delay, scoreGained);
                delay += 0.1f;
            }
            gemsInside = new List<Gem>();
            bubbles = new List<Gem>();
        }

        else
        {
            Penalty();
        }
    }
    void Penalty()
    {
        audioHandler.illegalMove.Play();
        Sequence mySequence = DOTween.Sequence();
        mySequence.Join(ui.redFlashImage.DOColor(new Color(1f, 1f, 1f, 0.7f), 0.3f));
        mySequence.Append(ui.redFlashImage.DOColor(new Color(1f, 1f, 1f, 0f), 0.3f));

    }
    void FlashScore(int dif, string not)
    {
        float fontSize = dif > 3 ? 2 : 1.2f ;

        if (ui.scoreTextTween != null)
        {
            ui.scoreTextTween.Kill(true);
        }

        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(ui.flashText.GetComponent<Text>().DOFade(0f, 0.0f));
        mySequence.Join(ui.flashText.DOMove(circle.transform.position, 0f));
        mySequence.Join(ui.flashText.GetComponent<Text>().DOFade(1f, 0.5f));
        mySequence.Join(ui.flashText.GetComponent<Text>().DOText(not + dif, 0f));
        mySequence.Join(DOVirtual.Float(1, fontSize, 0.5f, (float val) => { ui.flashText.GetComponent<Text>().rectTransform.localScale = new Vector2(val , val); }));
        mySequence.Append(ui.flashText.GetComponent<Text>().DOFade(0f, 0.5f));
        mySequence.Append(ui.flashText.DOMove(new Vector3(30, 30, 0), 0f));
        ui.scoreTextTween = mySequence;




    }
    public void Restart()
    {
        audioHandler.UItap.Play();
        Application.LoadLevel(Application.loadedLevel);
    }
    void EndGame(bool win)
    {
        if (win)
        {
            adjustLevelEvents.SendLevelEvent(levelID);
            audioHandler.victory.Play();
            if (maxLevels == levelID)
            {
                ui.ShowVictoryImage();
                ui.SetButtons(true, false, false);
            }
            else
            {
                ui.ShowVictoryImage();
                ui.SetButtons(true, true, false);
            }


            gamestate = GameState.Ended;
            timer = 0f;


            string levelsCompleted = PlayerPrefs.GetString("Levels", "");
            string[] words = Regex.Split(levelsCompleted, "-");
            bool add = true;
            foreach (string lvl in words)
            {
                if (lvl == levelID.ToString())
                {
                    add = false;
                }
            }
            if (add)
            {
                levelsCompleted = levelsCompleted + levelID + "-";
                PlayerPrefs.SetString("Levels", levelsCompleted);
            }

        }
        else
        {
            audioHandler.lose.Play();
            ui.ShowLoseImage();
            ui.SetButtons(true, false, true);
           
            gamestate = GameState.Ended;
            timer = 0f;

        }
    }
    public void CheckWinCondition()
    {
        switch (objectiveID)
        {
            case 1:
                CheckTime();
                if (score >= objectiveScore)
                {
                    EndGame(true);
                }
                break;
            case 2:
                CheckTime();
                if (score >= objectiveScore)
                {
                    EndGame(true);               
                }
                break;
            case 3:
                CheckTime();
                if (score >= objectiveScore)
                {
                    EndGame(true);
                }
                break;

        }
    }
    public void SetLevel(int level)
    {
        audioHandler.PlayUISound();

        levelID = level;
        ui.scoreText.text = "Score: 0";
        fourPopped = false;
        ui.levelCanvas.enabled = false;

        ui.hud.enabled = true;
        ui.SetButtons(false, false, false);
        ui.victoryImage.enabled = false;
        ui.loseImage.enabled = false;
        ui.loseImage.color = new Color(1f,1f,1f,0f);
        LevelObjectiveData ehe = GetLevelData();
        LoadLevel(ehe);

        ui.FlashText(ui.levelText, "Level " + levelID.ToString(), 1f, 1f);
        ui.FlashText(ui.headerText, objectiveHeader, 1f, 1f);
        ui.FlashText(ui.footerText, objectiveInfo, 1f, 1f);


        ui.tapToResumeButton.SetActive(true);

    }
    public void NextLevel()
    {
        audioHandler.UItap.Play();
        SetLevel(levelID + 1);
    }
    public void RestartLevel()
    {
        gamestate = GameState.Ended;
        SetLevel(levelID);
    }
    LevelObjectiveData GetLevelData()
    {
        string levelPrefabString = "Levels/" + "Level_" + levelID;

        TextAsset jsonAsset = Resources.Load(levelPrefabString) as TextAsset;
        if (jsonAsset != null)
        {
            string jsonText = jsonAsset.text;

            var meta = JsonConvert.DeserializeObject<LevelObjectiveData>(jsonText);
            lastLevelData = new LevelObjectiveData(meta);

            return lastLevelData;
        }
        throw new System.Exception("Level Asset couldn't be loaded : " + levelPrefabString);
    }
    void LoadLevel(LevelObjectiveData leveldata)
    {
        objectiveColorList = new List<string>();
        string[] words = Regex.Split(leveldata.ObjectiveColor, "-");
        if (words.Length > 0 && words[0] != "")
        {
            foreach (string word in words)
            {
                objectiveColorList.Add(word.ToUpper());
            }
        }


        score = 0;
        objectiveID = leveldata.ObjectiveId;
        objectiveScore = leveldata.ObjectiveScore;
        gemspawn.maxGem = leveldata.ColorCount;
        objectiveHeader = leveldata.Title;
        objectiveInfo = leveldata.Info;
        objectiveCount = leveldata.ObjectiveCount;
        objectiveTime = leveldata.Time;
        ObjectiveBubbleSpeed = leveldata.BubbleSpeed;
        gemspawn.ChangeBubbleSpeeds(ObjectiveBubbleSpeed);
        timer = objectiveTime;
        tutorialText = leveldata.TutorialText;
        tutorialTitle = leveldata.TutorialTitle;
        ui.timerText.text = "";
        ui.scoreText.text = "0/" + objectiveScore;
        gemspawn.levelSpecialGemTime = leveldata.FastBubbleTimer;
        gemspawn.SetFastBubbleTimer();

        if (objectiveColorList.Count > 0)
        {
            objectiveImage.SetImage(objectiveColorList[0], objectiveID, objectiveCount);
        }
        else
        {
            objectiveImage.SetImage("WHITE", objectiveID , objectiveCount);
        }



       
    }
    void GetProgress()
    {

    }
    void CheckTime()
    {
        if (objectiveTime != 0)
        {
            if (timer <= 0f)
            {
                EndGame(false);
            }
            else
            {
                timer -= Time.deltaTime;
                ui.timerText.text = Mathf.Ceil(timer).ToString();
            }
        }

    }
    void IncrementScore(int count, int currentScore)
    {

        scoreTween = DOVirtual.Float(currentScore, currentScore + count, 1f, (float val) =>
        {
            ui.scoreText.text = Mathf.RoundToInt(val).ToString() + "/" + objectiveScore.ToString();
        });

    }

    public void PopSound()
    {
        int x = Random.Range(1,4);
     
        switch(x){
            case 1: audioHandler.pop1.Play(); break;
            case 2: audioHandler.pop2.Play(); break;
            case 3: audioHandler.pop3.Play(); break;
        }
        
      
    }

}
