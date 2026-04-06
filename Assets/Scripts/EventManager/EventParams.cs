using System.Collections.Generic;
using UnityEngine;

public static class EventParams
{
    public class CombatAttackParam
    {
    }
    public class FinisherParam
    {
    }
    public class BloodEffectParam
    {
    }
    public class SoundParam
    {
        public Vector3 pos;

        public SoundParam(Vector3 pos)
        {
            this.pos = pos;
        }
    }
    public class LevelStartParam
    {
    }
    public class LevelSceneDetailsParam
    {
    }
    public class SceneChangeParam
    {
    }
}
