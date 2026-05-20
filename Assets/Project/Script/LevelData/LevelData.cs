using System;
using UnityEngine;
[CreateAssetMenu(fileName = "LevelData")]
public class LevelData : ScriptableObject
{
    public int starEarned;
    public bool unlocked;
    public int Level;
    public string Question;
    public int Answer;
    public string Category;
    public Diffculty difficulty;
}

public enum Diffculty
{
    Easy,
    Medium,
    Hard
}


