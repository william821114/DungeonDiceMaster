using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSpell : Skill
{

    private bool success;

    public override int skillWeight(int[] checkValue)
    {
        // 回3hp
        finalCheckValue = 3;
        return finalCheckValue;
    }
}