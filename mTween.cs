using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

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
        UpdateQueuedEvents();
    }

    #endregion

    #region New DelayedCall Instance

    private static List<Tween> myCall = new List<Tween>();

    /// <summary>
    /// Queue new event to be called after a period of time, returns call onComplete and onUpdate
    /// </summary>
    public static Tween NewTimeline(float time)
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
    /// Queue new event to be called after a period of time, returns call onComplete and onUpdate
    /// </summary>
    public static Tween NewTimeline(GameObject id, float time)
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
    /// Queue new event to be called after a period of time, returns call onComplete and onUpdate
    /// </summary>
    public static Tween NewTimeline(string id, float time)
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
    /// Delayed call that triggers an event after x amount of time
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
    /// Delayed call that triggers an event after x amount of time
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
    /// Cancels all events on this gameObject
    /// </summary>
    public static void CancelAllEvents()
    {
        if (myCall.Count == 0) return;

        for (int i = 0; i < myCall.Count; i++)
            myCall[i].Cancel();
    }

    /// <summary>
    /// Cancel event by ID
    /// </summary>
    public static void CancelEvent(string id)
    {
        for (int i = 0; i < myCall.Count; i++)
            if (myCall[i].GetID() == id)
            {
                myCall[i].Cancel();
                myCall[i].Cancel();
            }
    }

    /// <summary>
    /// Cancel event by ID
    /// </summary>
    public static void CancelEvent(GameObject id)
    {
        for (int i = 0; i < myCall.Count; i++)
            if (myCall[i].GetID() == id.GetInstanceID().ToString())
            {
                myCall[i].Cancel();
                myCall[i].Cancel();
            }
    }

    /// <summary>
    /// Cancel event by ID after t seconds
    /// </summary>
    public static void CancelEvent(GameObject id, float t)
    {
        for (int i = 0; i < myCall.Count; i++)
            if (myCall[i].GetID() == id.GetInstanceID().ToString())
            {
                myCall[i].Cancel();
                myCall.Add(new Tween(t).SetOnComplete(() => myCall[i].Cancel()));
            }
    }

    /// <summary>
    /// Cancel event by ID after t seconds
    /// </summary>
    public static void CancelEvent(string id, float t)
    {
        for (int i = 0; i < myCall.Count; i++)
            if (myCall[i].GetID() == id)
            {
                myCall[i].Cancel();
                myCall.Add(new Tween(t).SetOnComplete(() => myCall[i].Cancel()));
            }
    }

    /// <summary>
    /// Pause event by ID if provided event is not paused
    /// </summary>
    public static void PauseEvent(string id, float duration)
    {
        for (int i = 0; i < myCall.Count; i++)
            if (myCall[i].GetID() == id && !myCall[i].paused)
                myCall[i].Pause(duration);
    }

    /// <summary>
    /// Pause event by ID if provided event is not paused
    /// </summary>
    public static void PauseEvent(GameObject id, float duration)
    {
        for (int i = 0; i < myCall.Count; i++)
            if (myCall[i].GetID() == id.GetInstanceID().ToString() && !myCall[i].paused)
                myCall[i].Pause(duration);
    }

    /// <summary>
    /// Pause event by ID if provided event is not paused
    /// </summary>
    public static void PauseEvent(string id)
    {
        for (int i = 0; i < myCall.Count; i++)
            if (myCall[i].GetID() == id && !myCall[i].paused)
                myCall[i].Pause();
    }

    /// <summary>
    /// Pause event by ID if provided event is not paused
    /// </summary>
    public static void PauseEvent(GameObject id)
    {
        for (int i = 0; i < myCall.Count; i++)
            if (myCall[i].GetID() == id.GetInstanceID().ToString() && !myCall[i].paused)
                myCall[i].Pause();
    }

    /// <summary>
    /// Continue event by ID if provided event is paused
    /// </summary>
    public static void ContinueEvent(string id)
    {
        for (int i = 0; i < myCall.Count; i++)
            if (myCall[i].GetID() == id && myCall[i].paused)
                myCall[i].Resume();
    }

    /// <summary>
    /// Continue event by ID if provided event is paused
    /// </summary>
    public static void ContinueEvent(GameObject id)
    {
        for (int i = 0; i < myCall.Count; i++)
            if (myCall[i].GetID() == id.GetInstanceID().ToString() && myCall[i].paused)
                myCall[i].Resume();
    }

    /// <summary>
    /// Pause all queued events
    /// </summary>
    public static void PauseAllEvents()
    {
        for (int i = 0; i < myCall.Count; i++)
            myCall[i].Pause();
    }

    /// <summary>
    /// Continue all events
    /// </summary>
    public static void ContinueAllEvents()
    {
        for (int i = 0; i < myCall.Count; i++)
            myCall[i].Resume();
    }

    /// <summary>
    /// Adds more time to an event
    /// </summary>
    public static void DelayEvent(string id, float delay)
    {
        for (int i = 0; i < myCall.Count; i++)
            if (myCall[i].GetID() == id)
                myCall[i].AddDelay(delay);
    }

    /// <summary>
    /// Adds more time to an event
    /// </summary>
    public static void DelayEvent(GameObject id, float delay)
    {
        for (int i = 0; i < myCall.Count; i++)
            if (myCall[i].GetID() == id.GetInstanceID().ToString())
                myCall[i].AddDelay(delay);
    }

    /// <summary>
    /// Adds more time to an event
    /// </summary>
    public static void DelayEventUnsafe(string id, float delay)
    {
        for (int i = 0; i < myCall.Count; i++)
            if (myCall[i].GetID() == id)
                myCall[i].AddDelayUnsafe(delay);
    }

    /// <summary>
    /// Adds more time to an event
    /// </summary>
    public static void DelayEventUnsafe(GameObject id, float delay)
    {
        for (int i = 0; i < myCall.Count; i++)
            if (myCall[i].GetID() == id.GetInstanceID().ToString())
                myCall[i].AddDelayUnsafe(delay);
    }

    public static int GetActiveTimelinesCount() => myCall.Count;

    private void UpdateQueuedEvents()
    {
        if (myCall.Count == 0) return;

        for (int i = 0; i < myCall.Count; i++)
        {
            //Check if event was canceled, remove current timeline if true
            if (myCall[i].canceled)
            {
                myCall.Remove(myCall[i]);
                i--;
                continue;
            }

            //Check if event was paused, resume after unscaledPauseTime if one was provided
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

            //Check if event has an interval, delay the call between Complete and Start if true
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

                //Check if timeline is set to repeat
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

                //Call Complete and remove self from active mTimelines
                myCall[i].Complete();
                myCall.Remove(myCall[i]);
                continue;
            }

            //Update time of active mTimeline
            myCall[i].Update();
        }
    }

    #endregion
}