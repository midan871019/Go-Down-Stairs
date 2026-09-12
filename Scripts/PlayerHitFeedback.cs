using System.Collections;
using UnityEngine;

public class PlayerHitFeedback : MonoBehaviour
{
    public Renderer playerRenderer;
    public CameraShake cameraShake;

    Color originalColor;

    void Start()
    {
        originalColor = playerRenderer.material.color;
        GameManager.Instance.playerHealth.OnDamaged += PlayHit;
        GameManager.Instance.playerHealth.OnHealed += PlayHeal;
    }
    void OnDestroy()
    {
        // 記得取消訂閱
        GameManager.Instance.playerHealth.OnDamaged -= PlayHit;
        GameManager.Instance.playerHealth.OnHealed -= PlayHeal;
    }

    public void PlayHit()
    {
        StartCoroutine(Flash(Color.red));

        if (cameraShake != null)
            cameraShake.Shake();
    }

    public void PlayHeal()
    {
        StartCoroutine(Flash(Color.green));
    }

    IEnumerator Flash(Color color)
    {
        if (playerRenderer != null)
        {
            playerRenderer.material.color = color;

            yield return new WaitForSeconds(0.2f);

            playerRenderer.material.color = originalColor;

        }
        
    }
}
