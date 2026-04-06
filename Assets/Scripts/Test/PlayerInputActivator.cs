using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputActivator : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager<bool>.EventTrigger(EventKey.ControlEnabled_All, false);
    }

    private void OnDisable()
    {
        EventManager<bool>.EventTrigger(EventKey.ControlEnabled_All, true);
    }
}
