using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamble: Skill {

	public override int skillWeight(int[] checkValue){
        finalCheckValue = 0;
        for (int i = 0; i < checkValue.Length; i++)
        {
            finalCheckValue += checkValue[i];
        }

        if (finalCheckValue >= 12)
        {
            finalCheckValue *= 2;
            Debug.Log("賭術發動成功! -" + finalCheckValue);
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

        finalCheckValue = 0;
        for (int i = 0; i < checkValue.Length; i++)
        {
            finalCheckValue += checkValue[i];
        }

        if (finalCheckValue >= 12)
        {
            finalCheckValue *= 2;
            skilleffect.setSkillActivated(true);
            Debug.Log("賭術發動成功! -" + finalCheckValue);
        }
        else
        {
            finalCheckValue = 0;
        }

        
        skilleffect.setDamage(finalCheckValue);
        return skilleffect;
    }
}