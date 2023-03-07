using System;
using System.Collections.Generic;

namespace Data.ValueObjects
{
    
    [Serializable]
    public struct LevelData
    {
        public int level;

        public LevelData(int levelIndex)
        {
            level = levelIndex;
        }
    }
}