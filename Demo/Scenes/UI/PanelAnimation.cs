using UnityEngine;
using UnityEngine.UI;

public class PanelAnimation : MonoBehaviour
{
    [Header("Position")]
    public AnimationCurve moveCurve;
    public Vector2 moveFrom, moveTo;

    [Header("Rotation")]
    public AnimationCurve rotateCurve;
    public float angleFrom, angleTo;

    [Header("Scale")]
    public AnimationCurve scaleCurve;
    public Vector2 scaleFrom, scaleTo;

    [Header("Color")]
    public AnimationCurve gradientCurve;
    public Gradient gradient;

    private RectTransform t;
    private Tween myTween;
    private void Awake() => t = GetComponent<RectTransform>();

    public void SetRepeat(bool state)
    {
        if(state)
            myTween?.Repeat();
    }

    public void Animate()
    {
        myTween?.Cancel();

        myTween = mTween.NewTween(1f)
            .MoveToLocal(t, moveFrom, moveTo, moveCurve)
            .AngleTo(t, angleFrom, angleTo, t.forward, rotateCurve)
            .ScaleTo(t, scaleFrom, scaleTo, scaleCurve)
            .ColorTo(t.GetComponent<Image>(), gradient, gradientCurve);
    }
}
