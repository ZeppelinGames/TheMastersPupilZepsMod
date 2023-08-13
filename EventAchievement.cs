using System;
using System.Collections;
using UnityEngine;

namespace TheMastersPupilZepsMod
{
    public class EventAchievement : Achievement
    {
        public EventAchievement(string name, string desc, Action<EventSubscriber> subscribeEvent) : base(name, desc, subscribeEvent) { }
    }
}