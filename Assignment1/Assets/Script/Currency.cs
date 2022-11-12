using System.Collections;
using UnityEngine;

/// <summary>
/// This class handles the trigger events of currency and passes the value to GameData
/// </summary>
public class Currency : MonoBehaviour
{
    public int CurrncyValue;
    public GameDataSO GameData;

    private int _bottleLayer, _scoreMultiplier;
    private bool _bottleTouchingCurrency;

    private void Start()
    {
        _scoreMultiplier = 1;
        GameData.OnBottleStopped += OnBottleStopped;
        GameData.OnChancesUpdated += OnChanceCountUpdated;
        _bottleLayer = LayerMask.NameToLayer("Bottle");
        StartCoroutine(InitializeScoreMultiplierPosition());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _bottleLayer)
        {
            _bottleTouchingCurrency = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == _bottleLayer)
        {
            _bottleTouchingCurrency = false;
        }
    }

    private void OnBottleStopped()
    {
        if (_bottleTouchingCurrency)
        {
            ClearRegisteredCallbacks();
            GameData.IncreaseScore(CurrncyValue * _scoreMultiplier);
            _scoreMultiplier = 1;
            Destroy(transform.parent.gameObject);
        }
    }

    private void OnChanceCountUpdated(int chancesLeft)
    {
        if(chancesLeft > 0)
        {
            TrySettingOurPositionForScoreMultiplier();
        }
    }

    private void OnScoreMultiplierValueRecieved(int scoreMultiplier)
    {
        _scoreMultiplier = scoreMultiplier;
    }

    private void TrySettingOurPositionForScoreMultiplier()
    {
        // If the generated number is an even number only then we will request to set our position for score multiplier
        if (Random.Range(1, 101) % 2 == 0)
        {
            GameData.SetCurrencyPositionForScoreMultiplier(transform.position, OnScoreMultiplierValueRecieved);
        }
    }

    private void ClearRegisteredCallbacks()
    {
        GameData.OnBottleStopped -= OnBottleStopped;
        GameData.OnChancesUpdated -= OnChanceCountUpdated;
    }

    private IEnumerator InitializeScoreMultiplierPosition()
    {
        yield return new WaitForSeconds(1f);
        TrySettingOurPositionForScoreMultiplier();
    }
}
