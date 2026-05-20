using UnityEngine;
using System.Collections.Generic;



[CreateAssetMenu(fileName = "LevelListData", menuName = "LevelListData", order = 0)]
public class LevelListData : ScriptableObject
{
    public List<LevelDataSO> levelDataSOs = new List<LevelDataSO>();
}