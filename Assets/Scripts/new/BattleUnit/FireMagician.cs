using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMagician : Monster
{

    public override void AI(int[] checkValue)
    {
        // 強盜: 低攻高防，每一回合都會使用綁架封印我方一顆骰子
        // 天生技: 攻擊減1/3
        // 技能1 : 綁架 封印一顆骰子
        int finalCheckValue = 0;
        Character target = stateManager.getCharacter();
        MonsterSkillEffect mse = new MonsterSkillEffect();

        

        if (stateManager.turn % 4 == 0)
        {
            bool isAllEven = true;
            
            for (int i = 0; i < checkValue.Length; i++)
            {
                if (checkValue[i] % 2 != 0)
                {
                    isAllEven = false;
                }

                finalCheckValue += checkValue[i];
            }

            if(isAllEven)
            {
                //大字火
                mse.setDamage(40);
                mse.setSkillActivated(true);
                mse.setUsingSkill(this.skill[2]);
            }
            else
            {
                // 普通
                mse.setDamage(finalCheckValue);
                mse.setSkillActivated(false);
            }
            
        }
        
        else
        {
            bool isAllOdd = true;
            bool isAllSmallerThan3 = true;
            for (int i = 0; i < checkValue.Length; i++)
            {
                if(checkValue[i] % 2 == 0)
                {
                    isAllOdd = false;
                }

                if(checkValue[i] > 3)
                {
                    isAllSmallerThan3 = false;
                }

                finalCheckValue += checkValue[i];
            }

            if(isAllOdd)
            {
                // 封印術
                int k = Random.Range(0, target.diceStates.Length - 1);
                mse.setDisable(k, 2);
                mse.setSkillActivated(true);
                mse.setUsingSkill(this.skill[1]);
            }

            else if (isAllSmallerThan3)
            {
                // 火球術
                mse.setDamage(finalCheckValue + 10);
                mse.setSkillActivated(true);
                mse.setUsingSkill(this.skill[0]);
            }

            else
            {
                // 普通
                mse.setDamage(finalCheckValue);
                mse.setSkillActivated(false);
            }
        }

        target.check(mse);
    }
}
