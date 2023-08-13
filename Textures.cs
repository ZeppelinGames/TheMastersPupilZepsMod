using System;
using System.Collections;
using UnityEngine;

namespace TheMastersPupilZepsMod
{
    public static class Textures
    {
        public static Texture2D LoadTexture(string path)
        {
            Texture2D texture = new Texture2D(1, 1);
            ImageConversion.LoadImage(texture, System.IO.File.ReadAllBytes(path));
            texture.Apply();

            return texture;
        }
    }
}