#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("mTween/mTween/mTweener")]
public class mTweener : MonoBehaviour
{
    public mTweenerInstance[] tweens;
    public bool CanTrigger
    {
        get
        {
            return tweens != null || tweens.Length > 0;
        }
    }
    public bool isEditing;

    public void StartTween()
    {
        foreach (var tween in tweens)
            tween.StartTween();
    }
    public void CancelTween()
    {
        foreach (var tween in tweens)
            tween.CancelTween();
    }
    public void PauseTween()
    {
        foreach (var tween in tweens)
            tween.PauseTween();
    }
    public void ResumeTween()
    {
        foreach (var tween in tweens)
            tween.ResumeTween();
    }
}

public enum TweenerType { Float, Vector2, Vector3, Rotation, Color }

[System.Serializable] public class mTweenerInstance
{
    public string ID;

    public bool isVisible;

    public TweenerType tweenType;
    public float animationTime = 1f;

    public bool repeat;
    public int repeatCount = 1;

    public bool interuptable;
    public bool restoreOnComplete;
    private bool isAnimating;

    public float FromFloat, ToFloat;
    public Vector2 FromVector2, ToVector2;
    public Vector3 FromVector3, ToVector3;
    public Vector3 FromRotation, ToRotation;
    public Color FromColor, ToColor;

    public AnimationCurve curve = AnimationCurve.Linear(0, 0, 1, 1);

    public UnityEvent<float> onUpdateFloat;
    public UnityEvent<Vector2> onUpdateVector2;
    public UnityEvent<Vector3> onUpdateVector3;
    public UnityEvent<Quaternion> onUpdateRotation;
    public UnityEvent<Color> onUpdateColor;

    public UnityEvent onTweenStarted;
    public UnityEvent onTweenCompleted;
    public UnityEvent onTweenCanceled;
    public UnityEvent onTweenPaused;
    public UnityEvent onTweenResume;

    private Tween tween;

    public int showTweenSettings;
    public int currentTweenEventTab;

    public mTweenerInstance()
    {
        Initialize();
    }

    public void Initialize()
    {
        isVisible = true;
        animationTime = 1f;
        curve = AnimationCurve.Linear(0, 0, 1, 1);
    }

    public void PauseTween()
    {
        tween.Pause();
        onTweenPaused?.Invoke();
    }
    public void ResumeTween()
    {
        tween.Resume();
        onTweenResume?.Invoke();
    }
    public void CancelTween()
    {
        tween.Cancel();
        onTweenCanceled?.Invoke();
    }
    private void OnComplete()
    {
        isAnimating = false;
        onTweenCompleted?.Invoke();
    }

    public void StartTween()
    {
        if (isAnimating && !interuptable) return;

        isAnimating = true;
        onTweenStarted?.Invoke();

        string id = string.IsNullOrEmpty(ID) ? GetHashCode().ToString() : ID;

        mTween.CancelTween(id);
        switch (tweenType)
        {
            case TweenerType.Float: tween = mTween.Value(id, FromFloat, ToFloat, animationTime, curve, value => onUpdateFloat?.Invoke(value)); break;
            case TweenerType.Vector2: tween = mTween.Value(id, FromVector2, ToVector2, animationTime, curve, value => onUpdateVector2?.Invoke(value)); break;
            case TweenerType.Vector3: tween = mTween.value(id, FromVector3, ToVector3, animationTime, curve, value => onUpdateVector3?.Invoke(value)); break;
            case TweenerType.Rotation: tween = mTween.value(id, FromRotation, ToRotation, animationTime, curve, value => onUpdateRotation?.Invoke(Quaternion.Euler(value))); break;
            case TweenerType.Color: tween = mTween.Value(id, FromColor, ToColor, animationTime, curve, value => onUpdateColor?.Invoke(value)); break;
        }

        tween.Repeat(repeat ? repeatCount : 0).RestoreOnCancel(restoreOnComplete).SetOnComplete(OnComplete).SetOnCancel(() => isAnimating = false);
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(mTweener))] public class mTweenerEditor : Editor
{
    SerializedProperty tweens;
    SerializedProperty editing;
    private void OnEnable()
    {
        tweens = serializedObject.FindProperty("tweens");
        editing = serializedObject.FindProperty("isEditing");
    }

    public override void OnInspectorGUI()
    {
        Title("mTweener");

        using (new EditorGUILayout.HorizontalScope(EditorStyles.helpBox))
        {
            EditorGUIUtility.labelWidth = 52;
            EditorGUILayout.PropertyField(tweens.FindPropertyRelative("Array.size"), new GUIContent("Tweens"));
            EditorGUIUtility.labelWidth = 70;

            if (GUILayout.Button("+", GUILayout.Width(20)))
            {
                if ((target as mTweener).tweens == null)
                {
                    (target as mTweener).tweens = new mTweenerInstance[1];
                    serializedObject.ApplyModifiedProperties();
                }
                else
                {
                    tweens.arraySize++;
                    serializedObject.ApplyModifiedProperties();
                }

                return;
            }

            if (GUILayout.Button("-", GUILayout.Width(20)))
            {
                if (tweens.arraySize - 1 < 0)
                    tweens.arraySize = 0;
                else
                    tweens.arraySize -= 1;

                serializedObject.ApplyModifiedProperties();

                return;
            }

            if (GUILayout.Button("Edit", GUILayout.Width(36))) editing.boolValue = !editing.boolValue;
        }

        if ((target as mTweener).tweens == null || (target as mTweener).tweens.Length <= 0) return;

        for (int i = 0; i < (target as mTweener).tweens.Length; i++)
        {
            EditorGUILayout.Space(2);

            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
            {
                RenderInterface(i, out bool shouldBreak);

                if (shouldBreak == true)
                    break;
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void RenderInterface(int current, out bool shouldBreak)
    {
        shouldBreak = false;
        var targ = (target as mTweener).tweens[current];
        var labelWidth = 72;

        using (new GUILayout.HorizontalScope())
        {
            var title = string.IsNullOrEmpty(targ.ID) ? "Tween " + current.ToString() : targ.ID;

            if (GUILayout.Button(title, GetLabelStyle(), GUILayout.Height(18)))
            {
                targ.isVisible = !targ.isVisible;
            }
            if (editing.boolValue)
            {
                if (GUILayout.Button("-", GUILayout.Height(18), GUILayout.Width(18)))
                {
                    tweens.DeleteArrayElementAtIndex(current);
                    shouldBreak = true;
                    return;
                }
            }
        }

        EditorGUILayout.Space(6);

        if (!targ.isVisible) return;

        EditorGUILayout.Space(2);

        //Tween Info
        using (new GUILayout.VerticalScope(EditorStyles.helpBox))
        {
            //Tween Type
            using (new GUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField(new GUIContent("Type", "The value type of this tween being performed"), GUILayout.Width(labelWidth));
                targ.tweenType = (TweenerType)EditorGUILayout.EnumPopup(targ.tweenType);
            }

            //Tween ID
            using (new GUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField(new GUIContent("ID", "An identifier used to access the tween via code"), GUILayout.Width(labelWidth));
                targ.ID = EditorGUILayout.TextField(targ.ID);
            }
        }

        EditorGUILayout.Space();
        GUILayout.Label("Tween Options");

        //Tween Settings
        using (new GUILayout.VerticalScope(EditorStyles.helpBox))
        {
            targ.showTweenSettings = GUILayout.Toolbar(targ.showTweenSettings, new string[] { "Animation", "Playback" });

            if (targ.showTweenSettings == 1)
            {
                //Tween repeat
                using (new GUILayout.HorizontalScope())
                {
                    EditorGUILayout.LabelField(new GUIContent("Repeat", "Repeats the tween for a set number of cycles"), GUILayout.Width(labelWidth));
                    targ.repeat = EditorGUILayout.Toggle(targ.repeat, GUILayout.Width(20));

                    using (new EditorGUI.DisabledGroupScope(!targ.repeat))
                    {
                        EditorGUILayout.LabelField("Count", GUILayout.Width(labelWidth - 26));
                        targ.repeatCount = EditorGUILayout.IntField(targ.repeatCount);
                        targ.repeatCount = Mathf.Clamp(targ.repeatCount, 1, 999);
                    }
                }

                //Tween restore
                using (new GUILayout.HorizontalScope())
                {
                    EditorGUILayout.LabelField(new GUIContent("Restore", "Restores the state of the tween when completed"), GUILayout.Width(labelWidth));
                    targ.restoreOnComplete = EditorGUILayout.Toggle(targ.restoreOnComplete, GUILayout.Width(20));
                }

                //Tween interuptable
                using (new GUILayout.HorizontalScope())
                {
                    EditorGUILayout.LabelField(new GUIContent("Interuptable", "Interupts current tween if a new one is triggered"), GUILayout.Width(labelWidth));
                    targ.interuptable = EditorGUILayout.Toggle(targ.interuptable, GUILayout.Width(20));
                }
            }
            else
            {
                //Tween time
                using (new GUILayout.HorizontalScope())
                {
                    EditorGUILayout.LabelField(new GUIContent("Duration", "The amount of time in seconds for the tween to complete"), GUILayout.Width(labelWidth));
                    targ.animationTime = EditorGUILayout.FloatField(targ.animationTime);
                }

                //Tween Curve
                using (new GUILayout.HorizontalScope())
                {
                    EditorGUILayout.LabelField(new GUIContent("Curve", "Alters the rate of progress along a tween. Can be used for easing effects such as ease in/out"), GUILayout.Width(labelWidth));
                    targ.curve = EditorGUILayout.CurveField(targ.curve);
                }

                //Draw a border around selected content
                using (new GUILayout.VerticalScope())
                {
                    switch (targ.tweenType)
                    {
                        case TweenerType.Float: RenderFloat(targ, labelWidth); break;
                        case TweenerType.Vector2: RenderVector2(targ, labelWidth); break;
                        case TweenerType.Vector3: RenderVector3(targ, labelWidth); break;
                        case TweenerType.Rotation: RenderRotation(targ, labelWidth); break;
                        case TweenerType.Color: RenderColor(targ, labelWidth); break;
                    }
                }
            }
        }

        EditorGUILayout.Space();
        GUILayout.Label("Tween Event");

        //Tween Events
        using (new GUILayout.VerticalScope(EditorStyles.helpBox))
        {
            switch (targ.tweenType)
            {
                case TweenerType.Float: RenderTweenEvent(targ, current, "onUpdateFloat"); break;
                case TweenerType.Vector2: RenderTweenEvent(targ, current, "onUpdateVector2"); break;
                case TweenerType.Vector3: RenderTweenEvent(targ, current, "onUpdateVector3"); break;
                case TweenerType.Rotation: RenderTweenEvent(targ, current, "onUpdateRotation"); break;
                case TweenerType.Color: RenderTweenEvent(targ, current, "onUpdateColor"); break;
            }
        }
    }

    private void RenderFloat(mTweenerInstance targ, float labelWidth)
    {
        using (new GUILayout.HorizontalScope())
        {
            EditorGUILayout.LabelField(new GUIContent("From", "Starting value of the tween"), GUILayout.Width(labelWidth));
            targ.FromFloat = EditorGUILayout.FloatField(targ.FromFloat);
        }

        using (new GUILayout.HorizontalScope())
        {
            EditorGUILayout.LabelField(new GUIContent("To", "Value reached at the end of a tween"), GUILayout.Width(labelWidth));
            targ.ToFloat = EditorGUILayout.FloatField(targ.ToFloat);
        }
    }
    private void RenderVector2(mTweenerInstance targ, float labelWidth)
    {
        using (new GUILayout.HorizontalScope())
        {
            EditorGUILayout.LabelField(new GUIContent("From", "Starting value of the tween"), GUILayout.Width(labelWidth));
            targ.FromVector2 = EditorGUILayout.Vector2Field("", targ.FromVector2);
        }

        using (new GUILayout.HorizontalScope())
        {
            EditorGUILayout.LabelField(new GUIContent("To", "Value reached at the end of a tween"), GUILayout.Width(labelWidth));
            targ.ToVector2 = EditorGUILayout.Vector2Field("", targ.ToVector2);
        }
    }
    private void RenderVector3(mTweenerInstance targ, float labelWidth)
    {
        using (new GUILayout.HorizontalScope())
        {
            EditorGUILayout.LabelField(new GUIContent("From", "Starting value of the tween"), GUILayout.Width(labelWidth));
            targ.FromVector3 = EditorGUILayout.Vector3Field("", targ.FromVector3);
        }

        using (new GUILayout.HorizontalScope())
        {
            EditorGUILayout.LabelField(new GUIContent("To", "Value reached at the end of a tween"), GUILayout.Width(labelWidth));
            targ.ToVector3 = EditorGUILayout.Vector3Field("", targ.ToVector3);
        }
    }
    private void RenderRotation(mTweenerInstance targ, float labelWidth)
    {
        using (new GUILayout.HorizontalScope())
        {
            EditorGUILayout.LabelField(new GUIContent("From", "Starting value of the tween"), GUILayout.Width(labelWidth));
            targ.FromRotation = EditorGUILayout.Vector3Field("", targ.FromRotation);
        }

        using (new GUILayout.HorizontalScope())
        {
            EditorGUILayout.LabelField(new GUIContent("To", "Value reached at the end of a tween"), GUILayout.Width(labelWidth));
            targ.ToRotation = EditorGUILayout.Vector3Field("", targ.ToRotation);
        }
    }
    private void RenderColor(mTweenerInstance targ, float labelWidth)
    {
        using (new GUILayout.HorizontalScope())
        {
            EditorGUILayout.LabelField(new GUIContent("From", "Starting value of the tween"), GUILayout.Width(labelWidth));
            targ.FromColor = EditorGUILayout.ColorField(new GUIContent(""), targ.FromColor, true, true, true);
        }

        using (new GUILayout.HorizontalScope())
        {
            EditorGUILayout.LabelField(new GUIContent("To", "Value reached at the end of a tween"), GUILayout.Width(labelWidth));
            targ.ToColor = EditorGUILayout.ColorField(new GUIContent(""), targ.ToColor, true, true, true);
        }
    }

    private void RenderTweenEvent(mTweenerInstance targ, int arrayIndex, string property)
    {
        targ.currentTweenEventTab = EditorGUILayout.Popup(targ.currentTweenEventTab, new string[] 
        { 
            "On Tween Updated", 
            "On Tween Started", 
            "On Tween Completed", 
            "On Tween Canceled", 
            "On Tween Paused", 
            "On Tween Resume"
        });

        if (targ.currentTweenEventTab == 0)
            EditorGUILayout.PropertyField(serializedObject.FindProperty("tweens").GetArrayElementAtIndex(arrayIndex).FindPropertyRelative(property));

        if (targ.currentTweenEventTab == 1)
            EditorGUILayout.PropertyField(serializedObject.FindProperty("tweens").GetArrayElementAtIndex(arrayIndex).FindPropertyRelative("onTweenStarted"));

        if (targ.currentTweenEventTab == 2)
            EditorGUILayout.PropertyField(serializedObject.FindProperty("tweens").GetArrayElementAtIndex(arrayIndex).FindPropertyRelative("onTweenCompleted"));

        if (targ.currentTweenEventTab == 3)
            EditorGUILayout.PropertyField(serializedObject.FindProperty("tweens").GetArrayElementAtIndex(arrayIndex).FindPropertyRelative("onTweenCanceled"));

        if (targ.currentTweenEventTab == 4)
            EditorGUILayout.PropertyField(serializedObject.FindProperty("tweens").GetArrayElementAtIndex(arrayIndex).FindPropertyRelative("onTweenPaused"));

        if (targ.currentTweenEventTab == 5)
            EditorGUILayout.PropertyField(serializedObject.FindProperty("tweens").GetArrayElementAtIndex(arrayIndex).FindPropertyRelative("onTweenResume"));
    }

    void Title(string title)
    {
        EditorGUILayout.Space(5);

        GUIStyle i = new GUIStyle();
        i.fontSize = 14;
        i.alignment = TextAnchor.MiddleCenter;
        i.normal.textColor = new Color(0.8f, 0.8f, 0.8f, 1);
        i.fontStyle = FontStyle.Bold;
        EditorGUI.LabelField(GUILayoutUtility.GetRect(200, 24), title, i);
        EditorGUI.DrawRect(GUILayoutUtility.GetRect(1, 1), new Color(0, 0, 0, 0.5f));
        EditorGUILayout.Space();
    }

    void TitleSmall(string title)
    {
        GUIStyle i = new GUIStyle();
        i.fontSize = 14;
        i.alignment = TextAnchor.MiddleLeft;
        i.normal.textColor = new Color(0.8f, 0.8f, 0.8f, 1);
        i.fontStyle = FontStyle.Bold;
        i.contentOffset = Vector2.right * 4;
        EditorGUI.LabelField(GUILayoutUtility.GetRect(200, 24), title, i);
        EditorGUILayout.Space();
    }

    private GUIStyle GetLabelStyle()
    {
        GUIStyle style = new GUIStyle();
        style.alignment = TextAnchor.LowerLeft;
        style.normal.textColor = new Color(0.8f, 0.8f, 0.8f, 1);
        style.fontStyle = FontStyle.Bold;
        style.fontSize = 14;
        style.contentOffset = new Vector2(2, 3);
        return style;
    }
}

#endif
