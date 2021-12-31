using UnityEngine;

public class PanelAnimation : MonoBehaviour
{
    public AnimationCurve moveCurve;
    public AnimationCurve rotateCurve;

    private RectTransform t;
    private void Awake() => t = GetComponent<RectTransform>();

    public void Animate()
    {
        mTween.NewTween(1f)
            .MoveToLocal(t, new Vector3(0f, 100f, 0f), new Vector3(0f, 300f, 0f), moveCurve)
            .AngleTo(t, 40f, t.forward, rotateCurve);
    }
}
