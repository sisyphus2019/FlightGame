using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    public int playerID;
    public string playerName;
    public int coins;
    public List<int> unlockedPlanes;
    public List<int> unlockedLevels;
}
