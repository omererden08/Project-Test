using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager<TEventArgs>
{
    private static Dictionary<EventKey, Action<TEventArgs>> dic_Events = new Dictionary<EventKey, Action<TEventArgs>>();
    public static void EventRegister(EventKey eventKey, Action<TEventArgs> eventHandler)
    {
        if (dic_Events.ContainsKey(eventKey))
            dic_Events[eventKey] += eventHandler;
        else
            dic_Events[eventKey] = eventHandler;
    }
    public static void EventUnregister(EventKey eventKey, Action<TEventArgs> eventHandler)
    {
        if (!dic_Events.ContainsKey(eventKey)) return;

        dic_Events[eventKey] -= eventHandler;
    }
    public static void EventTrigger(EventKey eventKey, TEventArgs eventArgs)
    {
        if (!dic_Events.ContainsKey(eventKey)) return;

        dic_Events[eventKey]?.Invoke(eventArgs);
    }

    #region Coroutine
    public static void EventTrigger(EventKey eventKey, TEventArgs eventArgs, float delayTime)
    {
        StaticCoroutine.Start(Coroutine_EventTrigger(eventKey, eventArgs, delayTime));
    }
    private static IEnumerator Coroutine_EventTrigger(EventKey eventKey, TEventArgs eventArgs, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        EventTrigger(eventKey, eventArgs);
        yield break;
    }
    #endregion
}

#region Enum
public enum EventKey
{
    PlayerHealthUpdate,
    PlayerHealthIncrease,
    PlayerHealthIncrease_Percent,
    PlayerFlip,
    Jump,
    Player_ScaleIncrease,
    Player_ChangeColor_Request,
    Player_ChangeColor_Apply,
    Input_DeviceUpdated,
    Input_ActionMap_Update,
    Input_ActionMap_Updated,
    ControlEnabled_All,
    Finisher_Start,
    FinisherCheck_Refresh,
    Cam_Shake,
    SlowTime,
    Enemy_Spawn,
    Enemy_Dead,
    Gold_Increase,
    Gold_Updated,
    LevelStart,
    LevelWaves_Completed,
    LevelReward_Show,
    LevelDoor_Enter,
    RunStarted,
    GameOver,
    RunReset,
    StartScene_ForRun,
    NextScene_ForRun,
    GameScene_Open,
    UpgradeTake,
    UpgradeRemove,
    UI_Submit,
    UI_Cancel,
    UI_FlashEffect,
    BuildMode_BusRoom_Change,
    BuildMode_Place,
    BuildMode_Clear,
    BuildMode_OuterVisuals_Toggle,
    BuildMode_OuterVisuals_Updated,
    BuildMode_ColorChange_Input,
    BuildMode_ColorChange_Updated,
    BuildMode_TopPos_Updated,
    BuildModeFluffy_Activate,
    BuildModeFluffy_Place,
    BuildModeFluffy_Placed,
    Music_Play,
    Sound_Play
}
#endregion