using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaDraw: Skill {

	public override int skillWeight(int[] checkValue){
		return finalCheckValue;
	}

    public override SkillEffect calSkillEffect(int[] checkValue)
    {
        finalCheckValue = 0;
        for (int i = 0; i < checkValue.Length; i++)
        {
            if(checkValue[i] % 2 == 1)
            {
                finalCheckValue += checkValue[i];
            }
            
        }


        SkillEffect skilleffect = new SkillEffect(this.isDamage,
                                                this.isHeal,
                                                this.isDodge,
                                                this.isShield,
                                                this.isDisable,
                                                this.isSelfDisable,
                                                this.isMPDamage,
                                                this.isHealMP);

        // 吸收對方的MP 回復自己的

        Debug.Log("魔力吸收! - " + finalCheckValue);
        skilleffect.setHealMP(finalCheckValue);
        skilleffect.setMPDamage(finalCheckValue);
        return skilleffect;
    }
}