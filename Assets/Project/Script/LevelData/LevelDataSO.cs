using System;
using UnityEngine;
[CreateAssetMenu(fileName = "LevelData")]
public class LevelDataSO : ScriptableObject
{
    public int starEarned;
    public bool unlocked;
    public int Level;
    public string Question;
    public int Answer;
    public PuzzleType Category;
    public Diffculty difficulty;
}
