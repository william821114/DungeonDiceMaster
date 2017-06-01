using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : Skill
{

    public override int skillWeight(int[] checkValue)
    {
        bool isTrigger = true;
        finalCheckValue = 0;
        for (int i = 0; i < checkValue.Length; i++)
        {
            if (checkValue[i] > 5) isTrigger = false;
        }

        if (isTrigger)
        {

            Debug.Log("迴避發動成功!");
            finalCheckValue = 1;

        }

        return finalCheckValue;
    }

    public override SkillEffect calSkillEffect(int[] checkValue)
    {
        bool isTrigger = true;
        finalCheckValue = 0;
        for (int i = 0; i < checkValue.Length; i++)
        {
            if (checkValue[i] > 5) isTrigger = false;
        }

        if (isTrigger)
        {

            Debug.Log("迴避發動成功!");
            finalCheckValue = 1;

        }

        SkillEffect skilleffect = new SkillEffect(this.isDamage, this.isHeal, this.isDodge, this.isShield, 0, 0, finalCheckValue, 0);
        return skilleffect;
    }
}