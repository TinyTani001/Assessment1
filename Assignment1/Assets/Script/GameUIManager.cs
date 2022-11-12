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
    public Canvas MainCanvas;
    public GameObject[] ChanceCountImages;
    public CanvasGroup FinalScoreUIGroup, MainUIGroup;
    public TMP_Text CurrentScoreText, FinalScoreText, ScoreMultiplierText;
    public RectTransform ScoreMultiplierParentRect;
    public Image MeterMaskImage;

    private RectTransform _scoreMultiplierRect;

    private void Start()
    {
        _scoreMultiplierRect = ScoreMultiplierText.rectTransform;
        ScoreMultiplierText.text = string.Empty;

        GameData.OnChancesUpdated += OnChancesUpdated;
        GameData.OnScoreUpdated += OnScoreUpdate;
        GameData.OnBottleDragged += OnBottleDragged;
        GameData.OnNewScoreMultiplierPosition += OnNewScoreMultiplierPosition;
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
        ScoreMultiplierText.text = string.Empty;
    }

    private void OnBottleDragged(float percent)
    {
        MeterMaskImage.fillAmount = percent;
    }

    private void OnNewScoreMultiplierPosition(Vector3 position, int multiplierValue)
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(position) / MainCanvas.scaleFactor;
        ScoreMultiplierParentRect.anchoredPosition = screenPosition;
        ScoreMultiplierText.text = $"x{multiplierValue}";
    }
}
