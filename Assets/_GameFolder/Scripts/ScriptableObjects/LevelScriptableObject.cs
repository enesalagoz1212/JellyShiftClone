using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JellyShiftClone.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewLevel", menuName = "Custom/Level")]
    public class LevelScriptableObject : ScriptableObject
    {
        [Header("Starting and Ending Positions")]
        public Vector3 playerStartTransform; 
        public Vector3 playerExitTransform;

        [Header("Level Prefabs")]
        public GameObject[] levelPrefabs;
    }
}

