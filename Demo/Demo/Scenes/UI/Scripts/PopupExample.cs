using UnityEngine;

public class PopupExample : MonoBehaviour
{
    public AnimationCurve moveInCurve, moveOutCurve;
    public Vector2 moveFrom, moveTo;

    public CanvasGroup group;

    private RectTransform t;
    private Tween myTween;

    bool isAnimating;
    bool isOpen;

    private void Awake() => t = GetComponent<RectTransform>();

    public void Animate()
    {
        if (isAnimating) return;

        myTween?.Cancel();

        Vector2 lastPos = isOpen ? moveTo : moveFrom;
        Vector2 newPos = isOpen ? moveFrom : moveTo;
        AnimationCurve curve = isOpen ? moveOutCurve : moveInCurve;
        float alphafrom = isOpen? 1f : 0f;
        float alphaTo = isOpen ? 0f : 1f;

        myTween = mTween.NewTween(0.6f)
            .MoveTo(t, lastPos, newPos, curve)
            .AlphaTo(group, alphafrom, alphaTo, moveInCurve)
            .SetOnStart(() => isAnimating = true)
            .SetOnComplete(OnComplete);
    }

    void OnComplete()
    {
        isAnimating = false;
        isOpen = !isOpen;
    }
}
