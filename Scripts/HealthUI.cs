using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public PlayerHealth playerHealth;

    public Slider slider;


    public Slider frontBar; // 立即變化
    public Slider backBar;  // 延遲

    public float delaySpeed = 2f;

    void Start()
    {
        playerHealth.OnHealthChanged += UpdateBar;
    }

    void UpdateBar(float current, float max)
    {
        float value = current / max;

        frontBar.value = value;
    }

    void Update()
    {
        backBar.value = Mathf.Lerp(
            backBar.value,
            frontBar.value,
            Time.deltaTime * delaySpeed
        );
    }
}
