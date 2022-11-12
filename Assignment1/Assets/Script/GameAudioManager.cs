using UnityEngine;

public class GameAudioManager : MonoBehaviour
{
    public GameDataSO GameData;
    public AudioSource CoinCollectAudio, BottleReleaseAudio;

    private void Start()
    {
        GameData.OnScoreUpdated += OnScoreUpdated;
        GameData.OnBottleReleased += OnBottleReleased;
    }

    private void OnBottleReleased()
    {
        BottleReleaseAudio.Play();
    }

    private void OnScoreUpdated(int newScore)
    {
        CoinCollectAudio.Play();
    }
}
