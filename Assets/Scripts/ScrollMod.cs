using UnityEngine;
using UnityEngine.UI;

public class ScrollMod : MonoBehaviour
{
    [SerializeField]
    private ScrollRect scrollRect = null;

    // NOTE: Disable clamp delta
    // public float maxScrollDelta = 1.0f;

    // NOTE: Disable scroll cooldown
    // public float scrollCooldown = 0.01f;
    // private float lastScrollTime;

    private float sensitivity;

    private void Awake()
    {
        sensitivity = scrollRect.scrollSensitivity;

        // Cancel scroll from scrollRect so we can manage it here in the OnScroll event
        scrollRect.scrollSensitivity = 0.0f;
    }

    public void OnScroll(string deltaStr)
    {
        if (float.TryParse(deltaStr, out float delta))
        {
            // NOTE: Disable scroll cooldown
            // float timeSinceLastScroll = Time.unscaledTime - lastScrollTime;
            // if (timeSinceLastScroll > scrollCooldown)
            {
                // NOTE: Disable clamp delta
                // float clampedDelta = Mathf.Clamp(delta, -maxScrollDelta, maxScrollDelta);
                // scrollRect.verticalNormalizedPosition += clampedDelta * sensitivity * Time.unscaledDeltaTime;

                scrollRect.verticalNormalizedPosition += delta * sensitivity * Time.unscaledDeltaTime;

                // NOTE: Disable scroll cooldown
                // lastScrollTime = Time.unscaledTime;
            }
        }
    }
}