using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mTween : MonoBehaviour
{
    #region Monobehaviour

    public static mTween instance;
    private void Awake()
    {
        if(instance == null)
            instance = this;
    }
    private void OnValidate()
    {
        if (instance == null)
        {
            if (GameObject.Find("mTween"))
                instance = GameObject.Find("mTween").GetComponent<mTween>();
            else
                CreateNewInstance();
        }
    }
    void LateUpdate() => StartCoroutine(LateLateUpdate());
    private IEnumerator LateLateUpdate()
    {
        yield return new WaitForEndOfFrame();
        UpdateQueuedTweens();
    }
    private static void CreateNewInstance()
    {
        if (instance == null)
        {
            GameObject mTween = new GameObject("mTween");
            mTween x = mTween.AddComponent<mTween>();
            instance = x;
        }
    }

    #endregion

    #region New Tween Instance

    private static List<Tween> activeTweens = new List<Tween>();

    /// <summary>
    /// Queue new tween to be called after a period of time, returns call onComplete and onUpdate
    /// </summary>
    public static Tween NewTween(float time)
    {
        Tween i = new Tween(time).SetID();
        activeTweens.Add(i);

        CreateNewInstance();

        return i;
    }
    public static Tween NewTween(GameObject id, float time)
    {
        Tween i = new Tween(id, time);
        activeTweens.Add(i);

        CreateNewInstance();

        return i;
    }
    public static Tween NewTween(string id, float time)
    {
        Tween i = new Tween(id, time);
        activeTweens.Add(i);

        CreateNewInstance();

        return i;
    }
    public static Tween NewTween(object id, float time)
    {
        Tween i = new Tween(id, time);
        activeTweens.Add(i);

        CreateNewInstance();

        return i;
    }

    /// <summary>
    /// Triggers an action after x amount of time
    /// </summary>
    public static Tween DelayedCall(Action OnComplete, float time)
    {
        Tween i = new Tween(time).SetOnComplete(OnComplete);
        activeTweens.Add(i);

        CreateNewInstance();

        return i;
    }

    /// <summary>
    /// Triggers an action after x amount of time
    /// </summary>
    public static Tween DelayedCall(GameObject id, Action OnComplete, float time)
    {
        Tween i = new Tween(id, time).SetOnComplete(OnComplete);
        activeTweens.Add(i);

        CreateNewInstance();

        return i;
    }

    #endregion

    #region Class Methods

    /// <summary>
    /// Cancels all tweens
    /// </summary>
    public static void CancelAllTweens()
    {
        if (activeTweens.Count == 0) return;

        for (int i = 0; i < activeTweens.Count; i++)
            activeTweens[i].Cancel();
    }

    /// <summary>
    /// Cancel tween by ID
    /// </summary>
    public static void CancelTween(string id)
    {
        for (int i = 0; i < activeTweens.Count; i++)
            if (activeTweens[i].GetID() == id)
                activeTweens[i].Cancel();
    }
    public static void CancelTween(GameObject id)
    {
        for (int i = 0; i < activeTweens.Count; i++)
            if (activeTweens[i].GetID() == id.GetInstanceID().ToString())
                activeTweens[i].Cancel();
    }
    public static void CancelTween(object id)
    {
        for (int i = 0; i < activeTweens.Count; i++)
            if (activeTweens[i].GetID() == id.ToString())
                activeTweens[i].Cancel();
    }
    public static void CancelTween(GameObject id, float t)
    {
        for (int i = 0; i < activeTweens.Count; i++)
            if (activeTweens[i].GetID() == id.GetInstanceID().ToString())
                activeTweens.Add(new Tween(t).SetOnComplete(() => activeTweens[i].Cancel()));
    }
    public static void CancelTween(string id, float t)
    {
        for (int i = 0; i < activeTweens.Count; i++)
            if (activeTweens[i].GetID() == id)
            {
                activeTweens[i].Cancel();
                activeTweens.Add(new Tween(t).SetOnComplete(() => activeTweens[i].Cancel()));
            }
    }
    public static void CancelTween(object id, float t)
    {
        for (int i = 0; i < activeTweens.Count; i++)
            if (activeTweens[i].GetID() == id.ToString())
            {
                activeTweens[i].Cancel();
                activeTweens.Add(new Tween(t).SetOnComplete(() => activeTweens[i].Cancel()));
            }
    }

    /// <summary>
    /// Pause tween by ID
    /// </summary>
    public static void PauseTween(string id)
    {
        for (int i = 0; i < activeTweens.Count; i++)
            if (activeTweens[i].GetID() == id && !activeTweens[i].paused)
                activeTweens[i].Pause();
    }
    public static void PauseTween(GameObject id)
    {
        for (int i = 0; i < activeTweens.Count; i++)
            if (activeTweens[i].GetID() == id.GetInstanceID().ToString() && !activeTweens[i].paused)
                activeTweens[i].Pause();
    }
    public static void PauseTween(object id)
    {
        for (int i = 0; i < activeTweens.Count; i++)
            if (activeTweens[i].GetID() == id.ToString() && !activeTweens[i].paused)
                activeTweens[i].Pause();
    }
    public static void PauseTween(string id, float duration)
    {
        for (int i = 0; i < activeTweens.Count; i++)
            if (activeTweens[i].GetID() == id && !activeTweens[i].paused)
                activeTweens[i].Pause(duration);
    }
    public static void PauseTween(GameObject id, float duration)
    {
        for (int i = 0; i < activeTweens.Count; i++)
            if (activeTweens[i].GetID() == id.GetInstanceID().ToString() && !activeTweens[i].paused)
                activeTweens[i].Pause(duration);
    }
    public static void PauseTween(object id, float duration)
    {
        for (int i = 0; i < activeTweens.Count; i++)
            if (activeTweens[i].GetID() == id.ToString() && !activeTweens[i].paused)
                activeTweens[i].Pause(duration);
    }

    /// <summary>
    /// Continue tween by ID
    /// </summary>
    public static void ContinueTween(string id)
    {
        for (int i = 0; i < activeTweens.Count; i++)
            if (activeTweens[i].GetID() == id && activeTweens[i].paused)
                activeTweens[i].Resume();
    }
    public static void ContinueTween(GameObject id)
    {
        for (int i = 0; i < activeTweens.Count; i++)
            if (activeTweens[i].GetID() == id.GetInstanceID().ToString() && activeTweens[i].paused)
                activeTweens[i].Resume();
    }
    public static void ContinueTween(object id)
    {
        for (int i = 0; i < activeTweens.Count; i++)
            if (activeTweens[i].GetID() == id.ToString() && activeTweens[i].paused)
                activeTweens[i].Resume();
    }

    /// <summary>
    /// Pause all tweens
    /// </summary>
    public static void PauseAllTweens()
    {
        for (int i = 0; i < activeTweens.Count; i++)
            activeTweens[i].Pause();
    }

    /// <summary>
    /// Continue all tweens
    /// </summary>
    public static void ContinueAllTweens()
    {
        for (int i = 0; i < activeTweens.Count; i++)
            activeTweens[i].Resume();
    }

    /// <summary>
    /// Adds more time to a tween
    /// </summary>
    public static void DelayTween(string id, float delay)
    {
        for (int i = 0; i < activeTweens.Count; i++)
            if (activeTweens[i].GetID() == id)
                activeTweens[i].AddDuration(delay);
    }
    public static void DelayTween(GameObject id, float delay)
    {
        for (int i = 0; i < activeTweens.Count; i++)
            if (activeTweens[i].GetID() == id.GetInstanceID().ToString())
                activeTweens[i].AddDuration(delay);
    }
    public static void DelayTween(object id, float delay)
    {
        for (int i = 0; i < activeTweens.Count; i++)
            if (activeTweens[i].GetID() == id.ToString())
                activeTweens[i].AddDuration(delay);
    }

    /// <summary>
    /// Adds more time to a tween. Adds to the total progress time which will continually delay the tween if used in repeat
    /// </summary>
    public static void DelayTweenUnsafe(string id, float delay)
    {
        for (int i = 0; i < activeTweens.Count; i++)
            if (activeTweens[i].GetID() == id)
                activeTweens[i].AddDurationUnsafe(delay);
    }
    public static void DelayTweenUnsafe(GameObject id, float delay)
    {
        for (int i = 0; i < activeTweens.Count; i++)
            if (activeTweens[i].GetID() == id.GetInstanceID().ToString())
                activeTweens[i].AddDurationUnsafe(delay);
    }
    public static void DelayTweenUnsafe(object id, float delay)
    {
        for (int i = 0; i < activeTweens.Count; i++)
            if (activeTweens[i].GetID() == id.ToString())
                activeTweens[i].AddDurationUnsafe(delay);
    }

    /// <summary>
    /// Get the number of active tweens
    /// </summary>
    public static int GetActiveTweensCount() => activeTweens.Count;

    private void UpdateQueuedTweens()
    {
        if (activeTweens.Count == 0) return;

        for (int i = 0; i < activeTweens.Count; i++)
        {
            //Check if tween was canceled, remove current timeline if true
            if (activeTweens[i].canceled)
            {
                activeTweens.Remove(activeTweens[i]);
                i--;
                continue;
            }

            //Check if tween was paused, resume after unscaledPauseTime if one was provided
            if (activeTweens[i].paused)
            {
                activeTweens[i].UpdatePauseTime();

                if (activeTweens[i].unscaledPauseTime >= activeTweens[i].pauseTime && activeTweens[i].pauseTime != 0f)
                {
                    activeTweens[i].unscaledPauseTime = 0f;
                    activeTweens[i].Resume();
                }
                continue;
            }

            //Check if tween has an interval, delay the call between Complete and Start if true
            if (activeTweens[i].onInterval)
            {
                activeTweens[i].UpdateIntervalTime();

                if (activeTweens[i].unscaledIntervalTime >= activeTweens[i].intervalTime && activeTweens[i].intervalTime != 0f)
                {
                    activeTweens[i].unscaledIntervalTime = 0f;
                    activeTweens[i].onInterval = false;
                    activeTweens[i].unscaledProgress = 0f;
                }
                continue;
            }

            //Check if unscaledProgress is 0, call Start if true
            if (activeTweens[i].unscaledProgress == 0f)
                activeTweens[i].Start();

            //Check if unscaledProgress is same as durationWithDelay, try call Complete if true
            if (activeTweens[i].unscaledProgress >= (activeTweens[i].durationWithDelay - Mathf.Epsilon))
            {
                activeTweens[i].unscaledProgress = activeTweens[i].durationWithDelay;

                //Check if tween is set to repeat
                if (activeTweens[i].repeat)
                {
                    activeTweens[i].durationWithDelay = activeTweens[i].duration;
                    activeTweens[i].ResetCustomActionsList();
                    activeTweens[i].CompletedRun();

                    //Check if activeRepeatCount is same as repeatCount. Call Complete if true, else increment activeRepeatCount
                    if (activeTweens[i].activeRepeatCount == activeTweens[i].repeatCount && activeTweens[i].repeatCount != 0)
                    {
                        activeTweens[i].repeat = false;
                        activeTweens[i].activeRepeatCount = 0;
                        activeTweens[i].Complete();
                        activeTweens.Remove(activeTweens[i]);
                        continue;
                    }
                    else if (activeTweens[i].activeRepeatCount < activeTweens[i].repeatCount && activeTweens[i].repeatCount != 0)
                        activeTweens[i].activeRepeatCount++;

                    //Set interval
                    if (activeTweens[i].interval)
                        activeTweens[i].onInterval = true;
                    else
                        activeTweens[i].unscaledProgress = 0f;
                        
                    continue;
                }

                //Call Complete and remove self from active tweens
                activeTweens[i].Complete();
                activeTweens.Remove(activeTweens[i]);
                continue;
            }

            //Update time of active tweens
            activeTweens[i].Update();
        }
    }

    #endregion
}