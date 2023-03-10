using System.Collections.Generic;
using Data.ValueObjects;
using UnityEngine;

namespace Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_Level",menuName = "CubeSurfer/CD_Level")]
    public class CD_Level : ScriptableObject
    {
        public List<LevelData> LevelList = new List<LevelData>();
    }
}