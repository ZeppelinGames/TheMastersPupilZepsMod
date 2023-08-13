using System;
using TheMastersPupilZepsMod;
using MelonLoader;
using UnityEngine;

[assembly: MelonInfo(typeof(TMPZepsMod), "TMPZepsMod", "1.0.0", "ZeppelinGames")]
[assembly: MelonGame(name:"TheMastersPupil-v1.2")]

namespace TheMastersPupilZepsMod
{
    public class TMPZepsMod : MelonMod
    {
        public delegate void LogDelegate(string txt);
        public static LogDelegate Log;

        private AchievementManager achievementManager = new AchievementManager();
        private GameObject player;

        private LineRenderer debugLine;

        public override void OnInitializeMelon()
        {
            base.OnInitializeMelon();

            EventManager.onDeathEvent += () =>
            {
                LoggerInstance.Msg("Player died");
            };

            Log = log;
        }

        void log(string s)
        {
            LoggerInstance.Msg(s);
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            achievementManager.NewScene(buildIndex);

            // Create debug line
            debugLine = new GameObject().AddComponent<LineRenderer>();
            debugLine.startWidth = 0.1f;
            debugLine.endWidth = 0.1f;

            debugLine.positionCount = 2;
        }

        public override void OnUpdate()
        {
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }

            if (player != null)
            {
            }

            achievementManager.Update(Time.deltaTime);
        }
    }
}
