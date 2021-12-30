using UnityEngine;

public partial class mTimeline
{
    /// <summary>
    /// Rotates Transform to face a target
    /// </summary>
    public mTimeline RotateTowardsTarget(Transform follower, Transform Target)
    {
        Quaternion lookDir = Quaternion.FromToRotation(follower.forward, Target.position - follower.position);
        lerpRot.Add(new RotationLerp((i) => follower.rotation = i, follower.rotation, lookDir));
        return this;
    }
    public mTimeline RotateTowardsTarget(Transform follower, Transform Target, AnimationCurve curve)
    {
        Quaternion lookDir = Quaternion.FromToRotation(follower.forward, Target.position - follower.position);
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
}
