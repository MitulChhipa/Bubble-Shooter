using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BallColor
{
    RED,
    GREEN, 
    BLUE,
    ORANGE,
    YELLOW,
}

[CreateAssetMenu(fileName = "New Ball", menuName ="Create Ball/new Ball")]
public class BallScriptable : ScriptableObject
{
    public BallColor colorType;
    public Material material;
}
