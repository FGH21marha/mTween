﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public partial class mTimeline
{
    #region Transfrom operations
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
    #endregion

    #region RectTransform operations
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
    #endregion

    #region Alpha operations
    /// <summary>
    /// Lerps CanvasGroup alpha towards value
    /// </summary>
    public mTimeline AlphaTo(CanvasGroup t, float to)
    {
        lerpFloat.Add(new floatLerp((i) => t.alpha = i, t.alpha, to));
        return this;
    }
    public mTimeline AlphaTo(CanvasGroup t, float to, AnimationCurve curve)
    {
        lerpFloat.Add(new floatLerp((i) => t.alpha = i, t.alpha, to, curve));
        return this;
    }
    public mTimeline AlphaTo(CanvasGroup t, float from, float to)
    {
        lerpFloat.Add(new floatLerp((i) => t.alpha = i, from, to));
        return this;
    }
    public mTimeline AlphaTo(CanvasGroup t, float from, float to, AnimationCurve curve)
    {
        lerpFloat.Add(new floatLerp((i) => t.alpha = i, from, to, curve));
        return this;
    }

    /// <summary>
    /// Lerps Image alpha towards value
    /// </summary>
    public mTimeline AlphaTo(Image t, float to)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, t.color, new Color(t.color.r, t.color.b, t.color.b, to)));
        return this;
    }
    public mTimeline AlphaTo(Image t, float to, AnimationCurve curve)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, t.color, new Color(t.color.r, t.color.b, t.color.b, to), curve));
        return this;
    }
    public mTimeline AlphaTo(Image t, float from, float to)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, new Color(t.color.r, t.color.b, t.color.b, from), new Color(t.color.r, t.color.b, t.color.b, to)));
        return this;
    }
    public mTimeline AlphaTo(Image t, float from, float to, AnimationCurve curve)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, new Color(t.color.r, t.color.b, t.color.b, from), new Color(t.color.r, t.color.b, t.color.b, to), curve));
        return this;
    }

    /// <summary>
    /// Lerps RawImage alpha towards value
    /// </summary>
    public mTimeline AlphaTo(RawImage t, float to)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, t.color, new Color(t.color.r, t.color.b, t.color.b, to)));
        return this;
    }
    public mTimeline AlphaTo(RawImage t, float to, AnimationCurve curve)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, t.color, new Color(t.color.r, t.color.b, t.color.b, to), curve));
        return this;
    }
    public mTimeline AlphaTo(RawImage t, float from, float to)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, new Color(t.color.r, t.color.b, t.color.b, from), new Color(t.color.r, t.color.b, t.color.b, to)));
        return this;
    }
    public mTimeline AlphaTo(RawImage t, float from, float to, AnimationCurve curve)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, new Color(t.color.r, t.color.b, t.color.b, from), new Color(t.color.r, t.color.b, t.color.b, to), curve));
        return this;
    }

    /// <summary>
    /// Lerps Text alpha towards value
    /// </summary>
    public mTimeline AlphaTo(Text t, float to)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, t.color, new Color(t.color.r, t.color.b, t.color.b, to)));
        return this;
    }
    public mTimeline AlphaTo(Text t, float to, AnimationCurve curve)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, t.color, new Color(t.color.r, t.color.b, t.color.b, to), curve));
        return this;
    }
    public mTimeline AlphaTo(Text t, float from, float to)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, new Color(t.color.r, t.color.b, t.color.b, from), new Color(t.color.r, t.color.b, t.color.b, to)));
        return this;
    }
    public mTimeline AlphaTo(Text t, float from, float to, AnimationCurve curve)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, new Color(t.color.r, t.color.b, t.color.b, from), new Color(t.color.r, t.color.b, t.color.b, to), curve));
        return this;
    }

    /// <summary>
    /// Lerps SpriteRenderer alpha towards value
    /// </summary>
    public mTimeline AlphaTo(SpriteRenderer t, float to)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, t.color, new Color(t.color.r, t.color.b, t.color.b, to)));
        return this;
    }
    public mTimeline AlphaTo(SpriteRenderer t, float to, AnimationCurve curve)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, t.color, new Color(t.color.r, t.color.b, t.color.b, to), curve));
        return this;
    }
    public mTimeline AlphaTo(SpriteRenderer t, float from, float to)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, new Color(t.color.r, t.color.b, t.color.b, from), new Color(t.color.r, t.color.b, t.color.b, to)));
        return this;
    }
    public mTimeline AlphaTo(SpriteRenderer t, float from, float to, AnimationCurve curve)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, new Color(t.color.r, t.color.b, t.color.b, from), new Color(t.color.r, t.color.b, t.color.b, to), curve));
        return this;
    }
    #endregion

    #region TextMesh operations
    /// <summary>
    /// Lerps TextMesh Color towards value
    /// </summary>
    public mTimeline ColorTo(TextMesh t, Color to)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, t.color, to));
        return this;
    }
    public mTimeline ColorTo(TextMesh t, Color to, AnimationCurve curve)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, t.color, to, curve));
        return this;
    }
    public mTimeline ColorTo(TextMesh t, Color from, Color to)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, from, to));
        return this;
    }
    public mTimeline ColorTo(TextMesh t, Color from, Color to, AnimationCurve curve)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, from, to, curve));
        return this;
    }

    /// <summary>
    /// Lerps TextMeshPro Color towards value
    /// </summary>
    public mTimeline ColorTo(TextMeshPro t, Color to)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, t.color, to));
        return this;
    }
    public mTimeline ColorTo(TextMeshPro t, Color to, AnimationCurve curve)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, t.color, to, curve));
        return this;
    }
    public mTimeline ColorTo(TextMeshPro t, Color from, Color to)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, from, to));
        return this;
    }
    public mTimeline ColorTo(TextMeshPro t, Color from, Color to, AnimationCurve curve)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, from, to, curve));
        return this;
    }

    /// <summary>
    /// Lerps TextMeshProUGUI Color towards value
    /// </summary>
    public mTimeline ColorTo(TextMeshProUGUI t, Color to)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, t.color, to));
        return this;
    }
    public mTimeline ColorTo(TextMeshProUGUI t, Color to, AnimationCurve curve)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, t.color, to, curve));
        return this;
    }
    public mTimeline ColorTo(TextMeshProUGUI t, Color from, Color to)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, from, to));
        return this;
    }
    public mTimeline ColorTo(TextMeshProUGUI t, Color from, Color to, AnimationCurve curve)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, from, to, curve));
        return this;
    }

    /// <summary>
    /// Lerps TextMesh alpha towards value
    /// </summary>
    public mTimeline AlphaTo(TextMesh t, float to)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, t.color, new Color(t.color.r, t.color.b, t.color.b, to)));
        return this;
    }
    public mTimeline AlphaTo(TextMesh t, float to, AnimationCurve curve)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, t.color, new Color(t.color.r, t.color.b, t.color.b, to), curve));
        return this;
    }
    public mTimeline AlphaTo(TextMesh t, float from, float to)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, new Color(t.color.r, t.color.b, t.color.b, from), new Color(t.color.r, t.color.b, t.color.b, to)));
        return this;
    }
    public mTimeline AlphaTo(TextMesh t, float from, float to, AnimationCurve curve)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, new Color(t.color.r, t.color.b, t.color.b, from), new Color(t.color.r, t.color.b, t.color.b, to), curve));
        return this;
    }

    /// <summary>
    /// Lerps TextMeshPro alpha towards value
    /// </summary>
    public mTimeline AlphaTo(TextMeshPro t, float to)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, t.color, new Color(t.color.r, t.color.b, t.color.b, to)));
        return this;
    }
    public mTimeline AlphaTo(TextMeshPro t, float to, AnimationCurve curve)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, t.color, new Color(t.color.r, t.color.b, t.color.b, to), curve));
        return this;
    }
    public mTimeline AlphaTo(TextMeshPro t, float from, float to)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, new Color(t.color.r, t.color.b, t.color.b, from), new Color(t.color.r, t.color.b, t.color.b, to)));
        return this;
    }
    public mTimeline AlphaTo(TextMeshPro t, float from, float to, AnimationCurve curve)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, new Color(t.color.r, t.color.b, t.color.b, from), new Color(t.color.r, t.color.b, t.color.b, to), curve));
        return this;
    }

    /// <summary>
    /// Lerps TextMeshProUGUI alpha towards value
    /// </summary>
    public mTimeline AlphaTo(TextMeshProUGUI t, float to)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, t.color, new Color(t.color.r, t.color.b, t.color.b, to)));
        return this;
    }
    public mTimeline AlphaTo(TextMeshProUGUI t, float to, AnimationCurve curve)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, t.color, new Color(t.color.r, t.color.b, t.color.b, to), curve));
        return this;
    }
    public mTimeline AlphaTo(TextMeshProUGUI t, float from, float to)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, new Color(t.color.r, t.color.b, t.color.b, from), new Color(t.color.r, t.color.b, t.color.b, to)));
        return this;
    }
    public mTimeline AlphaTo(TextMeshProUGUI t, float from, float to, AnimationCurve curve)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, new Color(t.color.r, t.color.b, t.color.b, from), new Color(t.color.r, t.color.b, t.color.b, to), curve));
        return this;
    }
    #endregion

    #region Light operations
    /// <summary>
    /// Lerps Light Intensity towards value
    /// </summary>
    public mTimeline IntensityTo(Light t, float to)
    {
        lerpFloat.Add(new floatLerp((i) => t.intensity = i, t.intensity, to));
        return this;
    }
    public mTimeline IntensityTo(Light t, float to, AnimationCurve curve)
    {
        lerpFloat.Add(new floatLerp((i) => t.intensity = i, t.intensity, to, curve));
        return this;
    }
    public mTimeline IntensityTo(Light t, float from, float to)
    {
        lerpFloat.Add(new floatLerp((i) => t.intensity = i, from, to));
        return this;
    }
    public mTimeline IntensityTo(Light t, float from, float to, AnimationCurve curve)
    {
        lerpFloat.Add(new floatLerp((i) => t.intensity = i, from, to, curve));
        return this;
    }

    /// <summary>
    /// Lerps Light range towards value
    /// </summary>
    public mTimeline RangeTo(Light t, float to)
    {
        lerpFloat.Add(new floatLerp((i) => t.range = i, t.range, to));
        return this;
    }
    public mTimeline RangeTo(Light t, float to, AnimationCurve curve)
    {
        lerpFloat.Add(new floatLerp((i) => t.range = i, t.range, to, curve));
        return this;
    }
    public mTimeline RangeTo(Light t, float from, float to)
    {
        lerpFloat.Add(new floatLerp((i) => t.range = i, from, to));
        return this;
    }
    public mTimeline RangeTo(Light t, float from, float to, AnimationCurve curve)
    {
        lerpFloat.Add(new floatLerp((i) => t.range = i, from, to, curve));
        return this;
    }

    /// <summary>
    /// Lerps Light Color towards value
    /// </summary>
    public mTimeline ColorTo(Light t, Color to)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, t.color, to));
        return this;
    }
    public mTimeline ColorTo(Light t, Color to, AnimationCurve curve)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, t.color, to, curve));
        return this;
    }
    public mTimeline ColorTo(Light t, Color from, Color to)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, from, to));
        return this;
    }
    public mTimeline ColorTo(Light t, Color from, Color to, AnimationCurve curve)
    {
        lerpColor.Add(new ColorLerp((i) => t.color = i, from, to, curve));
        return this;
    }
    #endregion

    #region Audio operations
    /// <summary>
    /// Lerps Audio volume towards value
    /// </summary>
    public mTimeline VolumeTo(AudioSource t, float to)
    {
        lerpFloat.Add(new floatLerp((i) => t.volume = i, t.volume, to));
        return this;
    }
    public mTimeline VolumeTo(AudioSource t, float to, AnimationCurve curve)
    {
        lerpFloat.Add(new floatLerp((i) => t.volume = i, t.volume, to, curve));
        return this;
    }
    public mTimeline VolumeTo(AudioSource t, float from, float to)
    {
        lerpFloat.Add(new floatLerp((i) => t.volume = i, from, to));
        return this;
    }
    public mTimeline VolumeTo(AudioSource t, float from, float to, AnimationCurve curve)
    {
        lerpFloat.Add(new floatLerp((i) => t.volume = i, from, to, curve));
        return this;
    }

    /// <summary>
    /// Lerps AudioMixerGroup value towards value
    /// </summary>
    public mTimeline VolumeTo(AudioMixerGroup t, string value, float to)
    {
        float o;
        t.audioMixer.GetFloat("Volume", out o);
        lerpFloat.Add(new floatLerp((i) => t.audioMixer.SetFloat(value, i), o, to));
        return this;
    }
    public mTimeline VolumeTo(AudioMixerGroup t, string value, float to, AnimationCurve curve)
    {
        float o;
        t.audioMixer.GetFloat("Volume", out o);
        lerpFloat.Add(new floatLerp((i) => t.audioMixer.SetFloat(value, i), o, to, curve));
        return this;
    }
    public mTimeline VolumeTo(AudioMixerGroup t, string value, float from, float to)
    {
        lerpFloat.Add(new floatLerp((i) => t.audioMixer.SetFloat(value, i), from, to));
        return this;
    }
    public mTimeline VolumeTo(AudioMixerGroup t, string value, float from, float to, AnimationCurve curve)
    {
        lerpFloat.Add(new floatLerp((i) => t.audioMixer.SetFloat(value, i), from, to, curve));
        return this;
    }

    /// <summary>
    /// Lerps Audio pitch towards value
    /// </summary>
    public mTimeline PitchTo(AudioSource t, float to)
    {
        lerpFloat.Add(new floatLerp((i) => t.pitch = i, t.pitch, to));
        return this;
    }
    public mTimeline PitchTo(AudioSource t, float to, AnimationCurve curve)
    {
        lerpFloat.Add(new floatLerp((i) => t.pitch = i, t.pitch, to, curve));
        return this;
    }
    public mTimeline PitchTo(AudioSource t, float from, float to)
    {
        lerpFloat.Add(new floatLerp((i) => t.pitch = i, from, to));
        return this;
    }
    public mTimeline PitchTo(AudioSource t, float from, float to, AnimationCurve curve)
    {
        lerpFloat.Add(new floatLerp((i) => t.pitch = i, from, to, curve));
        return this;
    }
    #endregion
}
