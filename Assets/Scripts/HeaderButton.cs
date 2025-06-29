using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeaderButton : MonoBehaviour, IScrollHandler
{
    [SerializeField]
    private ScrollRect scrollRect = null;

    [SerializeField]
    private CanvasGroup canvasGroup = null;

    [SerializeField]
    private float fadeDuration = 0.0f;

    [SerializeField]
    private GameObject gameObjectToActivate = null;
    [SerializeField]
    private GameObject gameObjectToDeactivate = null;

    [SerializeField]
    private HeaderButton otherHeaderButton = null;
    [SerializeField]
    private bool isSelected = false;

    private float targetAlpha;
    private float fadeSpeed;
    private bool isPointerOverButton;


    private void Awake()
    {
        fadeSpeed = 1.0f / fadeDuration;

        if (isSelected)
        {
            targetAlpha = 1.0f;
        }
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

        if (!isSelected)
        {
            targetAlpha = 0.0f;
        }
    }

    public void OnPointerClick()
    {
        gameObjectToActivate.SetActive(true);
        gameObjectToDeactivate.SetActive(false);

        isSelected = true;
        otherHeaderButton.isSelected = false;
        otherHeaderButton.targetAlpha = 0.0f;
    }

    public void OnScroll(PointerEventData eventData)
    {
        if (scrollRect != null && isPointerOverButton)
        {
            ExecuteEvents.Execute(scrollRect.gameObject, eventData, ExecuteEvents.scrollHandler);
        }
    }
}