using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;
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
    private Transform movingTransform = null;
    [SerializeField]
    private float moveAnimationDuration = 0.0f;

    [SerializeField]
    private CanvasGroup darkCover = null;
    [SerializeField]
    private float darkCoverAnimationAlpha = 0.0f;

    [SerializeField]
    private Image gameImage = null;
    [SerializeField]
    private Image pageGameImage = null;

    [SerializeField]
    private string spriteAddress = null;

    [SerializeField]
    private GameObject pageToOpen = null;


    private Action<GameElement> onGameElementClick;

    private bool isPointerOverButton = false;

    private Tween cachedPositionTween;
    private Tween cachedAplhaTween;

    private int safetyCounter;
    private const int MAX_SAFETY_COUNT = 15;


    public GameObject PageToOpen { get => pageToOpen; }


    public void Initialize(Action<GameElement> onGameElementClickParam)
    {
        onGameElementClick = onGameElementClickParam;

        Addressables.LoadAssetAsync<Sprite>(spriteAddress).Completed += OnSpriteLoaded;
    }

    private void OnSpriteLoaded(AsyncOperationHandle<Sprite> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            gameImage.sprite = handle.Result;
            pageGameImage.sprite = handle.Result;
        }
        else
        {
            Debug.LogError("[GameElement] Failed to load sprite from addressables.", this);

            // Try to load the sprite again, to a safety limit
            if (safetyCounter < MAX_SAFETY_COUNT)
            {
                Addressables.LoadAssetAsync<Sprite>(spriteAddress).Completed += OnSpriteLoaded;
                safetyCounter++;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (pageToOpen != null)
        {
            onGameElementClick?.Invoke(this);
            DeselectElement();
        }
        else if ( url != null && url != "" )
        {
            Application.OpenURL(url);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOverButton = true;

        // Animate position and canvas group
        TweenHandler.Instance.Cancel(cachedPositionTween);
        cachedPositionTween = TweenHandler.Instance.BeginQuadLocalPositionTween(movingTransform, upPosition, moveAnimationDuration);
        TweenHandler.Instance.Cancel(cachedAplhaTween);
        cachedAplhaTween = TweenHandler.Instance.BeginQuadCanvasGroupTween(darkCover, targetAplha: darkCoverAnimationAlpha, moveAnimationDuration);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        DeselectElement();
    }
    private void DeselectElement()
    {
        isPointerOverButton = false;

        // Animate position and canvas group
        TweenHandler.Instance.Cancel(cachedPositionTween);
        cachedPositionTween = TweenHandler.Instance.BeginQuadLocalPositionTween(movingTransform, originalPosition, moveAnimationDuration);
        TweenHandler.Instance.Cancel(cachedAplhaTween);
        cachedAplhaTween = TweenHandler.Instance.BeginQuadCanvasGroupTween(darkCover, targetAplha: 0.0f, moveAnimationDuration);
    }
    public void OnScroll(PointerEventData eventData)
    {
        if (scrollRect != null && isPointerOverButton)
        {
            ExecuteEvents.Execute(scrollRect.gameObject, eventData, ExecuteEvents.scrollHandler);
        }
    }
}
