using System.Collections;
using UnityEngine;

namespace TheMastersPupilZepsMod
{
    public class PickupAchievement : Achievement
    {
        public string Desc
        {
            get
            {
                return "Nah";
            }
        }

        public Vector3 position;
        public int scene;
        public GameObject go;

        public PickupAchievement(string name, string desc, Vector3 position, int scene = -1)
        {
            this.name = name;
            this.desc = desc;
            this.position = position;
            this.scene = scene;
        }

        public void CreatePickup()
        {
            GameObject pickupObject = new GameObject($"Pickup_{name}");
            BoxCollider2D collider = pickupObject.AddComponent<BoxCollider2D>();
            collider.isTrigger = true;

            pickupObject.transform.position = this.position;

            SpriteRenderer spr = pickupObject.AddComponent<SpriteRenderer>();

            Material spriteMaterial = new Material(Shader.Find("Sprites/Default"));
            spriteMaterial.SetFloat("_Mode", 2); // Set rendering mode to "Transparent"

            spr.material = spriteMaterial;

            Texture2D tex = Textures.LoadTexture("./Mods/assets/pickupSmall.png");
            spr.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one / 2);

            this.go = pickupObject;
        }

        public override void OnInvoke()
        {
            base.OnInvoke();
        }
    }
}