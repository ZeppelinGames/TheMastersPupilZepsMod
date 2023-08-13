using System.Collections;
using UnityEngine;
using HarmonyLib;
using System;

namespace TheMastersPupilZepsMod
{

    public static class EventManager
    {
        // Death (generic)
        public delegate void OnDeathDelegate();
        public static OnDeathDelegate? onDeathEvent;
        public static void SubscribeOnDeathEvent(EventSubscriber es) { onDeathEvent += es.OnInvoke; }

        [HarmonyPatch(typeof(DeathAndSaves), "Death")]
        private static class PlayerDeath
        {
            private static void Postfix()
            {
                onDeathEvent?.Invoke();
            }
        }
    }
}