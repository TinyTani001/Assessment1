using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class handles the main menu UI
/// </summary>
public class MainMenuUIManager : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }
}
