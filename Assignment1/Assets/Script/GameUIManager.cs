using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This class handles the in-game UI
/// </summary>
public class GameUIManager : MonoBehaviour
{
    public GameDataSO GameData;
    public GameObject[] ChanceCountImages;
    public CanvasGroup FinalScoreUIGroup, MainUIGroup;
    public TMP_Text CurrentScoreText, FinalScoreText;
    public Image MeterMaskImage;

    private void Start()
    {
        GameData.OnChancesUpdated += OnChancesUpdated;
        GameData.OnScoreUpdated += OnScoreUpdate;
        GameData.OnBottleDragged += OnBottleDragged;
    }

    private void OnDestroy()
    {
        GameData.ResetData();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnChancesUpdated(int currentChances)
    {
        ChanceCountImages[currentChances].gameObject.SetActive(false);
        if (currentChances == 0)
        {
            FinalScoreUIGroup.alpha = 1f;
            FinalScoreUIGroup.blocksRaycasts = true;
            MainUIGroup.alpha = 0f;
        }
        MeterMaskImage.fillAmount = 0f;
    }

    private void OnScoreUpdate(int newValue)
    {
        CurrentScoreText.text = FinalScoreText.text = newValue.ToString();
    }

    private void OnBottleDragged(float percent)
    {
        MeterMaskImage.fillAmount = percent;
    }
}
