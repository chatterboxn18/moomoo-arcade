using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/MooItems", order = 1)]
public class MooItems : ScriptableObject
{
    public List<Mamamoo> Mamamoos;
}

[Serializable]
public class Mamamoo
{
    public string dataName;
    public string dataPath;
    public int SpazzAmount;
}