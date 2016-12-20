using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelObjectiveData {

    public int LevelID;
    public int ObjectiveId;
    public int ObjectiveScore;
    public string ObjectiveColor;
    public int ColorCount;
    public string Title;
    public string Info;
    public int ObjectiveCount;
    public int Time;
    public string TutorialTitle;
    public string TutorialText;
    public int BubbleSpeed;
    public float FastBubbleTimer;

    public LevelObjectiveData()
    {

    }

    public LevelObjectiveData(LevelObjectiveData meta)
    {
        LevelID = meta.LevelID;
        ObjectiveId = meta.ObjectiveId;
        ObjectiveScore = meta.ObjectiveScore;
        ObjectiveColor = meta.ObjectiveColor;
        ColorCount = meta.ColorCount;
        Title = meta.Title;
        Info = meta.Info;
        ObjectiveCount = meta.ObjectiveCount;
        Time = meta.Time;
        TutorialText = meta.TutorialText;
        TutorialTitle = meta.TutorialTitle;
        BubbleSpeed = meta.BubbleSpeed;
        FastBubbleTimer = meta.FastBubbleTimer;
    }

}
