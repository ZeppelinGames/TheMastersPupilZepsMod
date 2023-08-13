using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TheMastersPupilZepsMod
{

    public class AchievementManager
    {
        public delegate void AchievementEarnedDelegate(Achievement ach);
        public static AchievementEarnedDelegate OnAchievementEarned;

        private float achievementActiveTime = 3f;

        private Canvas uiCanvas;
        private CanvasGroup uiGroup;

        private Text nameText;
        private Text descText;

        private bool activateUI;
        private float activeTime = 0;

        private GameObject player;

        public AchievementManager()
        {
            OnAchievementEarned = AchievementEarned;
        }

        int pickupsFound = 0;
        private HashSet<Achievement> pickupAchievements = new HashSet<Achievement>()
        {
            new PickupAchievement("Brush Found!", "{0}/{1} brushes collected", new Vector3(253,-16,0))
        };

        private Achievement[] achievements = new Achievement[] {
            new EventAchievement("Poof!", "Die once", EventManager.SubscribeOnDeathEvent),
        };

        private void AchievementEarned(Achievement ach)
        {
            if (ach.achieved)
            {
                return;
            }
            ach.achieved = true;

            //show popup
            if (uiCanvas != null)
            {
                uiGroup.alpha = 0;
                uiCanvas.gameObject.SetActive(true);

                string desc = ach.desc;
                if (pickupAchievements.Contains(ach))
                {
                    pickupsFound++;
                    desc = $"{pickupsFound}/{pickupAchievements.Count} hidden paintbrushes found!";
                }

                nameText.text = ach.name;
                descText.text = desc;

                activeTime = 0;
                activateUI = true;
            }
        }

        public void NewScene(int index)
        {
            CreateAchievementUI();
            foreach (PickupAchievement pickup in pickupAchievements)
            {
                if (pickup.scene == index)
                {
                    pickup.CreatePickup();
                }
            }
        }

        void CreateAchievementUI()
        {
            //Create canvas
            uiCanvas = GameObjectHelpers.CreateCanvas();
            uiCanvas.gameObject.SetActive(false);
            uiGroup = uiCanvas.gameObject.GetComponent<CanvasGroup>();

            RectTransform panel = GameObjectHelpers.CreatePanel(uiCanvas, Color.black, 450, 175, AnchorPreset.BottomRight);
            VerticalLayoutGroup vlg = panel.gameObject.AddComponent<VerticalLayoutGroup>();
            vlg.padding = new RectOffset(16, 16, 16, 16);
            vlg.childControlHeight = true;

            GameObjectHelpers.CreateTextBox(panel, "Achievement Unlocked!", Color.white, 16, TextAnchor.MiddleLeft, AnchorPreset.Stretch);
            nameText = GameObjectHelpers.CreateTextBox(panel, "Top Text", Color.white, 48, TextAnchor.MiddleLeft, AnchorPreset.Stretch);
            descText = GameObjectHelpers.CreateTextBox(panel, "Bottom Text", Color.white, 16, TextAnchor.MiddleLeft, AnchorPreset.Stretch);
        }

        public void Update(float dt)
        {
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }

            if (activateUI)
            {
                activeTime += dt;
                if (activeTime <= 1)
                {
                    uiGroup.alpha = activeTime;
                }
                if (activeTime >= (achievementActiveTime - 1))
                {
                    uiGroup.alpha = achievementActiveTime - activeTime;
                }

                if (activeTime >= achievementActiveTime)
                {
                    uiCanvas.gameObject.SetActive(false);
                    activateUI = false;
                }
            }

            if (player != null)
            {
                foreach (PickupAchievement pickup in pickupAchievements)
                {
                    Collider2D col = Physics2D.OverlapCircle(pickup.position, 1f);
                    if (col)
                    {
                        if (col.transform == player.transform)
                        {
                            TMPZepsMod.Log("YO NO WAY");
                            pickup.OnInvoke();
                            pickup.go.SetActive(false);
                        }
                    }
                }
            }
        }
    }
}