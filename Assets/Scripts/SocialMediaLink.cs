using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SocialMediaLink : MonoBehaviour, IScrollHandler
{
    [SerializeField]
    private ScrollRect scrollRect = null;

    [SerializeField]
    private string url;

    [SerializeField]
    private CanvasGroup canvasGroup = null;

    [SerializeField]
    private float fadeDuration = 0.0f;


    private float targetAlpha;
    private float fadeSpeed;
    private bool isPointerOverButton;


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
        isPointerOverButton = true;

        targetAlpha = 1.0f;
    }

    public void OnPointerExit()
    {
        isPointerOverButton = false;

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

    public void OnScroll(PointerEventData eventData)
    {
        if (scrollRect != null && isPointerOverButton)
        {
            ExecuteEvents.Execute(scrollRect.gameObject, eventData, ExecuteEvents.scrollHandler);
        }
    }
}