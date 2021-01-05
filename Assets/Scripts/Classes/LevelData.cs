﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Level", menuName = "ScriptableObjects/Level", order = 1)]
public class LevelData : ScriptableObject
{
    public int startTime, endTime;
    public int days;
    public List<GameObject> actionSlots;
}
