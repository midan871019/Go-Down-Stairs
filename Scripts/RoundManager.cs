using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{
    public GameState state =
        GameState.Menu;


    public event Action OnGameStart;
    public event Action OnGameOver;

    public GameObject menuUI;
    public GameObject playingUI;
    public GameObject gameOverUI;

    void Start()
    {
        GameManager.Instance.playerMovementManager.rb.isKinematic = true;
        menuUI.SetActive(true);
        playingUI.SetActive(false);
        gameOverUI.SetActive(false);
    }


    public void StartGame()
    {
        state = GameState.Playing;
        GameManager.Instance.playerMovementManager.rb.isKinematic = false;

        menuUI.SetActive(false);
        playingUI.SetActive(true);


        OnGameStart?.Invoke();
    }


    public void GameOver()
    {
        state = GameState.GameOver;

        GameManager.Instance.isGamePlaying = false;
        gameOverUI.SetActive(true);

        OnGameOver?.Invoke();

    }

    public void ResetButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public bool IsPlaying()
    {
        return state == GameState.Playing;
    }
}
