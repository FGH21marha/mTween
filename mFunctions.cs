using UnityEngine;

public partial class mTimeline
{
    /// <summary>
    /// Rotates Transform to face a target
    /// </summary>
    public mTimeline RotateTowardsTarget(Transform follower, Transform Target)
    {
        Vector3 dir = (Target.position - follower.position).normalized;
        Quaternion lookDir = Quaternion.LookRotation(dir);

        lerpRot.Add(new RotationLerp((i) => follower.rotation = i, follower.rotation, lookDir));
        return this;
    }
    public mTimeline RotateTowardsTarget(Transform follower, Transform Target, AnimationCurve curve)
    {
        Vector3 dir = (Target.position - follower.position).normalized;
        Quaternion lookDir = Quaternion.LookRotation(dir);

        lerpRot.Add(new RotationLerp((i) => follower.rotation = i, follower.rotation, lookDir, curve));
        return this;
    }

    /// <summary>
    /// Make a transform face a target transform continiously (instant)
    /// </summary>
    public mTimeline FaceTargetContinuous(Transform t, Transform target)
    {
        lerpTarget.Add(new ContinuousTargetLerp(t, target));
        return this;
    }
    public mTimeline FaceTargetContinuous(Transform t, Transform target, Vector3 worldUp)
    {
        lerpTarget.Add(new ContinuousTargetLerp(t, target, worldUp));
        return this;
    }

    /// <summary>
    /// Moves transform towards position
    /// </summary>
    public mTimeline MoveTo(Transform t, Vector3 to)
    {
        lerpVector3.Add(new Vector3Lerp((i) => t.position = i, t.position, to));
        return this;
    }
    public mTimeline MoveTo(Transform t, Vector3 from, Vector3 to)
    {
        lerpVector3.Add(new Vector3Lerp((i) => t.position = i, from, to));
        return this;
    }
    public mTimeline MoveTo(Transform t, Vector3 to, AnimationCurve curve)
    {
        lerpVector3.Add(new Vector3Lerp((i) => t.position = i, t.position, to, curve));
        return this;
    }
    public mTimeline MoveTo(Transform t, Vector3 from, Vector3 to, AnimationCurve curve)
    {
        lerpVector3.Add(new Vector3Lerp((i) => t.position = i, from, to, curve));
        return this;
    }
    public mTimeline MoveTo(Transform t, Vector2 to)
    {
        lerpVector2.Add(new Vector2Lerp((i) => t.position = i, t.position, to));
        return this;
    }
    public mTimeline MoveTo(Transform t, Vector2 from, Vector2 to)
    {
        lerpVector2.Add(new Vector2Lerp((i) => t.position = i, from, to));
        return this;
    }
    public mTimeline MoveTo(Transform t, Vector2 to, AnimationCurve curve)
    {
        lerpVector2.Add(new Vector2Lerp((i) => t.position = i, t.position, to, curve));
        return this;
    }
    public mTimeline MoveTo(Transform t, Vector2 from, Vector2 to, AnimationCurve curve)
    {
        lerpVector2.Add(new Vector2Lerp((i) => t.position = i, from, to, curve));
        return this;
    }

    /// <summary>
    /// Moves transform towards position in local space
    /// </summary>
    public mTimeline MoveToLocal(Transform t, Vector3 to)
    {
        lerpVector3.Add(new Vector3Lerp((i) => t.localPosition = i, t.localPosition, to));
        return this;
    }
    public mTimeline MoveToLocal(Transform t, Vector3 from, Vector3 to)
    {
        lerpVector3.Add(new Vector3Lerp((i) => t.localPosition = i, from, to));
        return this;
    }
    public mTimeline MoveToLocal(Transform t, Vector3 to, AnimationCurve curve)
    {
        lerpVector3.Add(new Vector3Lerp((i) => t.localPosition = i, t.localPosition, to, curve));
        return this;
    }
    public mTimeline MoveToLocal(Transform t, Vector3 from, Vector3 to, AnimationCurve curve)
    {
        lerpVector3.Add(new Vector3Lerp((i) => t.localPosition = i, from, to, curve));
        return this;
    }
    public mTimeline MoveToLocal(Transform t, Vector2 to)
    {
        lerpVector2.Add(new Vector2Lerp((i) => t.localPosition = i, t.localPosition, to));
        return this;
    }
    public mTimeline MoveToLocal(Transform t, Vector2 from, Vector2 to)
    {
        lerpVector2.Add(new Vector2Lerp((i) => t.localPosition = i, from, to));
        return this;
    }
    public mTimeline MoveToLocal(Transform t, Vector2 to, AnimationCurve curve)
    {
        lerpVector2.Add(new Vector2Lerp((i) => t.localPosition = i, t.localPosition, to, curve));
        return this;
    }
    public mTimeline MoveToLocal(Transform t, Vector2 from, Vector2 to, AnimationCurve curve)
    {
        lerpVector2.Add(new Vector2Lerp((i) => t.localPosition = i, from, to, curve));
        return this;
    }

