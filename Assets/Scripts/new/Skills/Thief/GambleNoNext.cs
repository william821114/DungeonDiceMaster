using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GambleNoNext: Skill {

	public override int skillWeight(int[] checkValue){
		return finalCheckValue;
	}

    public override SkillEffect calSkillEffect(int[] checkValue)
    {
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
            Debug.Log("下一擊不會中!");
        }

        SkillEffect skilleffect = new SkillEffect(this.isDamage,
                                        this.isHeal,
                                        this.isDodge,
                                        this.isShield,
                                        this.isDisable,
                                        this.isSelfDisable,
                                        this.isMPDamage,
                                        this.isHealMP);
        skilleffect.setDodge(finalCheckValue);
        return skilleffect;
    }
}