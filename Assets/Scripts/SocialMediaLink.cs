using UnityEngine;

public class SocialMediaLink : MonoBehaviour
{
    [SerializeField]
    private string url;

    [SerializeField]
    private CanvasGroup canvasGroup = null;

    [SerializeField]
    private float fadeDuration = 0.0f;


    private float targetAlpha;
    private float fadeSpeed;


    private void Awake()
    {
        fadeSpeed = 1.0f / fadeDuration;
    }

    private void Update()
    {
        if (canvasGroup.alpha != targetAlpha)
        {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, targetAlpha, fadeSpeed * Time.deltaTime);
        }
    }

    public void OnPointerEnter()
    {
        targetAlpha = 1.0f;
    }

    public void OnPointerExit()
    {
        targetAlpha = 0.0f;
    }

    public void OnPointerClick()
    {
        Application.OpenURL(url);
    }
    public void OnMailPointerClick()
    {
        Application.OpenURL($"mailto:{url}");
    }
}