    /// <summary>
    /// Scales Transform towards size
    /// </summary>
    public mTimeline ScaleTo(Transform t, Vector2 to)
    {
        lerpVector2.Add(new Vector2Lerp((i) => t.localScale = i, t.localScale, to));
        return this;
    }
    public mTimeline ScaleTo(Transform t, Vector2 from, Vector2 to)
    {
        lerpVector2.Add(new Vector2Lerp((i) => t.localScale = i, from, to));
        return this;
    }
    public mTimeline ScaleTo(Transform t, Vector2 to, AnimationCurve curve)
    {
        lerpVector2.Add(new Vector2Lerp((i) => t.localScale = i, t.localScale, to, curve));
        return this;
    }
    public mTimeline ScaleTo(Transform t, Vector2 from, Vector2 to, AnimationCurve curve)
    {
        lerpVector2.Add(new Vector2Lerp((i) => t.localScale = i, from, to, curve));
        return this;
    }
    public mTimeline ScaleTo(Transform t, Vector3 to)
    {
        lerpVector3.Add(new Vector3Lerp((i) => t.localScale = i, t.localScale, to));
        return this;
    }
    public mTimeline ScaleTo(Transform t, Vector3 from, Vector3 to)
    {
        lerpVector3.Add(new Vector3Lerp((i) => t.localScale = i, from, to));
        return this;
    }
    public mTimeline ScaleTo(Transform t, Vector3 to, AnimationCurve curve)
    {
        lerpVector3.Add(new Vector3Lerp((i) => t.localScale = i, t.localScale, to, curve));
        return this;
    }
    public mTimeline ScaleTo(Transform t, Vector3 from, Vector3 to, AnimationCurve curve)
    {
        lerpVector3.Add(new Vector3Lerp((i) => t.localScale = i, from, to, curve));
        return this;
    }

    /// <summary>
    /// Rotates Transform towards rotation
    /// </summary>
    public mTimeline RotateTo(Transform t, Quaternion to)
    {
        lerpRot.Add(new RotationLerp((i) => t.rotation = i, t.rotation, to));
        return this;
    }
    public mTimeline RotateTo(Transform t, Quaternion from, Quaternion to)
    {
        lerpRot.Add(new RotationLerp((i) => t.rotation = i, from, to));
        return this;
    }
    public mTimeline RotateTo(Transform t, Quaternion to, AnimationCurve curve)
    {
        lerpRot.Add(new RotationLerp((i) => t.rotation = i, t.rotation, to, curve));
        return this;
    }
    public mTimeline RotateTo(Transform t, Quaternion from, Quaternion to, AnimationCurve curve)
    {
        lerpRot.Add(new RotationLerp((i) => t.rotation = i, from, to, curve));
        return this;
    }
    public mTimeline RotateTo(Transform t, Vector3 to)
    {
        lerpRot.Add(new RotationLerp((i) => t.rotation = i, t.rotation, Quaternion.Euler(to)));
        return this;
    }
    public mTimeline RotateTo(Transform t, Vector3 from, Vector3 to)
    {
        lerpRot.Add(new RotationLerp((i) => t.rotation = i, Quaternion.Euler(from), Quaternion.Euler(to)));
        return this;
    }
    public mTimeline RotateTo(Transform t, Vector3 to, AnimationCurve curve)
    {
        lerpRot.Add(new RotationLerp((i) => t.rotation = i, t.rotation, Quaternion.Euler(to), curve));
        return this;
    }
    public mTimeline RotateTo(Transform t, Vector3 from, Vector3 to, AnimationCurve curve)
    {
        lerpRot.Add(new RotationLerp((i) => t.rotation = i, Quaternion.Euler(from), Quaternion.Euler(to), curve));
        return this;
    }

