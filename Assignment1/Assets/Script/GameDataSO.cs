using System;
using UnityEngine;

[CreateAssetMenu(fileName ="GameData", menuName ="Scriptable Objects/Game Data")]
public class GameDataSO : ScriptableObject
{
    private int _chancesLeft = 3;

    public Action<int> OnChancesUpdated;

    public void DeductChance()
    {
        _chancesLeft--;
        OnChancesUpdated?.Invoke(_chancesLeft);
    }

    public void ResetData()
    {
        _chancesLeft = 3;
        OnChancesUpdated = null;
    }
}
