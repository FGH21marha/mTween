using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

[Serializable] public partial class Tween
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
    public bool restoreOnCancel = false;

    public float min = 0f;
    public float max = 1f;

    public AnimationCurve curve;

    /// <summary>
    /// Creates a new tween with a duration
    /// </summary>
    public Tween(float duration)
    {
        curve = new AnimationCurve();
        curve.AddKey(0f, 0f);
        curve.AddKey(1f, 1f);
        this.duration = duration;
        durationWithDelay = duration;
        unscaledProgress = 0f;
    }
    public Tween(GameObject id, float duration)
    {
        curve = new AnimationCurve();
        curve.AddKey(0f, 0f);
        curve.AddKey(1f, 1f);
        this.duration = duration;
        durationWithDelay = duration;
        ID = id.GetInstanceID().ToString();
        unscaledProgress = 0f;
    }
    public Tween(string id, float duration)
    {
        curve = new AnimationCurve();
        curve.AddKey(0f, 0f);
        curve.AddKey(1f, 1f);
        this.duration = duration;
        durationWithDelay = duration;
        ID = id;
        unscaledProgress = 0f;
    }
    public Tween(object id, float duration)
    {
        curve = new AnimationCurve();
        curve.AddKey(0f, 0f);
        curve.AddKey(1f, 1f);
        this.duration = duration;
        durationWithDelay = duration;
        ID = id.ToString();
        unscaledProgress = 0f;
    }

    /// <summary>
    /// Returns progress based on input curve
    /// </summary>
    public Tween SetCurve(AnimationCurve newCurve)
    {
        curve = newCurve;
        return this;
    }

    /// <summary>
    /// Repeats the tween
    /// </summary>
    public Tween Repeat()
    {
        repeat = true;
        return this;
    }
    public Tween Repeat(int n)
    {
        repeat = true;
        repeatCount = n;
        return this;
    }
    public Tween Repeat(bool state)
    {
        repeat = state;
        return this;
    }

    /// <summary>
    /// Cancels the tween
    /// </summary>
    public Tween Cancel()
    {
        ResetLerps();
        this.canceled = true;
        return this;
    }

    /// <summary>
    /// Pause tween
    /// </summary>
    public Tween Pause(float duration)
    {
        if (canceled) return this;

        pauseTime = duration;
        paused = true;
        onPause?.Invoke();

        return this;
    }
    public Tween Pause()
    {
        if (canceled) return this;

        paused = true;
        onPause?.Invoke();

        return this;
    }

    /// <summary>
    /// Resumes tween
    /// </summary>
    public Tween Resume()
    {
        if (canceled) return this;

        paused = false;
        onContinue?.Invoke();

        return this;
    } 

    /// <summary>
    /// Executes the tween n times
    /// </summary>
    public Tween ExecuteNTimes(int n)
    {
        repeat = true;
        repeatCount = n - 1;
        return this;
    }

    /// <summary>
    /// Delay between tween repetition
    /// </summary>
    public Tween SetInterval(float duration)
    {
        interval = true;
        if (duration > 0f)
        {
            intervalTime = duration;
        }
        return this;
    }

    /// <summary>
    /// Play the tween in reverse
    /// </summary>
    public Tween PlayReversed()
    {
        float a = min;
        float b = max;
        min = b;
        max = a;
        return this;
    }

    /// <summary>
    /// Triggers an action at the start of a tween
    /// </summary>
    public Tween SetOnStart(Action onStart)
    {
        this.onStart = onStart;
        return this;
    }

    /// <summary>
    /// Triggers an action for every update of the tween. Returns progress from 0 to 1
    /// </summary>
    public Tween SetOnUpdate01(Action<float> onUpdate)
    {
        onUpdate01 = onUpdate;
        return this;
    }

    /// <summary>
    /// Triggers an action for every update of the tween
    /// </summary>
    public Tween SetOnUpdate(Action onUpdate)
    {
        this.onUpdate = onUpdate;
        return this;
    }
    public Tween SetOnUpdate(Action<float> onUpdate)
    {
        onUpdateFloat = onUpdate;
        return this;
    }

    /// <summary>
    /// Triggers an action on tween completion
    /// </summary>
    public Tween SetOnComplete(Action onComplete)
    {
        this.onComplete = onComplete;
        return this;
    }
    public Tween SetOnComplete(Action onComplete, bool triggerOnCancel)
    {
        this.onComplete = onComplete;
        completeTriggeredOnCancel = triggerOnCancel;
        return this;
    }

    /// <summary>
    /// Triggers an action if the tween is canceled
    /// </summary>
    public Tween SetOnCancel(Action onCancel)
    {
        this.onCancel = onCancel;
        return this;
    }

    /// <summary>
    /// Triggers an action if the tween is paused
    /// </summary>
    public Tween SetOnPause(Action onPause)
    {
        this.onPause = onPause;
        return this;
    }

    /// <summary>
    /// Triggers an action if the tween is continued
    /// </summary>
    public Tween SetOnContinue(Action onContinue)
    {
        this.onContinue = onContinue;
        return this;
    }

    /// <summary>
    /// Triggers an action on repeat completion
    /// </summary>
    public Tween SetOnRepeat(Action onFinished)
    {
        this.onCompletedRun = onFinished;
        return this;
    }
    public Tween SetOnRepeat(Action onFinished, bool triggerOnCanceled)
    {
        this.onCompletedRun = onFinished;
        completeLoopTriggeredOnCancel = triggerOnCanceled;
        return this;
    }

    /// <summary>
    /// Restore the state of the tweened object when the tween is canceled
    /// </summary>
    public Tween RestoreOnCancel()
    {
        restoreOnCancel = true;
        return this;
    }
    public Tween RestoreOnCancel(bool restore)
    {
        restoreOnCancel = restore;
        return this;
    }

    /// <summary>
    /// Sets a unique ID for this tween
    /// </summary>
    public Tween SetID()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        System.Random random = new System.Random();

        ID = new string(Enumerable.Repeat(chars, 10).Select(s => s[random.Next(s.Length)]).ToArray());
        return this;
    }
    public Tween SetID(string id)
    {
        ID = id;
        return this;
    }
    public Tween SetID(GameObject id)
    {
        ID = id.GetInstanceID().ToString();
        return this;
    }
    public Tween SetID(object id)
    {
        ID = id.ToString();
        return this;
    }

    /// <summary>
    /// Set progress min max. onUpdate returns progress from the new min max values
    /// </summary>
    public Tween SetMinMax(float min, float max)
    {
        this.min = min;
        this.max = max;
        return this;
    }

    /// <summary>
    /// Calls action after t seconds during the tween
    /// </summary>
    public Tween CallOnTime(float t, Action a)
    {
        if (t >= 0f && a != null)
            customActions.Add(new CustomAction(t, a));

        return this;
    }
    /// <summary>
    /// Calls action after t seconds during the tween, scaling determines if the time used is scaled (0 to 1) or unscaled time (0 to tween duration)
    /// </summary>
    public Tween CallOnTime(float t, Action a, bool scaling)
    {
        if (t >= 0f && a != null)
            customActions.Add(new CustomAction(t, a, scaling));

        return this;
    }

    /// <summary>
    /// Resets list of custom actions, DO NOT USE UNLESS NECESSARY
    /// </summary>
    public Tween ResetCustomActionsList()
    {
        if (completedCustomActions.Count == 0f) return this;

        for (int i = 0; i < completedCustomActions.Count; i++)
            customActions.Add(completedCustomActions[i]);

        completedCustomActions.Clear();

        return this;
    }

    /// <summary>
    /// Reset all tweens lerps to the tween lerp start values
    /// </summary>
    private void ResetLerps()
    {
        if (!restoreOnCancel)
            return;

        foreach (floatLerp i in lerpFloat)
            i.Reset();

        foreach (Vector2Lerp i in lerpVector2)
            i.Reset();

        foreach (Vector3Lerp i in lerpVector3)
            i.Reset();

        foreach (ColorLerp i in lerpColor)
            i.Reset();
    }

    /// <summary>
    /// Interpolate Color
    /// </summary>
    public Tween LerpColor(Action<Color> sr, Color startColor, Color endColor)
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
    public Tween LerpColor(Action<Color> sr, Color startColor, Color endColor, AnimationCurve curve)
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
    public Tween LerpColor(Action<Color> sr, Gradient g)
    {
        ColorLerp newLerp = new ColorLerp(sr, g);
        lerpColor.Add(newLerp);
        return this;
    }
    public Tween LerpColor(Action<Color> sr, Gradient g, AnimationCurve curve)
    {
        ColorLerp newLerp = new ColorLerp(sr, g, curve);
        lerpColor.Add(newLerp);
        return this;
    }
    public Tween LerpColor(Action<Color> sr, List<Color> myColors)
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
    public Tween LerpColor(Action<Color> sr, List<Color> myColors, AnimationCurve curve)
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
    public Tween LerpColor(Action<Color> sr, List<ColorTimes> colortimes)
    {
        Gradient gradient = new Gradient();
        GradientColorKey[] colorKeys = new GradientColorKey[colortimes.Count];
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[colortimes.Count];

        for (int i = 0; i < colorKeys.Length; i++)
        {
            colorKeys[i].color = colortimes[i].color;
            colorKeys[i].time = colortimes[i].time / duration;
        }
        for (int i = 0; i < alphaKeys.Length; i++)
        {
            alphaKeys[i].alpha = colortimes[i].color.a;
            alphaKeys[i].time = colortimes[i].time / duration;
        }
        

        gradient.SetKeys(colorKeys, alphaKeys);

        ColorLerp newLerp = new ColorLerp(sr, gradient);
        lerpColor.Add(newLerp);
        return this;
    }
    public Tween LerpColor(Action<Color> sr, List<ColorTimes> colortimes, AnimationCurve curve)
    {
        Gradient gradient = new Gradient();
        GradientColorKey[] colorKeys = new GradientColorKey[colortimes.Count];
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[colortimes.Count];

        for (int i = 0; i < colorKeys.Length; i++)
        {
            colorKeys[i].color = colortimes[i].color;
            colorKeys[i].time = colortimes[i].time / duration;
        }

        for (int i = 0; i < alphaKeys.Length; i++)
        {
            alphaKeys[i].alpha = colortimes[i].color.a;
            alphaKeys[i].time = colortimes[i].time / duration;
        }

        gradient.SetKeys(colorKeys, alphaKeys);

        ColorLerp newLerp = new ColorLerp(sr, gradient, curve);
        lerpColor.Add(newLerp);
        return this;
    }
    public Tween LerpColor(Action<Color> sr, List<ColorTimes> colortimes, bool scaled)
    {
        Gradient gradient = new Gradient();
        GradientColorKey[] colorKeys = new GradientColorKey[colortimes.Count];
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[colortimes.Count];

        float divider = duration;
        if (scaled)
            divider = 1f;

        for (int i = 0; i < colorKeys.Length; i++)
        {
            colorKeys[i].color = colortimes[i].color;
            colorKeys[i].time = colortimes[i].time / divider;
        }
        for (int i = 0; i < alphaKeys.Length; i++)
        {
            alphaKeys[i].alpha = colortimes[i].color.a;
            alphaKeys[i].time = colortimes[i].time / divider;
        }


        gradient.SetKeys(colorKeys, alphaKeys);

        ColorLerp newLerp = new ColorLerp(sr, gradient);
        lerpColor.Add(newLerp);
        return this;
    }
    public Tween LerpColor(Action<Color> sr, List<ColorTimes> colortimes, bool scaled, AnimationCurve curve)
    {
        Gradient gradient = new Gradient();
        GradientColorKey[] colorKeys = new GradientColorKey[colortimes.Count];
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[colortimes.Count];

        float divider = duration;
        if (scaled)
            divider = 1f;

        for (int i = 0; i < colorKeys.Length; i++)
        {
            colorKeys[i].color = colortimes[i].color;
            colorKeys[i].time = colortimes[i].time / divider;
        }

        for (int i = 0; i < alphaKeys.Length; i++)
        {
            alphaKeys[i].alpha = colortimes[i].color.a;
            alphaKeys[i].time = colortimes[i].time / divider;
        }

        gradient.SetKeys(colorKeys, alphaKeys);

        ColorLerp newLerp = new ColorLerp(sr, gradient, curve);
        lerpColor.Add(newLerp);
        return this;
    }

    /// <summary>
    /// Interpolate float
    /// </summary>
    public Tween LerpFloat(Action<float> a, float from, float to)
    {
        lerpFloat.Add(new floatLerp(a, from, to));
        return this;
    }
    public Tween LerpFloat(Action<float> a, float from, float to, AnimationCurve curve)
    {
        lerpFloat.Add(new floatLerp(a, from, to, curve));
        return this;
    }

    /// <summary>
    /// Interpolate Vector2
    /// </summary>
    public Tween LerpVector2(Action<Vector2> a, Vector2 from, Vector2 to)
    {
        lerpVector2.Add(new Vector2Lerp(a, from, to));
        return this;
    }
    public Tween LerpVector2(Action<Vector2> a, Vector2 from, Vector2 to, AnimationCurve curve)
    {
        lerpVector2.Add(new Vector2Lerp(a, from, to, curve));
        return this;
    }

    /// <summary>
    /// Interpolate Vector3
    /// </summary>
    public Tween LerpVector3(Action<Vector3> a, Vector3 from, Vector3 to)
    {
        lerpVector3.Add(new Vector3Lerp(a, from, to));
        return this;
    }
    public Tween LerpVector3(Action<Vector3> a, Vector3 from, Vector3 to, AnimationCurve curve)
    {
        lerpVector3.Add(new Vector3Lerp(a, from, to, curve));
        return this;
    }

    /// <summary>
    /// Interpolate Quaternion
    /// </summary>
    public Tween LerpQuaternion(Action<Quaternion> t, Quaternion from, Quaternion to)
    {
        lerpRot.Add(new RotationLerp(t, from, to));
        return this;
    }
    public Tween LerpQuaternion(Action<Quaternion> t, Quaternion from, Quaternion to, AnimationCurve curve)
    {
        lerpRot.Add(new RotationLerp(t, from, to, curve));
        return this;
    }

    protected struct floatLerp
    {
        public Action<float> a;
        public float start;
        public float end;
        public AnimationCurve curve;

        public floatLerp(Action<float> a, float start, float end)
        {
            this.a = a;
            this.start = start;
            this.end = end;
            this.curve = new AnimationCurve();
            curve.AddKey(0f, 0f);
            curve.AddKey(1f, 1f);
        }
        public floatLerp(Action<float> a, float start, float end, AnimationCurve curve)
        {
            this.a = a;
            this.start = start;
            this.end = end;
            this.curve = curve;
        }

        public void Reset() => a?.Invoke(start);
    }
    protected List<floatLerp> lerpFloat = new List<floatLerp>();

    protected struct Vector2Lerp
    {
        public Action<Vector2> a;
        public Vector2 start;
        public Vector2 end;
        public AnimationCurve curve;

        public Vector2Lerp(Action<Vector2> a, Vector2 start, Vector2 end)
        {
            this.a = a;
            this.start = start;
            this.end = end;
            this.curve = new AnimationCurve();
            curve.AddKey(0f, 0f);
            curve.AddKey(1f, 1f);
        }
        public Vector2Lerp(Action<Vector2> a, Vector2 start, Vector2 end, AnimationCurve curve)
        {
            this.a = a;
            this.start = start;
            this.end = end;
            this.curve = curve;
        }

        public void Reset() => a?.Invoke(start);
    }
    protected List<Vector2Lerp> lerpVector2 = new List<Vector2Lerp>();

    protected struct Vector3Lerp
    {
        public Action<Vector3> a;
        public Vector3 start;
        public Vector3 end;
        public AnimationCurve curve;

        public Vector3Lerp(Action<Vector3> a, Vector3 start, Vector3 end)
        {
            this.a = a;
            this.start = start;
            this.end = end;
            this.curve = new AnimationCurve();
            curve.AddKey(0f, 0f);
            curve.AddKey(1f, 1f);
        }
        public Vector3Lerp(Action<Vector3> a, Vector3 start, Vector3 end, AnimationCurve curve)
        {
            this.a = a;
            this.start = start;
            this.end = end;
            this.curve = curve;
        }

        public void Reset() => a?.Invoke(start);
    }
    protected List<Vector3Lerp> lerpVector3 = new List<Vector3Lerp>();

    protected struct RotationLerp
    {
        public Action<Quaternion> a;
        public Quaternion from;
        public Quaternion to;
        public AnimationCurve curve;

        public RotationLerp(Action<Quaternion> a, Quaternion from, Quaternion to, AnimationCurve curve)
        {
            this.a = a;
            this.from = from;
            this.to = to;
            this.curve = curve;
        }
        public RotationLerp(Action<Quaternion> a, Quaternion from, Quaternion to)
        {
            this.a = a;
            this.from = from;
            this.to = to;
            curve = new AnimationCurve();
            curve.AddKey(0f, 0f);
            curve.AddKey(1f, 1f);
        }

        public void Reset() => a?.Invoke(from);
    }
    protected List<RotationLerp> lerpRot = new List<RotationLerp>();

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

        public void Reset() => color?.Invoke(gradient.colorKeys[0].color);
    }
    protected List<ColorLerp> lerpColor = new List<ColorLerp>();
    protected Gradient GetGradient(Color from, Color to)
    {
        Gradient gradient;
        GradientColorKey[] colorKeys;
        GradientAlphaKey[] alphaKeys;

        List<Color> colors = new List<Color>();
        colors.Add(from);
        colors.Add(to);

        gradient = new Gradient();
        colorKeys = new GradientColorKey[colors.Count];
        alphaKeys = new GradientAlphaKey[colors.Count];
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
        return gradient;
    }
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

    /// <summary>
    /// Starts the tween. DO NOT USE
    /// </summary>
    public void Start() 
    {
        canceled = false;
        onStart?.Invoke(); 
    }

    /// <summary>
    /// Called on full tween completion. DO NOT USE
    /// </summary>
    public void Complete()
    {
        ResetLerps();
        onComplete?.Invoke();
    }

    /// <summary>
    /// Called on completed tween loop. DO NOT USE
    /// </summary>
    public void CompletedRun()
    {
        onCompletedRun?.Invoke();
    }
    public void Update()
    {
        if (canceled) return;

        unscaledProgress += Time.deltaTime;

        progress = curve.Evaluate(Remap(unscaledProgress, 0f, duration, min, max));
        progress = Mathf.Clamp(progress, min, max);
        onUpdate?.Invoke();
        onUpdate01?.Invoke(curve.Evaluate(Remap(Mathf.Clamp(unscaledProgress, 0f, duration), 0f, duration, 0, 1)));
        onUpdateFloat?.Invoke(progress);

        if (lerpFloat.Count > 0)
        {
            for (int i = 0; i < lerpFloat.Count; i++)
            {
                if (lerpFloat[i].a == null)
                {
                    lerpFloat.Remove(lerpFloat[i]);
                    i--;
                }

                lerpFloat[i].a?.Invoke(Mathf.LerpUnclamped(lerpFloat[i].start, lerpFloat[i].end, lerpFloat[i].curve.Evaluate(progress)));
            }
        }

        if (lerpVector2.Count > 0)
        {
            for (int i = 0; i < lerpVector2.Count; i++)
            {
                if (lerpVector2[i].a == null)
                {
                    lerpVector2.Remove(lerpVector2[i]);
                    i--;
                }

                lerpVector2[i].a?.Invoke(Vector2.LerpUnclamped(lerpVector2[i].start, lerpVector2[i].end, lerpVector2[i].curve.Evaluate(progress)));
            }
        }

        if (lerpVector3.Count > 0)
        {
            for (int i = 0; i < lerpVector3.Count; i++)
            {
                if (lerpVector3[i].a == null)
                {
                    lerpVector3.Remove(lerpVector3[i]);
                    i--;
                }

                lerpVector3[i].a?.Invoke(Vector3.LerpUnclamped(lerpVector3[i].start, lerpVector3[i].end, lerpVector3[i].curve.Evaluate(progress)));
            }
        }

        if (lerpRot.Count > 0)
        {
            for (int i = 0; i < lerpRot.Count; i++)
            {
                if (lerpRot[i].a == null)
                {
                    lerpRot.Remove(lerpRot[i]);
                    i--;
                }
                
                lerpRot[i].a?.Invoke(Quaternion.SlerpUnclamped(lerpRot[i].from, lerpRot[i].to, lerpRot[i].curve.Evaluate(progress)));
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

    /// <summary>
    /// Called on tween cancellation. DO NOT USE
    /// </summary>
    public void OnCancel()
    {
        if (canceled) return;

        onCancel?.Invoke();

        if (completeTriggeredOnCancel)
            onComplete?.Invoke();

        if (completeLoopTriggeredOnCancel)
            onCompletedRun?.Invoke();
    }
    public void UpdatePauseTime() => unscaledPauseTime += Time.deltaTime;
    public void UpdateIntervalTime() => unscaledIntervalTime += Time.deltaTime;

    /// <summary>
    /// Add time to the tween duration
    /// </summary>
    public void AddDuration(float additionalDelay) => durationWithDelay += additionalDelay;

    /// <summary>
    /// Add time to the tween duration, disposes the old duration value
    /// </summary>
    public void AddDurationUnsafe(float additionalDelay) => duration += additionalDelay;

    /// <summary>
    /// Remap the start and end values of tween time
    /// </summary>
    protected float Remap(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}

/// <summary>
/// Used to add new keyframes to ColorLerp gradient
/// </summary>
[Serializable] public struct ColorTimes
{
    public Color color;
    public float time;

    public ColorTimes(Color c, float t)
    {
        this.color = c;
        this.time = t;
    }
}
