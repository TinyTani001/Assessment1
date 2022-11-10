using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.SceneManagement;

/// <summary>
/// This class handles the in-game UI
/// </summary>
public class GameUIManager : MonoBehaviour
{
    public GameDataSO GameData;
    public GameObject[] ChanceCountImages;
    public CanvasGroup ScoreUIGroup, ChanceUIGroup;

    private void Start()
    {
        GameData.OnChancesUpdated += OnChancesUpdated;
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
            ScoreUIGroup.alpha = 1f;
            ScoreUIGroup.blocksRaycasts = true;
            ChanceUIGroup.alpha = 0f;
        }
    }
}
