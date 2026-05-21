using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelJsonData
{
    public int Level;
    public string Question;
    public string Answer;
    public int timelimit;
    public PuzzleType Category;
    public Diffculty difficulty;
}

[System.Serializable]
public class LevelJsonWrapper
{
    public List<LevelJsonData> Levels;
}