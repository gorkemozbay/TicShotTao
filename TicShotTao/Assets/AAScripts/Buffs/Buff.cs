using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Buff", menuName = "Buffs")]
public class Buff : ScriptableObject
{
    public string buffName;
    public string buffDescription;
    public Sprite buffIcon;
    public float buffPower;
    public int buffLevel;

}
