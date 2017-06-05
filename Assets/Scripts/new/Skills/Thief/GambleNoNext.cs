using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GambleNoNext: Skill {

	public override int skillWeight(int[] checkValue){
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

        bool isAllSmallerThanThree = true;
      
        for (int i = 0; i < checkValue.Length; i++)
        {
            if(checkValue[i] >= 3)
            {
                isAllSmallerThanThree = false;
            }
        }

        if(isAllSmallerThanThree)
        {
            finalCheckValue = 1;
            skilleffect.setSkillActivated(true);
            Debug.Log("下一擊不會中!");
        }

        
        skilleffect.setDodge(finalCheckValue);
        return skilleffect;
    }
}