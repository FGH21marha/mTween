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

    private static List<mTimeline> myCall = new List<mTimeline>();

    /// <summary>
    /// Queue new event to be called after a period of time, returns call onComplete and onUpdate
    /// </summary>
    public static mTimeline NewTimeline(float time)
    {
        mTimeline i = new mTimeline(time);
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
    public static mTimeline NewTimeline(GameObject id, float time)
    {
        mTimeline i = new mTimeline(id, time);
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
    public static mTimeline NewTimeline(string id, float time)
    {
        mTimeline i = new mTimeline(id, time);
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
    public static mTimeline DelayedCall(float time, Action OnComplete)
    {
        mTimeline i = new mTimeline(time);
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
    public static mTimeline DelayedCall(GameObject id, float time, Action OnComplete)
    {
        mTimeline i = new mTimeline(id, time).SetOnComplete(OnComplete);
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
                myCall[i].OnCancelEvent();
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
                myCall[i].OnCancelEvent();
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
                myCall[i].OnCancelEvent();
                myCall.Add(new mTimeline(t).SetOnComplete(() => myCall[i].Cancel()));
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
                myCall[i].OnCancelEvent();
                myCall.Add(new mTimeline(t).SetOnComplete(() => myCall[i].Cancel()));
            }
    }

    /// <summary>
    /// Pause event by ID if provided event is not paused
    /// </summary>
    public static void PauseEvent(string id, float duration)
    {
        for (int i = 0; i < myCall.Count; i++)
            if (myCall[i].GetID() == id && !myCall[i].paused)
                myCall[i].PauseEvent(duration);
    }

    /// <summary>
    /// Pause event by ID if provided event is not paused
    /// </summary>
    public static void PauseEvent(GameObject id, float duration)
    {
        for (int i = 0; i < myCall.Count; i++)
            if (myCall[i].GetID() == id.GetInstanceID().ToString() && !myCall[i].paused)
                myCall[i].PauseEvent(duration);
    }

    /// <summary>
    /// Pause event by ID if provided event is not paused
    /// </summary>
    public static void PauseEvent(string id)
    {
        for (int i = 0; i < myCall.Count; i++)
            if (myCall[i].GetID() == id && !myCall[i].paused)
                myCall[i].PauseEvent();
    }

    /// <summary>
    /// Pause event by ID if provided event is not paused
    /// </summary>
    public static void PauseEvent(GameObject id)
    {
        for (int i = 0; i < myCall.Count; i++)
            if (myCall[i].GetID() == id.GetInstanceID().ToString() && !myCall[i].paused)
                myCall[i].PauseEvent();
    }

    /// <summary>
    /// Continue event by ID if provided event is paused
    /// </summary>
    public static void ContinueEvent(string id)
    {
        for (int i = 0; i < myCall.Count; i++)
            if (myCall[i].GetID() == id && myCall[i].paused)
                myCall[i].ContinueEvent();
    }

    /// <summary>
    /// Continue event by ID if provided event is paused
    /// </summary>
    public static void ContinueEvent(GameObject id)
    {
        for (int i = 0; i < myCall.Count; i++)
            if (myCall[i].GetID() == id.GetInstanceID().ToString() && myCall[i].paused)
                myCall[i].ContinueEvent();
    }

    /// <summary>
    /// Pause all queued events
    /// </summary>
    public static void PauseAllEvents()
    {
        for (int i = 0; i < myCall.Count; i++)
            myCall[i].PauseEvent();
    }

    /// <summary>
    /// Continue all events
    /// </summary>
    public static void ContinueAllEvents()
    {
        for (int i = 0; i < myCall.Count; i++)
            myCall[i].ContinueEvent();
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

    private void UpdateQueuedEvents()
    {
        if (myCall.Count == 0) return;

        for (int i = 0; i < myCall.Count; i++)
        {
            if (myCall[i].canceled)
            {
                myCall.Remove(myCall[i]);
                i--;
                continue;
            }

            if (myCall[i].paused)
            {
                myCall[i].UpdatePauseTime();

                if (myCall[i].unscaledPauseTime >= myCall[i].pauseTime && myCall[i].pauseTime != 0f)
                {
                    myCall[i].unscaledPauseTime = 0f;
                    myCall[i].ContinueEvent();
                }
                continue;
            }

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

            if (myCall[i].unscaledProgress == 0f)
                myCall[i].Start();

            if (myCall[i].unscaledProgress >= (myCall[i].durationWithDelay - Mathf.Epsilon))
            {
                myCall[i].unscaledProgress = myCall[i].durationWithDelay;

                if (myCall[i].repeat)
                {
                    myCall[i].ResetCustomActionsList();
                    myCall[i].durationWithDelay = myCall[i].duration;
                    myCall[i].CompletedRun();

                    if (myCall[i].activeRepeatCount == myCall[i].repeatCount && myCall[i].repeatCount != 0)
                    {
                        myCall[i].repeat = false;
                        myCall[i].activeRepeatCount = 0;
                        myCall[i].Complete();
                        myCall.Remove(myCall[i]);
                        continue;
                    }
                    else if (myCall[i].activeRepeatCount < myCall[i].repeatCount && myCall[i].repeatCount != 0)
                    {
                        myCall[i].activeRepeatCount++;
                    }

                    if (myCall[i].interval)
                    {
                        myCall[i].onInterval = true;
                    }
                    else
                    {
                        myCall[i].unscaledProgress = 0f;
                    }
                        
                    continue;
                }

                myCall[i].Complete();
                myCall.Remove(myCall[i]);
                continue;
            }

            myCall[i].UpdateTime();
            myCall[i].Update();
        }
    }

    #endregion
}