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
}
