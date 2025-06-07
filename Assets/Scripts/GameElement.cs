using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameElement : MonoBehaviour
{
    [SerializeField]
    private ScrollRect scrollRect = null;
    [SerializeField]
    private string url = null;
    public void OnPointerDown()
    {
        Application.OpenURL(url);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        ExecuteEvents.Execute(scrollRect.gameObject, eventData, ExecuteEvents.beginDragHandler);
    }
    public void OnDrag(PointerEventData eventData)
    {
        ExecuteEvents.Execute(scrollRect.gameObject, eventData, ExecuteEvents.dragHandler);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        ExecuteEvents.Execute(scrollRect.gameObject, eventData, ExecuteEvents.endDragHandler);
    }

}
