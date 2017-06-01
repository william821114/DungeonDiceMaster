using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavySlash : Skill
{

    public override int skillWeight(int[] checkValue)
    {
        return finalCheckValue;
    }

    public override SkillEffect calSkillEffect(int[] checkValue)
    {

        finalCheckValue = 0;
        SkillEffect skilleffect = new SkillEffect(this.isDamage, this.isHeal, this.isDodge, this.isShield, finalCheckValue, 0, 0, 0);

        for (int i = 0; i < checkValue.Length; i++)
        {
            finalCheckValue += checkValue[i];

        }

        if (finalCheckValue > 7)
        {
            finalCheckValue *= 2;
            skilleffect.setDamage(finalCheckValue);
            skilleffect.setHeal(2);
            Debug.Log("重擊發動成攻! - " + finalCheckValue);
        }

        return skilleffect;
    }
}