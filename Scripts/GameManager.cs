using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public UIManager uIManager;
    public PlayerMovementManager playerMovementManager;
    public PlatformManager platformManager;
    public PlayerHealth playerHealth;
    public FloatingJoystick floatingJoystick;
    public CameraManager cameraManager;
    public RoundManager roundManager;
    public PlayerEffectManager playerEffectManager;

    public bool isGamePlaying = true;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void GameOver()
    {
        isGamePlaying = false;

        Debug.Log("Game Over");
    }
}