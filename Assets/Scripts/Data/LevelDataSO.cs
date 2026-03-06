using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Arrows/Level Data")]
public class LevelDataSO : ScriptableObject
{
    [Min(1)] public int width = 5;
    [Min(1)] public int height = 5;
    public List<ArrowSpawnData> arrows = new List<ArrowSpawnData>();
}