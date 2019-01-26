using UnityEngine;
using System.Collections;

public enum ChickStatus
{
    worry = 0b0000001,
    hungry = 0b0000010,
    ill = 0b0000100,
    dead = 0b0001000,
    normal = 0b0000000
}
public enum MomStatus
{
    normal,
    hungry
}
public enum Direction
{
    left,
    right,
    top,
    buttom
}