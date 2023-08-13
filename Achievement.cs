using System;
using System.Collections;
using UnityEngine;

namespace TheMastersPupilZepsMod
{
    public class Achievement : EventSubscriber
    {
        public string name = "";
        public string desc = "";

        public bool achieved = false;

        public Action<EventSubscriber>? subscribeEvent;

        public Achievement() { }

        public Achievement(Action<EventSubscriber> subscribeEvent)
        {
            this.subscribeEvent = subscribeEvent;
            this.subscribeEvent.Invoke(this);
        }

        public Achievement(string name, string desc, Action<EventSubscriber> subscribeEvent = null)
        {
            this.name = name;
            this.desc = desc;

            if (subscribeEvent != null)
            {
                this.subscribeEvent = subscribeEvent;
                this.subscribeEvent.Invoke(this);
            }
        }

        public override void OnInvoke()
        {
            AchievementManager.OnAchievementEarned(this);
        }
    }
}