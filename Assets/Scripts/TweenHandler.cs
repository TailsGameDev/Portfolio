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
        for (int t = tweens.Count - 1; t >= 0; t--)
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

    public Tween BeginQuadLocalPositionTween(Transform transform, Transform target, float duration)
    {
        Tween tween = new Tween();
        float startTime = Time.time;
        Vector3 originalLocalPosition = transform.localPosition;

        tween.execute = () =>
        {
            float elapsedTime = Time.time - startTime;

            // Quadratic easing in-out function (AI code)
            float t = Mathf.Clamp01(elapsedTime / duration);
            t = t < 0.5f ? 2f * t * t : 1f - Mathf.Pow(-2f * t + 2f, 2f) / 2f;

            transform.localPosition = Vector3.Lerp(originalLocalPosition, target.localPosition, t);

            if (elapsedTime >= duration)
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
