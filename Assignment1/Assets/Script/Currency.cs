using UnityEngine;

/// <summary>
/// This class handles the trigger events of currency and passes the value to GameData
/// </summary>
public class Currency : MonoBehaviour
{
    public int CurrncyValue;
    public GameDataSO GameData;

    private int _bottleLayer;
    private bool _bottleTouchingCurrency;

    private void Start()
    {
        GameData.OnBottleStopped += OnBottleStopped;
        _bottleLayer = LayerMask.NameToLayer("Bottle");
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
            GameData.OnBottleStopped -= OnBottleStopped;
            GameData.IncreaseScore(CurrncyValue);
            Destroy(transform.parent.gameObject);
        }
    }
}
