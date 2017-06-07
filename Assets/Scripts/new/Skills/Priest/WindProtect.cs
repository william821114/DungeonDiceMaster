using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindProtect: Skill {

	public override int skillWeight(int[] checkValue){
        finalCheckValue = 0;
        for (int i = 0; i < checkValue.Length; i++)
        {
            finalCheckValue += checkValue[i];
        }

        if (finalCheckValue > 10)
        {
            Debug.Log("風之守護!");
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

        finalCheckValue = 0;
        for (int i = 0; i < checkValue.Length; i++)
        {
            finalCheckValue += checkValue[i];
        }

        if (finalCheckValue > 10)
        {
            Debug.Log("風之守護!");
            skilleffect.setSkillActivated(true);
            finalCheckValue = 1;
        }

        
        skilleffect.setDodge(finalCheckValue);
        return skilleffect;
    }
}