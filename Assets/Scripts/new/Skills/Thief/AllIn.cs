using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllIn: Skill {

	public override int skillWeight(int[] checkValue){

        bool isAllSix = true;
        for (int i = 0; i < checkValue.Length; i++)
        {
            finalCheckValue += checkValue[i];
            if (checkValue[i] != 6) isAllSix = false;
        }

        if(isAllSix)
        {
            finalCheckValue = 60;
            Debug.Log("梭哈成功! - " + finalCheckValue);
        } else
        {
            finalCheckValue = 0;
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

        bool isAllSix = true;
        for (int i = 0; i < checkValue.Length; i++)
        {
            finalCheckValue += checkValue[i];
            if (checkValue[i] != 6) isAllSix = false;
        }

        if (isAllSix)
        {
            finalCheckValue = 60;
            skilleffect.setSkillActivated(true);
            Debug.Log("梭哈成功! - " + finalCheckValue);
        }
        else
        {
            finalCheckValue = 0;
        }

        
        skilleffect.setDamage(finalCheckValue);
        return skilleffect;
    }

}