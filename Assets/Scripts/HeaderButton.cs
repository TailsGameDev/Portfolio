using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
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
    [FormerlySerializedAs("gameObjectToActivate")]
    private GameObject pageToActivate = null;

    [SerializeField]
    private HeaderButton otherHeaderButton = null;
    [SerializeField]
    private bool isSelected = false;

    private float targetAlpha;
    private float fadeSpeed;
    private bool isPointerOverButton;
    private Action<HeaderButton> onHeaderButtonClick;


    public GameObject PageToActivate { get => pageToActivate; }


    public void Initialize(Action<HeaderButton> onHeaderButtonClickParam)
    {
        fadeSpeed = 1.0f / fadeDuration;

        if (isSelected)
        {
            targetAlpha = 1.0f;
        }

        onHeaderButtonClick = onHeaderButtonClickParam;
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
        onHeaderButtonClick?.Invoke(this);
    }

    public void Select()
    {
        isSelected = true;
        targetAlpha = 1.0f;

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

    public void SetGameObjectActive(bool activate)
    {
        gameObject.SetActive(activate);

        if (activate)
        {
            // Set the canvas group alpha as deselected so if button gets activated again it does not look selected
            canvasGroup.alpha = 0.0f;
            targetAlpha = 0.0f;
        }
    }
}