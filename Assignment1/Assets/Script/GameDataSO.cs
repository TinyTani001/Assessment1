using System;
using UnityEngine;

[CreateAssetMenu(fileName ="GameData", menuName ="Scriptable Objects/Game Data")]
public class GameDataSO : ScriptableObject
{
    private int _chancesLeft = 6, _score;

    public Action<int> OnChancesUpdated, OnScoreUpdated;
    public Action OnBottleStopped, OnBottleReleased;
    public Action<float> OnBottleDragged;

    public void DeductChance()
    {
        _chancesLeft--;
        OnChancesUpdated?.Invoke(_chancesLeft);
    }

    public void IncreaseScore(int byValue)
    {
        _score+= byValue;
        OnScoreUpdated?.Invoke(_score);
    }

    public void ResetData()
    {
        _chancesLeft = 6;
        _score = 0;
        OnChancesUpdated = null;
        OnBottleStopped = null;
        OnBottleDragged = null;
    }
}
