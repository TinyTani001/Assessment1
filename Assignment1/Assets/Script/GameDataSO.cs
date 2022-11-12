using System;
using UnityEngine;

[CreateAssetMenu(fileName ="GameData", menuName ="Scriptable Objects/Game Data")]
public class GameDataSO : ScriptableObject
{
    private int _chancesLeft = 6, _score;

    public Action<int> OnChancesUpdated, OnScoreUpdated;
    public Action OnBottleStopped, OnBottleReleased;
    public Action<float> OnBottleDragged;
    public Action<Vector3, int> OnNewScoreMultiplierPosition;

    private bool _currencyPositionForScoreMultiplierUpdated;

    public void DeductChance()
    {
        _currencyPositionForScoreMultiplierUpdated = false;
        _chancesLeft--;
        OnChancesUpdated?.Invoke(_chancesLeft);
    }

    public void SetCurrencyPositionForScoreMultiplier(Vector3 position, Action<int> OnPositionAccepted)
    {

        // Only accept score multiplier position request only if this random number is even and we didn't already accepted a request
        if (UnityEngine.Random.Range(1, 101) % 2 == 0 && !_currencyPositionForScoreMultiplierUpdated)
        {
            _currencyPositionForScoreMultiplierUpdated = true;
            int multiplierValue = UnityEngine.Random.Range(2, 6);
            OnPositionAccepted?.Invoke(multiplierValue);
            OnNewScoreMultiplierPosition?.Invoke(position, multiplierValue);
        }
    }

    public void IncreaseScore(int byValue)
    {
        _score+= byValue;
        OnScoreUpdated?.Invoke(_score);
    }

    public void ResetData()
    {
        _currencyPositionForScoreMultiplierUpdated = false;
        _chancesLeft = 6;
        _score = 0;
        OnChancesUpdated = null;
        OnBottleStopped = null;
        OnBottleReleased = null;
        OnBottleDragged = null;
        OnScoreUpdated = null;
        OnNewScoreMultiplierPosition = null;
    }
}
