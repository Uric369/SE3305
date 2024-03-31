using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [Header("道具数量")]
    public int crystalNum = 0;
    public int stopperNum = 0;
    public int healerNum = 0;
    public int reverserNum = 0;

    [Header("对应分值")]
    public int crystalScore = 5;
    public int stopperScore = 10;
    public int healerScore = 10;
    public int reverserScore = 15;

    public int totalScore = 0;

    public UnityEvent<ScoreManager> onScoreChange;

    public void collectCrystal()
    {
        crystalNum++;
        totalScore += crystalScore;
        onScoreChange?.Invoke(this);
    }

    public void collectStopper()
    {
        stopperNum++;
        totalScore += stopperScore;
        onScoreChange?.Invoke(this);
    }

    public void collectHealer()
    {
        healerNum++;
        totalScore += healerScore;
        onScoreChange?.Invoke(this);
    }

    public void collectReverser()
    {
        reverserNum++;
        totalScore += reverserScore;
        onScoreChange?.Invoke(this);
    }

}
