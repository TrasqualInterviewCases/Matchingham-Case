﻿using UnityEngine;

namespace TnTStudio.ObjectPlacer
{
    public class ToggleObject
    {
        public GameObject prefab;
        public Vector3 offset;
        public bool isActive;

        public ToggleObject(GameObject pre)
        {
            prefab = pre;
        }
    }
}