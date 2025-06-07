using System.Collections.Generic;
using UnityEngine;

public class Tween
{
    public System.Action execute;
}

public class TweenHandler : Singleton<TweenHandler>
{
    private List<Tween> tweens;

    protected override void Awake()
    {
        base.Awake();
        tweens = new List<Tween>();
    }

    private void Update()
    {
        for (int t = 0; t < tweens.Count; t++)
        {
            tweens[t].execute();
        }
    }

    public Tween BeginLocalPositionTween(Transform transform, Transform target, float duration)
    {
        Tween tween = new Tween();
        float timeToEnd = Time.time + duration;
        Vector3 originalLocalPosition = transform.localPosition;
        tween.execute = () =>
        {
            if (timeToEnd > Time.time)
            {
                float timeLeft = timeToEnd - Time.time;
                float interpolator = (1.0f - timeLeft) / duration;
                transform.localPosition = Vector3.Lerp(originalLocalPosition, target.localPosition, interpolator);
            }
            else
            {
                transform.localPosition = target.localPosition;
                tweens.Remove(tween);
            }
        };
        tweens.Add(tween);
        return tween;
    }

    public void Cancel(Tween tween)
    {
        tweens.Remove(tween);
    }
}
