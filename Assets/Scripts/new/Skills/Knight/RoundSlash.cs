using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundSlash : Skill
{

    public override int skillWeight(int[] checkValue)
    {
        bool isTrigger = true;
        finalCheckValue = 0;
        for (int i = 0; i < checkValue.Length; i++)
        {
            finalCheckValue += checkValue[i];
            if (checkValue[i] < 2) isTrigger = false;
        }

        if (isTrigger)
        {
            finalCheckValue += 4;
            Debug.Log("迴旋斬發動成功! - " + finalCheckValue);
        }

        return finalCheckValue;
    }

    public override SkillEffect calSkillEffect(int[] checkValue)
    {
        bool isTrigger = true;
        finalCheckValue = 0;
        SkillEffect skilleffect;
        for (int i = 0; i < checkValue.Length; i++)
        {
            finalCheckValue += checkValue[i];
            if (checkValue[i] < 2) isTrigger = false;
        }

        if (isTrigger)
        {
            finalCheckValue += 4;
            Debug.Log("迴旋斬發動成功! - " + finalCheckValue);
        }

        skilleffect = new SkillEffect(this.isDamage, this.isHeal, this.isDodge, this.isShield, finalCheckValue, 0, 0, 0);
        return skilleffect;
    }
}