    /// <summary>
    /// Rotates Transform towards rotation in local space
    /// </summary>
    public mTimeline RotateToLocal(Transform t, Quaternion to)
    {
        lerpRot.Add(new RotationLerp((i) => t.localRotation = i, t.localRotation, to));
        return this;
    }
    public mTimeline RotateToLocal(Transform t, Quaternion from, Quaternion to)
    {
        lerpRot.Add(new RotationLerp((i) => t.localRotation = i, from, to));
        return this;
    }
    public mTimeline RotateToLocal(Transform t, Quaternion to, AnimationCurve curve)
    {
        lerpRot.Add(new RotationLerp((i) => t.localRotation = i, t.localRotation, to, curve));
        return this;
    }
    public mTimeline RotateToLocal(Transform t, Quaternion from, Quaternion to, AnimationCurve curve)
    {
        lerpRot.Add(new RotationLerp((i) => t.localRotation = i, from, to, curve));
        return this;
    }
    public mTimeline RotateToLocal(Transform t, Vector3 to)
    {
        lerpRot.Add(new RotationLerp((i) => t.localRotation = i, t.localRotation, Quaternion.Euler(to)));
        return this;
    }
    public mTimeline RotateToLocal(Transform t, Vector3 from, Vector3 to)
    {
        lerpRot.Add(new RotationLerp((i) => t.localRotation = i, Quaternion.Euler(from), Quaternion.Euler(to)));
        return this;
    }
    public mTimeline RotateToLocal(Transform t, Vector3 to, AnimationCurve curve)
    {
        lerpRot.Add(new RotationLerp((i) => t.localRotation = i, t.localRotation, Quaternion.Euler(to), curve));
        return this;
    }
    public mTimeline RotateToLocal(Transform t, Vector3 from, Vector3 to, AnimationCurve curve)
    {
        lerpRot.Add(new RotationLerp((i) => t.localRotation = i, Quaternion.Euler(from), Quaternion.Euler(to), curve));
        return this;
    }

    /// <summary>
    /// Moves RectTransform towards position
    /// </summary>
    public mTimeline MoveTo(RectTransform t, Vector3 to)
    {
        lerpVector3.Add(new Vector3Lerp((i) => t.anchoredPosition = i, t.anchoredPosition, to));
        return this;
    }
    public mTimeline MoveTo(RectTransform t, Vector3 from, Vector3 to)
    {
        lerpVector3.Add(new Vector3Lerp((i) => t.anchoredPosition = i, from, to));
        return this;
    }
    public mTimeline MoveTo(RectTransform t, Vector3 to, AnimationCurve curve)
    {
        lerpVector3.Add(new Vector3Lerp((i) => t.anchoredPosition = i, t.anchoredPosition, to, curve));
        return this;
    }
    public mTimeline MoveTo(RectTransform t, Vector3 from, Vector3 to, AnimationCurve curve)
    {
        lerpVector3.Add(new Vector3Lerp((i) => t.anchoredPosition = i, from, to, curve));
        return this;
    }
    public mTimeline MoveTo(RectTransform t, Vector2 to)
    {
        lerpVector2.Add(new Vector2Lerp((i) => t.anchoredPosition = i, t.anchoredPosition, to));
        return this;
    }
    public mTimeline MoveTo(RectTransform t, Vector2 from, Vector2 to)
    {
        lerpVector2.Add(new Vector2Lerp((i) => t.anchoredPosition = i, from, to));
        return this;
    }
    public mTimeline MoveTo(RectTransform t, Vector2 to, AnimationCurve curve)
    {
        lerpVector2.Add(new Vector2Lerp((i) => t.anchoredPosition = i, t.anchoredPosition, to, curve));
        return this;
    }
    public mTimeline MoveTo(RectTransform t, Vector2 from, Vector2 to, AnimationCurve curve)
    {
        lerpVector2.Add(new Vector2Lerp((i) => t.anchoredPosition = i, from, to, curve));
        return this;
    }

    /// <summary>
    /// Scales RectTransform towards size
    /// </summary>
    public mTimeline ScaleTo(RectTransform t, Vector2 to)
    {
        lerpVector2.Add(new Vector2Lerp((i) => t.sizeDelta = i, t.sizeDelta, to));
        return this;
    }
    public mTimeline ScaleTo(RectTransform t, Vector2 from, Vector2 to)
    {
        lerpVector2.Add(new Vector2Lerp((i) => t.sizeDelta = i, from, to));
        return this;
    }
    public mTimeline ScaleTo(RectTransform t, Vector2 to, AnimationCurve curve)
    {
        lerpVector2.Add(new Vector2Lerp((i) => t.sizeDelta = i, t.sizeDelta, to, curve));
        return this;
    }
    public mTimeline ScaleTo(RectTransform t, Vector2 from, Vector2 to, AnimationCurve curve)
    {
        lerpVector2.Add(new Vector2Lerp((i) => t.sizeDelta = i, from, to, curve));
        return this;
    }

