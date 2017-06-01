using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall: Skill {

	public override int skillWeight(int[] checkValue){
        finalCheckValue = 0;
        for (int i = 0; i < checkValue.Length; i++)
        {
            finalCheckValue += checkValue[i];
        }

        if(finalCheckValue >= 8)
        {
            finalCheckValue += 5;
            Debug.Log("火球發動成功! - " + finalCheckValue);
        }

        return finalCheckValue;
	}

    public override SkillEffect calSkillEffect(int[] checkValue)
    {
        finalCheckValue = 0;
        for (int i = 0; i < checkValue.Length; i++)
        {
            finalCheckValue += checkValue[i];
        }

        if (finalCheckValue >= 8)
        {
            finalCheckValue += 5;
            Debug.Log("火球發動成功! - " + finalCheckValue);
        }

        SkillEffect skilleffect = new SkillEffect(this.isDamage,
                                                this.isHeal,
                                                this.isDodge,
                                                this.isShield,
                                                this.isDisable,
                                                this.isSelfDisable,
                                                this.isMPDamage,
                                                this.isHealMP);
        skilleffect.setDamage(finalCheckValue);
        return skilleffect;
    }
}