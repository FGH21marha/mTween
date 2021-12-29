using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

[Serializable] public class mTimeline
{
    string ID;
    public string GetID() => ID;

    Action onStart;
    Action<float> onUpdate01;
    Action<float> onUpdateFloat;
    Action onUpdate;
    Action onCompletedRun;
    Action onComplete;
    Action onCancel;
    Action onPause;
    Action onContinue;

    public float duration;
    public float durationWithDelay;
    public float unscaledProgress;
    public float progress;
    public float pauseTime;
    public float unscaledPauseTime;
    public float intervalTime;
    public float unscaledIntervalTime;

    public int repeatCount;
    public int activeRepeatCount;

    public bool canceled;
    public bool paused;
    public bool interval;
    public bool onInterval;
    public bool repeat;
    public bool completeTriggeredOnCancel;
    public bool completeLoopTriggeredOnCancel;

    public float min = 0f;
    public float max = 1f;

    public AnimationCurve curve;

    /// <summary>
    /// Creates a new delayed action with a duration
    /// </summary>
    public mTimeline(float duration)
    {
        curve = new AnimationCurve();
        curve.AddKey(0f, 0f);
        curve.AddKey(1f, 1f);
        this.duration = duration;
        durationWithDelay = duration;
        unscaledProgress = 0f;
    }
    public mTimeline(GameObject id, float duration)
    {
        curve = new AnimationCurve();
        curve.AddKey(0f, 0f);
        curve.AddKey(1f, 1f);
        this.duration = duration;
        durationWithDelay = duration;
        ID = id.GetInstanceID().ToString();
        unscaledProgress = 0f;
    }
    public mTimeline(string id, float duration)
    {
        curve = new AnimationCurve();
        curve.AddKey(0f, 0f);
        curve.AddKey(1f, 1f);
        this.duration = duration;
        durationWithDelay = duration;
        ID = id;
        unscaledProgress = 0f;
    }

    /// <summary>
    /// Returns progress based on input curve
    /// </summary>
    public mTimeline SetCurve(AnimationCurve newCurve)
    {
        curve = newCurve;
        return this;
    }

    /// <summary>
    /// Repeats the action
    /// </summary>
    public mTimeline Repeat()
    {
        repeat = true;
        return this;
    }
    public mTimeline Repeat(int n)
    {
        repeat = true;
        repeatCount = n;
        return this;
    }

    /// <summary>
    /// Cancel event immediately
    /// </summary>
    public mTimeline Cancel()
    {
        this.canceled = true;
        return this;
    }

    /// <summary>
    /// Executes the event n times
    /// </summary>
    public mTimeline ExecuteNTimes(int n)
    {
        repeat = true;
        repeatCount = n - 1;
        return this;
    }

    /// <summary>
    /// Delay between action repetition
    /// </summary>
    public mTimeline SetInterval(float duration)
    {
        interval = true;
        if (duration > 0f)
        {
            intervalTime = duration;
        }
        return this;
    }

    /// <summary>
    /// Plays the event in reverse
    /// </summary>
    public mTimeline PlayReversed()
    {
        float a = min;
        float b = max;
        min = b;
        max = a;
        return this;
    }

    /// <summary>
    /// Triggers an action at the start of an event
    /// </summary>
    public mTimeline SetOnStart(Action onStart)
    {
        this.onStart = onStart;
        return this;
    }

    /// <summary>
    /// Triggers an action for every update of the event. Returns progress from 0 to 1 for the duration of the event
    /// </summary>
    public mTimeline SetOnUpdate01(Action<float> onUpdate)
    {
        onUpdate01 = onUpdate;
        return this;
    }

    /// <summary>
    /// Triggers an action for every update of the event
    /// </summary>
    public mTimeline SetOnUpdate(Action onUpdate)
    {
        this.onUpdate = onUpdate;
        return this;
    }
    public mTimeline SetOnUpdate(Action<float> onUpdate)
    {
        onUpdateFloat = onUpdate;
        return this;
    }

    /// <summary>
    /// Triggers an action on full event completion
    /// </summary>
    public mTimeline SetOnComplete(Action onComplete)
    {
        this.onComplete = onComplete;
        return this;
    }
    public mTimeline SetOnComplete(Action onComplete, bool triggerOnComplete)
    {
        this.onComplete = onComplete;
        completeTriggeredOnCancel = triggerOnComplete;
        return this;
    }

    /// <summary>
    /// Triggers an action if the event is canceled
    /// </summary>
    public mTimeline SetOnCancel(Action onCancel)
    {
        this.onCancel = onCancel;
        return this;
    }

    /// <summary>
    /// Triggers an action if the event is paused
    /// </summary>
    public mTimeline SetOnPause(Action onPause)
    {
        this.onPause = onPause;
        return this;
    }

    /// <summary>
    /// Triggers an action if the event is continued
    /// </summary>
    public mTimeline SetOnContinue(Action onContinue)
    {
        this.onContinue = onContinue;
        return this;
    }

    /// <summary>
    /// Triggers an action on partial event completion, e.g. when it has done a full cycle and is going to repeat
    /// </summary>
    public mTimeline SetOnRepeat(Action onFinished)
    {
        this.onCompletedRun = onFinished;
        return this;
    }
    public mTimeline SetOnRepeat(Action onFinished, bool triggerOnComplete)
    {
        this.onCompletedRun = onFinished;
        completeLoopTriggeredOnCancel = triggerOnComplete;
        return this;
    }

    /// <summary>
    /// Sets a unique ID for this event
    /// </summary>
    public mTimeline SetID()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        System.Random random = new System.Random();

        ID = new string(Enumerable.Repeat(chars, 10).Select(s => s[random.Next(s.Length)]).ToArray());
        return this;
    }
    public mTimeline SetID(string id)
    {
        ID = id;
        return this;
    }
    public mTimeline SetID(GameObject id)
    {
        ID = id.GetInstanceID().ToString();
        return this;
    }

    /// <summary>
    /// Set progress min max. onUpdate returns progress from the new min max values
    /// </summary>
    public mTimeline SetMinMax(float min, float max)
    {
        this.min = min;
        this.max = max;
        return this;
    }

    /// <summary>
    /// Calls action after t seconds during the event
    /// </summary>
    public mTimeline CallOnTime(float t, Action a)
    {
        if (t >= 0f && a != null)
            customActions.Add(new CustomAction(t, a));

        return this;
    }
    /// <summary>
    /// Calls action after t seconds during the event, scaling determines if the time used is scaled (0 to 1) or unscaled time (0 to event length)
    /// </summary>
    public mTimeline CallOnTime(float t, Action a, bool scaling)
    {
        if (t >= 0f && a != null)
            customActions.Add(new CustomAction(t, a, scaling));

        return this;
    }

    public mTimeline ResetCustomActionsList()
    {
        if (completedCustomActions.Count == 0f) return this;

        for (int i = 0; i < completedCustomActions.Count; i++)
            customActions.Add(completedCustomActions[i]);

        completedCustomActions.Clear();

        return this;
    }

    /// <summary>
    /// Make a transform face a target transform continiously (instant)
    /// </summary>
    public mTimeline FaceTargetContinuous(Transform t, Transform target)
    {
        ContinuousTargetLerp newLerp = new ContinuousTargetLerp(t, target);
        lerpTarget.Add(newLerp);
        return this;
    }
    public mTimeline FaceTargetContinuous(Transform t, Transform target, Vector3 worldUp)
    {
        ContinuousTargetLerp newLerp = new ContinuousTargetLerp(t, target, worldUp);
        lerpTarget.Add(newLerp);
        return this;
    }

    /// <summary>
    /// Make a transform face a target transform
    /// </summary>
    public mTimeline LerpRotateToTarget(Transform t, Transform Target)
    {
        RotationLerp newLerp = new RotationLerp(t, t.rotation, Quaternion.FromToRotation(t.forward, Target.position - t.position), false);
        lerpRot.Add(newLerp);
        return this;
    }
    public mTimeline LerpRotateToTarget(Transform t, Transform Target, AnimationCurve curve)
    {
        RotationLerp newLerp = new RotationLerp(t, t.rotation, Quaternion.FromToRotation(t.forward, Target.position - t.position), false, curve);
        lerpRot.Add(newLerp);
        return this;
    }
    public mTimeline LerpLocalRotateToTarget(Transform t, Transform Target)
    {
        RotationLerp newLerp = new RotationLerp(t, t.rotation, Quaternion.FromToRotation(t.forward, Target.position - t.position), true);
        lerpRot.Add(newLerp);
        return this;
    }
    public mTimeline LerpLocalRotateToTarget(Transform t, Transform Target, AnimationCurve curve)
    {
        RotationLerp newLerp = new RotationLerp(t, t.rotation, Quaternion.FromToRotation(t.forward, Target.position - t.position), true, curve);
        lerpRot.Add(newLerp);
        return this;
    }

    public mTimeline LerpColor(Action<Color> sr, Color startColor, Color endColor)
    {
        List<Color> colors = new List<Color>();
        colors.Add(startColor);
        colors.Add(endColor);

        Gradient gradient = new Gradient();
        GradientColorKey[] colorKeys = new GradientColorKey[colors.Count];
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[colors.Count];

        for (int i = 0; i < colorKeys.Length; i++)
        {
            colorKeys[i].color = colors[i];
            colorKeys[i].time = i / (colorKeys.Length - 1);
        }

        for (int i = 0; i < alphaKeys.Length; i++)
        {
            alphaKeys[i].alpha = colors[i].a;
            alphaKeys[i].time = i / (alphaKeys.Length - 1);
        }

        gradient.SetKeys(colorKeys, alphaKeys);

        ColorLerp newLerp = new ColorLerp(sr, gradient);
        lerpColor.Add(newLerp);
        return this;
    }
    public mTimeline LerpColor(Action<Color> sr, Color startColor, Color endColor, AnimationCurve curve)
    {
        List<Color> colors = new List<Color>();
        colors.Add(startColor);
        colors.Add(endColor);

        Gradient gradient = new Gradient();
        GradientColorKey[] colorKeys = new GradientColorKey[colors.Count];
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[colors.Count];

        for (int i = 0; i < colorKeys.Length; i++)
        {
            colorKeys[i].color = colors[i];
            colorKeys[i].time = i / (colorKeys.Length - 1);
        }

        for (int i = 0; i < alphaKeys.Length; i++)
        {
            alphaKeys[i].alpha = colors[i].a;
            alphaKeys[i].time = i / (alphaKeys.Length - 1);
        }

        gradient.SetKeys(colorKeys, alphaKeys);

        ColorLerp newLerp = new ColorLerp(sr, gradient, curve);
        lerpColor.Add(newLerp);
        return this;
    }
    public mTimeline LerpColor(Action<Color> sr, Gradient g)
    {
        ColorLerp newLerp = new ColorLerp(sr, g);
        lerpColor.Add(newLerp);
        return this;
    }
    public mTimeline LerpColor(Action<Color> sr, Gradient g, AnimationCurve curve)
    {
        ColorLerp newLerp = new ColorLerp(sr, g, curve);
        lerpColor.Add(newLerp);
        return this;
    }
    public mTimeline LerpColor(Action<Color> sr, List<Color> myColors)
    {
        List<Color> colors = myColors;
        Gradient gradient = new Gradient();
        GradientColorKey[] colorKeys = new GradientColorKey[colors.Count];
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[colors.Count];

        for (int i = 0; i < colorKeys.Length; i++)
        {
            colorKeys[i].color = colors[i];
            colorKeys[i].time = i / (colorKeys.Length - 1);
        }

        for (int i = 0; i < alphaKeys.Length; i++)
        {
            alphaKeys[i].alpha = colors[i].a;
            alphaKeys[i].time = i / (alphaKeys.Length - 1);
        }

        gradient.SetKeys(colorKeys, alphaKeys);

        ColorLerp newLerp = new ColorLerp(sr, gradient);
        lerpColor.Add(newLerp);
        return this;
    }
    public mTimeline LerpColor(Action<Color> sr, List<Color> myColors, AnimationCurve curve)
    {
        List<Color> colors = myColors;
        Gradient gradient = new Gradient();
        GradientColorKey[] colorKeys = new GradientColorKey[colors.Count];
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[colors.Count];

        for (int i = 0; i < colorKeys.Length; i++)
        {
            colorKeys[i].color = colors[i];
            colorKeys[i].time = i / (colorKeys.Length - 1);
        }

        for (int i = 0; i < alphaKeys.Length; i++)
        {
            alphaKeys[i].alpha = colors[i].a;
            alphaKeys[i].time = i / (alphaKeys.Length - 1);
        }

        gradient.SetKeys(colorKeys, alphaKeys);

        ColorLerp newLerp = new ColorLerp(sr, gradient, curve);
        lerpColor.Add(newLerp);
        return this;
    }

    public mTimeline LerpPosition(Transform t, Vector3 from, Vector3 to)
    {
        PositionLerp newLerp = new PositionLerp(t, from, to, false);

        lerpPos.Add(newLerp);

        return this;
    }
    public mTimeline LerpLocalPosition(Transform t, Vector3 from, Vector3 to)
    {
        PositionLerp newLerp = new PositionLerp(t, from, to, true);

        lerpPos.Add(newLerp);

        return this;
    }
    public mTimeline LerpPosition(Transform t, Vector3 from, Vector3 to, AnimationCurve curve)
    {
        PositionLerp newLerp = new PositionLerp(t, from, to, false, curve);

        lerpPos.Add(newLerp);

        return this;
    }
    public mTimeline LerpLocalPosition(Transform t, Vector3 from, Vector3 to, AnimationCurve curve)
    {
        PositionLerp newLerp = new PositionLerp(t, from, to, true, curve);

        lerpPos.Add(newLerp);

        return this;
    }

    public mTimeline MoveTo(Transform t, Vector3 to, bool localSpace)
    {
        PositionLerp newLerp = new PositionLerp(t, to, localSpace);

        lerpPos.Add(newLerp);

        return this;
    }
    public mTimeline MoveTo(Transform t, Vector3 to, bool localSpace, AnimationCurve curve)
    {
        PositionLerp newLerp = new PositionLerp(t, to, localSpace, curve);

        lerpPos.Add(newLerp);

        return this;
    }

    public mTimeline LerpRotation(Transform t, Quaternion from, Quaternion to)
    {
        RotationLerp newLerp = new RotationLerp(t, from, to, false);

        lerpRot.Add(newLerp);

        return this;
    }
    public mTimeline LerpLocalRotation(Transform t, Quaternion from, Quaternion to)
    {
        RotationLerp newLerp = new RotationLerp(t, from, to, true);

        lerpRot.Add(newLerp);

        return this;
    }
    public mTimeline LerpRotation(Transform t, Quaternion from, Quaternion to, AnimationCurve curve)
    {
        RotationLerp newLerp = new RotationLerp(t, from, to, false, curve);

        lerpRot.Add(newLerp);

        return this;
    }
    public mTimeline LerpLocalRotation(Transform t, Quaternion from, Quaternion to, AnimationCurve curve)
    {
        RotationLerp newLerp = new RotationLerp(t, from, to, true, curve);

        lerpRot.Add(newLerp);

        return this;
    }

    public mTimeline LerpScale(Transform t, Vector3 from, Vector3 to)
    {
        ScaleLerp newLerp = new ScaleLerp(t, from, to);

        lerpScale.Add(newLerp);

        return this;
    }
    public mTimeline LerpScale(Transform t, Vector3 from, Vector3 to, AnimationCurve curve)
    {
        ScaleLerp newLerp = new ScaleLerp(t, from, to, curve);

        lerpScale.Add(newLerp);

        return this;
    }

    protected struct PositionLerp
    {
        public Transform transform;
        public Vector3 from;
        public Vector3 to;
        public bool localSpace;
        public AnimationCurve curve;

        public PositionLerp(Transform target, Vector3 from, Vector3 to, bool localSpace)
        {
            this.transform = target;
            this.from = from;
            this.to = to;
            this.localSpace = localSpace;
            curve = new AnimationCurve();
            curve.AddKey(0f, 0f);
            curve.AddKey(1f, 1f);
        }

        public PositionLerp(Transform target, Vector3 from, Vector3 to, bool localSpace, AnimationCurve curve)
        {
            this.transform = target;
            this.from = from;
            this.to = to;
            this.localSpace = localSpace;
            this.curve = curve;
        }

        public PositionLerp(Transform target, Vector3 to, bool localSpace)
        {
            this.transform = target;
            this.from = localSpace ? target.localPosition : target.position;
            this.to = to;
            this.localSpace = localSpace;
            curve = new AnimationCurve();
            curve.AddKey(0f, 0f);
            curve.AddKey(1f, 1f);
        }

        public PositionLerp(Transform target, Vector3 to, bool localSpace, AnimationCurve curve)
        {
            this.transform = target;
            this.from = localSpace ? target.localPosition : target.position;
            this.to = to;
            this.localSpace = localSpace;
            this.curve = curve;
        }
    }
    protected List<PositionLerp> lerpPos = new List<PositionLerp>();

    protected struct RotationLerp
    {
        public Transform transform;
        public Quaternion from;
        public Quaternion to;
        public bool localSpace;
        public AnimationCurve curve;

        public RotationLerp(Transform target, Quaternion from, Quaternion to, bool localSpace, AnimationCurve curve)
        {
            this.transform = target;
            this.from = from;
            this.to = to;
            this.localSpace = localSpace;
            this.curve = curve;
        }
        public RotationLerp(Transform target, Quaternion from, Quaternion to, bool localSpace)
        {
            this.transform = target;
            this.from = from;
            this.to = to;
            this.localSpace = localSpace;
            curve = new AnimationCurve();
            curve.AddKey(0f, 0f);
            curve.AddKey(1f, 1f);
        }
    }
    protected List<RotationLerp> lerpRot = new List<RotationLerp>();

    protected struct ScaleLerp
    {
        public Transform transform;
        public Vector3 from;
        public Vector3 to;
        public AnimationCurve curve;

        public ScaleLerp(Transform target, Vector3 from, Vector3 to)
        {
            this.transform = target;
            this.from = from;
            this.to = to;
            curve = new AnimationCurve();
            curve.AddKey(0f, 0f);
            curve.AddKey(1f, 1f);
        }
        public ScaleLerp(Transform target, Vector3 from, Vector3 to, AnimationCurve curve)
        {
            this.transform = target;
            this.from = from;
            this.to = to;
            this.curve = curve;
        }
    }
    protected List<ScaleLerp> lerpScale = new List<ScaleLerp>();

    protected struct ColorLerp
    {
        public Action<Color> color;
        public Gradient gradient;
        public AnimationCurve curve;

        public ColorLerp(Action<Color> sr, Gradient gradient)
        {
            this.color = sr;
            this.gradient = gradient;
            this.curve = new AnimationCurve();
            curve.AddKey(0f, 0f);
            curve.AddKey(1f, 1f);
        }
        public ColorLerp(Action<Color> sr, Gradient gradient, AnimationCurve curve)
        {
            this.color = sr;
            this.gradient = gradient;
            this.curve = curve;
        }
    }
    protected List<ColorLerp> lerpColor = new List<ColorLerp>();

    protected struct ContinuousTargetLerp
    {
        public Transform transform;
        public Transform target;
        public Vector3 worldUp;

        public ContinuousTargetLerp(Transform t, Transform Target)
        {
            this.transform = t;
            this.target = Target;
            this.worldUp = Vector3.up;
        }
        public ContinuousTargetLerp(Transform t, Transform target, Vector3 WorldUp)
        {
            this.transform = t;
            this.target = target;
            this.worldUp = WorldUp;
        }
    }
    protected List<ContinuousTargetLerp> lerpTarget = new List<ContinuousTargetLerp>();

    protected struct CustomAction
    {
        public float time;
        public Action action;
        public bool scale;

        public CustomAction(float t, Action a)
        {
            this.time = t;
            this.action = a;
            this.scale = false;
        }
        public CustomAction(float t, Action a, bool scaling)
        {
            this.time = t;
            this.action = a;
            this.scale = scaling;
        }
    }
    protected List<CustomAction> customActions = new List<CustomAction>();
    protected List<CustomAction> completedCustomActions = new List<CustomAction>();

    public void Start() 
    {
        canceled = false;
        onStart?.Invoke(); 
    }
    public void Complete() => onComplete?.Invoke();
    public void CompletedRun() => onCompletedRun?.Invoke();
    public void Update()
    {
        if (canceled) return;

        progress = curve.Evaluate(Remap(unscaledProgress, 0f, duration, min, max));
        progress = Mathf.Clamp(progress, min, max);
        onUpdate?.Invoke();
        onUpdate01?.Invoke(curve.Evaluate(Remap(Mathf.Clamp(unscaledProgress, 0f, duration), 0f, duration, 0, 1)));
        onUpdateFloat?.Invoke(progress);

        if (lerpPos.Count > 0)
        {
            for (int i = 0; i < lerpPos.Count; i++)
            {
                if (lerpPos[i].transform == null)
                {
                    lerpPos.Remove(lerpPos[i]);
                    i--;
                }

                if (!lerpPos[i].localSpace)
                    lerpPos[i].transform.position = Vector3.LerpUnclamped(lerpPos[i].from, lerpPos[i].to, lerpPos[i].curve.Evaluate(progress));
                else
                    lerpPos[i].transform.localPosition = Vector3.LerpUnclamped(lerpPos[i].from, lerpPos[i].to, lerpPos[i].curve.Evaluate(progress));
            }
        }

        if (lerpRot.Count > 0)
        {
            for (int i = 0; i < lerpRot.Count; i++)
            {
                if (lerpRot[i].transform == null)
                {
                    lerpRot.Remove(lerpRot[i]);
                    i--;
                }

                if (!lerpRot[i].localSpace)
                    lerpRot[i].transform.rotation = Quaternion.SlerpUnclamped(lerpRot[i].from, lerpRot[i].to, lerpRot[i].curve.Evaluate(progress));
                else
                    lerpRot[i].transform.localRotation = Quaternion.SlerpUnclamped(lerpRot[i].from, lerpRot[i].to, lerpRot[i].curve.Evaluate(progress));
            }
        }

        if (lerpScale.Count > 0)
        {
            for (int i = 0; i < lerpScale.Count; i++)
            {
                if (lerpScale[i].transform == null)
                {
                    lerpScale.Remove(lerpScale[i]);
                    i--;
                }

                lerpScale[i].transform.localScale = Vector3.LerpUnclamped(lerpScale[i].from, lerpScale[i].to, lerpScale[i].curve.Evaluate(progress));
            }
        }

        if (lerpColor.Count > 0)
        {
            for (int i = 0; i < lerpColor.Count; i++)
            {
                if (lerpColor[i].color == null)
                {
                    lerpColor.Remove(lerpColor[i]);
                    i--;
                }

                lerpColor[i].color?.Invoke(lerpColor[i].gradient.Evaluate(lerpColor[i].curve.Evaluate(progress)));
            }
        }

        if (lerpTarget.Count > 0)
        {
            for (int i = 0; i < lerpTarget.Count; i++)
            {
                if (lerpTarget[i].transform == null || lerpTarget[i].target == null)
                {
                    lerpTarget.Remove(lerpTarget[i]);
                    i--;
                }
                lerpTarget[i].transform.LookAt(lerpTarget[i].target, lerpTarget[i].worldUp);
            }
        }

        if (customActions.Count > 0)
        {
            for (int i = 0; i < customActions.Count; i++)
            {
                if (customActions[i].scale == false && unscaledProgress >= customActions[i].time - Mathf.Epsilon)
                {
                    customActions[i].action?.Invoke();
                    completedCustomActions.Add(customActions[i]);
                    customActions.Remove(customActions[i]);
                    i--;
                }
                else if (customActions[i].scale == true && progress >= customActions[i].time - Mathf.Epsilon)
                {
                    customActions[i].action?.Invoke();
                    completedCustomActions.Add(customActions[i]);
                    customActions.Remove(customActions[i]);
                    i--;
                }
            }
        }
    }
    public void OnCancelEvent()
    {
        if (canceled) return;

        onCancel?.Invoke();

        if (completeTriggeredOnCancel)
            onComplete?.Invoke();

        if (completeLoopTriggeredOnCancel)
            onCompletedRun?.Invoke();
    }
    public void PauseEvent(float duration)
    {
        if (canceled) return;

        pauseTime = duration;
        paused = true;
    }
    public void PauseEvent()
    {
        if (canceled) return;

        paused = true;
        onPause?.Invoke();
    }
    public void ContinueEvent()
    {
        if (canceled) return;

        paused = false;
        onContinue?.Invoke();
    }
    public void UpdateTime() => unscaledProgress += Time.deltaTime;
    public void UpdatePauseTime() => unscaledPauseTime += Time.deltaTime;
    public void UpdateIntervalTime() => unscaledIntervalTime += Time.deltaTime;
    public void AddDelay(float additionalDelay) => durationWithDelay += additionalDelay;
    public void AddDelayUnsafe(float additionalDelay) => duration += additionalDelay;

    float Remap(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}
