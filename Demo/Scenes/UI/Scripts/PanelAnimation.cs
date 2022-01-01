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
    private bool repeat = false;
    private bool reset = true;
    private void Awake() => t = GetComponent<RectTransform>();

    public void Repeat(bool state)
    {
        repeat = state;
        myTween?.Repeat(state);
    }
    public void Reset(bool state)
    {
        reset = state;
        myTween?.RestoreOnCancel(state);
    }

    public void Pause() => myTween?.Pause();
    public void Resume() => myTween?.Resume();

    public void Animate()
    {
        Cancel();

        myTween = mTween.NewTween(1f)
            .Repeat(repeat)
            .RestoreOnCancel(reset)
            .MoveTo(t, moveFrom, moveTo, moveCurve)
            .AngleTo(t, angleFrom, angleTo, t.forward, rotateCurve)
            .ScaleTo(t, scaleFrom, scaleTo, scaleCurve)
            .ColorTo(t.GetComponent<Image>(), gradient, gradientCurve);
    }

    public void Cancel() => myTween?.Cancel();
}
