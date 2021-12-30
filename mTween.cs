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
            {
                instance = GameObject.Find("mTween").GetComponent<mTween>();
            }
            else
            {
                GameObject mTween = new GameObject("mTween");
                mTween x = mTween.AddComponent<mTween>();
                instance = x;
            }
        }
    }

    void LateUpdate() => StartCoroutine(LateLateUpdate());

    private IEnumerator LateLateUpdate()
    {
        yield return new WaitForEndOfFrame();
        UpdateQueuedTweens();
    }

    #endregion

    #region New DelayedCall Instance

    private static List<Tween> myCall = new List<Tween>();

    /// <summary>
    /// Queue new tween to be called after a period of time, returns call onComplete and onUpdate
    /// </summary>
    public static Tween NewTween(float time)
    {
        Tween i = new Tween(time).SetID();
        myCall.Add(i);

        if(instance == null)
        {
            GameObject mTween = new GameObject("mTween");
            mTween x = mTween.AddComponent<mTween>();
            instance = x;
        }

        return i;
    }

    /// <summary>
    /// Queue new tween to be called after a period of time, returns call onComplete and onUpdate
    /// </summary>
    public static Tween NewTween(GameObject id, float time)
    {
        Tween i = new Tween(id, time);
        myCall.Add(i);

        if (instance == null)
        {
            GameObject mTween = new GameObject("mTween");
            mTween x = mTween.AddComponent<mTween>();
            instance = x;
        }

        return i;
    }

    /// <summary>
    /// Queue new tween to be called after a period of time, returns call onComplete and onUpdate
    /// </summary>
    public static Tween NewTween(string id, float time)
    {
        Tween i = new Tween(id, time);
        myCall.Add(i);

        if (instance == null)
        {
            GameObject mTween = new GameObject("mTween");
            mTween x = mTween.AddComponent<mTween>();
            instance = x;
        }

        return i;
    }

    /// <summary>
    /// Triggers an action after x amount of time
    /// </summary>
    public static Tween DelayedCall(float time, Action OnComplete)
    {
        Tween i = new Tween(time);
        i.SetOnComplete(OnComplete);
        myCall.Add(i);

        if (instance == null)
        {
            GameObject mTween = new GameObject("mTween");
            mTween x = mTween.AddComponent<mTween>();
            instance = x;
        }

        return i;
    }

    /// <summary>
    /// Triggers an action after x amount of time
    /// </summary>
    public static Tween DelayedCall(GameObject id, float time, Action OnComplete)
    {
        Tween i = new Tween(id, time).SetOnComplete(OnComplete);
        myCall.Add(i);

        if (instance == null)
        {
            GameObject mTween = new GameObject("mTween");
            mTween x = mTween.AddComponent<mTween>();
            instance = x;
        }

        return i;
    }

    #endregion

    #region Class Methods

    /// <summary>
    /// Cancels all tweens on this gameObject
    /// </summary>
    public static void CancelAllTweens()
    {
        if (myCall.Count == 0) return;

        for (int i = 0; i < myCall.Count; i++)
            myCall[i].Cancel();
    }

    /// <summary>
    /// Cancel tween by ID
    /// </summary>
    public static void CancelTween(string id)
    {
        for (int i = 0; i < myCall.Count; i++)
            if (myCall[i].GetID() == id)
                myCall[i].Cancel();
    }

    /// <summary>
    /// Cancel tween by ID
    /// </summary>
    public static void CancelTween(GameObject id)
    {
        for (int i = 0; i < myCall.Count; i++)
            if (myCall[i].GetID() == id.GetInstanceID().ToString())
                myCall[i].Cancel();
    }

    /// <summary>
    /// Cancel tween by ID after t seconds
    /// </summary>
    public static void CancelTween(GameObject id, float t)
    {
        for (int i = 0; i < myCall.Count; i++)
            if (myCall[i].GetID() == id.GetInstanceID().ToString())
                myCall.Add(new Tween(t).SetOnComplete(() => myCall[i].Cancel()));
    }

    /// <summary>
    /// Cancel tween by ID after t seconds
    /// </summary>
    public static void CancelTween(string id, float t)
    {
        for (int i = 0; i < myCall.Count; i++)
            if (myCall[i].GetID() == id)
            {
                myCall[i].Cancel();
                myCall.Add(new Tween(t).SetOnComplete(() => myCall[i].Cancel()));
            }
    }

    /// <summary>
    /// Pause tween by ID if provided tween is not paused
    /// </summary>
    public static void PauseTween(string id, float duration)
    {
        for (int i = 0; i < myCall.Count; i++)
            if (myCall[i].GetID() == id && !myCall[i].paused)
                myCall[i].Pause(duration);
    }

    /// <summary>
    /// Pause tween by ID if provided tween is not paused, possibly for a duration
    /// </summary>
    public static void PauseTween(GameObject id, float duration)
    {
        for (int i = 0; i < myCall.Count; i++)
            if (myCall[i].GetID() == id.GetInstanceID().ToString() && !myCall[i].paused)
                myCall[i].Pause(duration);
    }

    /// <summary>
    /// Pause tween by ID if provided tween is not paused
    /// </summary>
    public static void PauseTween(string id)
    {
        for (int i = 0; i < myCall.Count; i++)
            if (myCall[i].GetID() == id && !myCall[i].paused)
                myCall[i].Pause();
    }
    public static void PauseTween(GameObject id)
    {
        for (int i = 0; i < myCall.Count; i++)
            if (myCall[i].GetID() == id.GetInstanceID().ToString() && !myCall[i].paused)
                myCall[i].Pause();
    }

    /// <summary>
    /// Continue tween by ID if provided tween is paused
    /// </summary>
    public static void ContinueTween(string id)
    {
        for (int i = 0; i < myCall.Count; i++)
            if (myCall[i].GetID() == id && myCall[i].paused)
                myCall[i].Resume();
    }
    public static void ContinueTween(GameObject id)
    {
        for (int i = 0; i < myCall.Count; i++)
            if (myCall[i].GetID() == id.GetInstanceID().ToString() && myCall[i].paused)
                myCall[i].Resume();
    }

    /// <summary>
    /// Pause all queued tweens
    /// </summary>
    public static void PauseAllTweens()
    {
        for (int i = 0; i < myCall.Count; i++)
            myCall[i].Pause();
    }

    /// <summary>
    /// Continue all tweens
    /// </summary>
    public static void ContinueAllTweens()
    {
        for (int i = 0; i < myCall.Count; i++)
            myCall[i].Resume();
    }

    /// <summary>
    /// Adds more time to an tween
    /// </summary>
    public static void DelayTween(string id, float delay)
    {
        for (int i = 0; i < myCall.Count; i++)
            if (myCall[i].GetID() == id)
                myCall[i].AddDuration(delay);
    }
    public static void DelayTween(GameObject id, float delay)
    {
        for (int i = 0; i < myCall.Count; i++)
            if (myCall[i].GetID() == id.GetInstanceID().ToString())
                myCall[i].AddDuration(delay);
    }

    /// <summary>
    /// Adds more time to an tween. NOTE: UNSAFE
    /// </summary>
    public static void DelayTweenUnsafe(string id, float delay)
    {
        for (int i = 0; i < myCall.Count; i++)
            if (myCall[i].GetID() == id)
                myCall[i].AddDurationUnsafe(delay);
    }
    public static void DelayTweenUnsafe(GameObject id, float delay)
    {
        for (int i = 0; i < myCall.Count; i++)
            if (myCall[i].GetID() == id.GetInstanceID().ToString())
                myCall[i].AddDurationUnsafe(delay);
    }

    /// <summary>
    /// Get the number of active tweens
    /// </summary>
    public static int GetActiveTweensCount() => myCall.Count;

    private void UpdateQueuedTweens()
    {
        if (myCall.Count == 0) return;

        for (int i = 0; i < myCall.Count; i++)
        {
            //Check if tween was canceled, remove current timeline if true
            if (myCall[i].canceled)
            {
                myCall.Remove(myCall[i]);
                i--;
                continue;
            }

            //Check if tween was paused, resume after unscaledPauseTime if one was provided
            if (myCall[i].paused)
            {
                myCall[i].UpdatePauseTime();

                if (myCall[i].unscaledPauseTime >= myCall[i].pauseTime && myCall[i].pauseTime != 0f)
                {
                    myCall[i].unscaledPauseTime = 0f;
                    myCall[i].Resume();
                }
                continue;
            }

            //Check if tween has an interval, delay the call between Complete and Start if true
            if (myCall[i].onInterval)
            {
                myCall[i].UpdateIntervalTime();

                if (myCall[i].unscaledIntervalTime >= myCall[i].intervalTime && myCall[i].intervalTime != 0f)
                {
                    myCall[i].unscaledIntervalTime = 0f;
                    myCall[i].onInterval = false;
                    myCall[i].unscaledProgress = 0f;
                }
                continue;
            }

            //Check if unscaledProgress is 0, call Start if true
            if (myCall[i].unscaledProgress == 0f)
                myCall[i].Start();

            //Check if unscaledProgress is same as durationWithDelay, try call Complete if true
            if (myCall[i].unscaledProgress >= (myCall[i].durationWithDelay - Mathf.Epsilon))
            {
                myCall[i].unscaledProgress = myCall[i].durationWithDelay;

                //Check if tween is set to repeat
                if (myCall[i].repeat)
                {
                    myCall[i].durationWithDelay = myCall[i].duration;
                    myCall[i].ResetCustomActionsList();
                    myCall[i].CompletedRun();

                    //Check if activeRepeatCount is same as repeatCount. Call Complete if true, else increment activeRepeatCount
                    if (myCall[i].activeRepeatCount == myCall[i].repeatCount && myCall[i].repeatCount != 0)
                    {
                        myCall[i].repeat = false;
                        myCall[i].activeRepeatCount = 0;
                        myCall[i].Complete();
                        myCall.Remove(myCall[i]);
                        continue;
                    }
                    else if (myCall[i].activeRepeatCount < myCall[i].repeatCount && myCall[i].repeatCount != 0)
                        myCall[i].activeRepeatCount++;

                    if (myCall[i].interval)
                        myCall[i].onInterval = true;
                    else
                        myCall[i].unscaledProgress = 0f;
                        
                    continue;
                }

                //Call Complete and remove self from active tweens
                myCall[i].Complete();
                myCall.Remove(myCall[i]);
                continue;
            }

            //Update time of active tweens
            myCall[i].Update();
        }
    }

    #endregion
}