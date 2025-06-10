using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MyScrollRect : ScrollRect
{
    private Vector2 maxScrollDelta;
    private float lastTime;


    const float MAX_ABS_SCROLL_VELOCITY = 2.0f;


    protected override void Awake()
    {
        base.Awake();
        maxScrollDelta = Vector2.up * MAX_ABS_SCROLL_VELOCITY;
    }

    public override void OnScroll(PointerEventData data)
    {
        // Limit scroll delta to a max value
        float timeInterval = Time.time - lastTime;
        float scrollVelocity = data.scrollDelta.y / timeInterval;
        if (Mathf.Abs(scrollVelocity) > MAX_ABS_SCROLL_VELOCITY)
        {
            data.scrollDelta = (scrollVelocity > 0.0f) ? maxScrollDelta : (-maxScrollDelta);
        }

        base.OnScroll(data);

        lastTime = Time.time;
    }
}
