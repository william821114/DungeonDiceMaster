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

            Debug.Log("疾風發動成功!");
            finalCheckValue = 1;

        }

        return finalCheckValue;
    }

    public override SkillEffect calSkillEffect(int[] checkValue)
    {
        SkillEffect skilleffect = new SkillEffect(this.isDamage,
                                       this.isHeal,
                                       this.isDodge,
                                       this.isShield,
                                       this.isDisable,
                                       this.isSelfDisable,
                                       this.isMPDamage,
                                       this.isHealMP);

        bool isTrigger = true;
        finalCheckValue = 0;
        for (int i = 0; i < checkValue.Length; i++)
        {
            if (checkValue[i] > 4) isTrigger = false;
            finalCheckValue += checkValue[i];
        }

        if (isTrigger)
        {

            Debug.Log("疾風發動成功!");
            skilleffect.setSkillActivated(true);
            finalCheckValue = 1;

        }
        skilleffect.setDisable(0, 2);
        skilleffect.setDodge(1);
        skilleffect.setDamage(finalCheckValue);

        return skilleffect;
    }
}