using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public PlayerHealth playerHealth;

    public Slider heightSlider;

    public TextMeshProUGUI currentLayer;

    public PlayerHealth player;

    public Image frontFill;

    public Image backFill;

    public float delaySpeed = 2f;

    float targetFill;

    public float maxHeight = 100f;

    public RectTransform icon;

    public RectTransform bar;

    void Start()
    {
        playerHealth.OnHealthChanged += UpdateBar;
    }

    void UpdateBar(float current, float max)
    {
        targetFill = current / max;

        frontFill.fillAmount = targetFill;
    }

    void Update()
    {
        backFill.fillAmount =
            Mathf.Lerp(
                backFill.fillAmount,
                targetFill,
                Time.deltaTime * delaySpeed
            );
        currentLayer.text = $"Current Layer : {GameManager.Instance.platformManager.currentLayer} ";

        float t =
            Mathf.InverseLerp(GameManager.Instance.platformManager.respawnY, GameManager.Instance.platformManager.recycleHeight, GameManager.Instance.platformManager.player.gameObject.transform.position.y);
        Debug.Log(GameManager.Instance.platformManager.player.gameObject.transform.position.y);

        float height =
            bar.rect.height*3;

        icon.anchoredPosition =
            new Vector2(icon.anchoredPosition.x, t * height - height / 2);



        heightSlider.maxValue = GameManager.Instance.platformManager.recycleHeight - GameManager.Instance.platformManager.respawnY;

        heightSlider.value = GameManager.Instance.platformManager.player.gameObject.transform.position.y - GameManager.Instance.platformManager.respawnY;
    }
}