    /// <summary>
    /// Rotates RectTransform towards rotation
    /// </summary>
    public mTimeline RotateTo(RectTransform t, Quaternion to)
    {
        lerpRot.Add(new RotationLerp((i) => t.rotation = i, t.rotation, to));
        return this;
    }
    public mTimeline RotateTo(RectTransform t, Quaternion from, Quaternion to)
    {
        lerpRot.Add(new RotationLerp((i) => t.rotation = i, from, to));
        return this;
    }
    public mTimeline RotateTo(RectTransform t, Quaternion to, AnimationCurve curve)
    {
        lerpRot.Add(new RotationLerp((i) => t.rotation = i, t.rotation, to, curve));
        return this;
    }
    public mTimeline RotateTo(RectTransform t, Quaternion from, Quaternion to, AnimationCurve curve)
    {
        lerpRot.Add(new RotationLerp((i) => t.rotation = i, from, to, curve));
        return this;
    }
    public mTimeline RotateTo(RectTransform t, Vector3 to)
    {
        lerpRot.Add(new RotationLerp((i) => t.rotation = i, t.rotation, Quaternion.Euler(to)));
        return this;
    }
    public mTimeline RotateTo(RectTransform t, Vector3 from, Vector3 to)
    {
        lerpRot.Add(new RotationLerp((i) => t.rotation = i, Quaternion.Euler(from), Quaternion.Euler(to)));
        return this;
    }
    public mTimeline RotateTo(RectTransform t, Vector3 to, AnimationCurve curve)
    {
        lerpRot.Add(new RotationLerp((i) => t.rotation = i, t.rotation, Quaternion.Euler(to), curve));
        return this;
    }
    public mTimeline RotateTo(RectTransform t, Vector3 from, Vector3 to, AnimationCurve curve)
    {
        lerpRot.Add(new RotationLerp((i) => t.rotation = i, Quaternion.Euler(from), Quaternion.Euler(to), curve));
        return this;
    }

    /// <summary>
    /// Rotates RectTransform towards rotation in local space
    /// </summary>
    public mTimeline RotateToLocal(RectTransform t, Quaternion to)
    {
        lerpRot.Add(new RotationLerp((i) => t.localRotation = i, t.localRotation, to));
        return this;
    }
    public mTimeline RotateToLocal(RectTransform t, Quaternion from, Quaternion to)
    {
        lerpRot.Add(new RotationLerp((i) => t.localRotation = i, from, to));
        return this;
    }
    public mTimeline RotateToLocal(RectTransform t, Quaternion to, AnimationCurve curve)
    {
        lerpRot.Add(new RotationLerp((i) => t.localRotation = i, t.localRotation, to, curve));
        return this;
    }
    public mTimeline RotateToLocal(RectTransform t, Quaternion from, Quaternion to, AnimationCurve curve)
    {
        lerpRot.Add(new RotationLerp((i) => t.localRotation = i, from, to, curve));
        return this;
    }
    public mTimeline RotateToLocal(RectTransform t, Vector3 to)
    {
        lerpRot.Add(new RotationLerp((i) => t.localRotation = i, t.localRotation, Quaternion.Euler(to)));
        return this;
    }
    public mTimeline RotateToLocal(RectTransform t, Vector3 from, Vector3 to)
    {
        lerpRot.Add(new RotationLerp((i) => t.localRotation = i, Quaternion.Euler(from), Quaternion.Euler(to)));
        return this;
    }
    public mTimeline RotateToLocal(RectTransform t, Vector3 to, AnimationCurve curve)
    {
        lerpRot.Add(new RotationLerp((i) => t.localRotation = i, t.localRotation, Quaternion.Euler(to), curve));
        return this;
    }
    public mTimeline RotateToLocal(RectTransform t, Vector3 from, Vector3 to, AnimationCurve curve)
    {
        lerpRot.Add(new RotationLerp((i) => t.localRotation = i, Quaternion.Euler(from), Quaternion.Euler(to), curve));
        return this;
    }
}
