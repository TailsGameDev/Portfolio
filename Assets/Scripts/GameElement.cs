using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IScrollHandler, IPointerClickHandler
{
    [SerializeField]
    private ScrollRect scrollRect = null;
    [SerializeField]
    private string url = null;

    [SerializeField]
    private Transform upPosition = null;
    [SerializeField]
    private Transform originalPosition = null;

    [SerializeField]
    private Image darkCover = null;
    [SerializeField]
    private Transform movingTransform = null;
    [SerializeField]
    private float moveAnimationDuration = 0.0f;

    private bool isPointerOverButton = false;

    // private float timeLimitToConsiderClick;
    private Tween cachedTween;

    public void OnPointerClick(PointerEventData eventData)
    {
        Application.OpenURL(url);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOverButton = true;

        TweenHandler.Instance.Cancel(cachedTween);
        cachedTween = TweenHandler.Instance.BeginLocalPositionTween(movingTransform, upPosition, moveAnimationDuration);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOverButton = false;

        TweenHandler.Instance.Cancel(cachedTween);
        cachedTween = TweenHandler.Instance.BeginLocalPositionTween(movingTransform, originalPosition, moveAnimationDuration);
    }
    public void OnScroll(PointerEventData eventData)
    {
        if (scrollRect != null && isPointerOverButton)
        {
            ExecuteEvents.Execute(scrollRect.gameObject, eventData, ExecuteEvents.scrollHandler);
        }
    }
}
