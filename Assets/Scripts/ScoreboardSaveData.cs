using System;
using System.Collections.Generic;

namespace Stephen.Scoreboards
{
    [Serializable]
    public class ScoreboardSaveData
    {
        public List<ScoreboardEntryData> highscores = new List<ScoreboardEntryData>();
    }
}