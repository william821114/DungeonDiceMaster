using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer : Monster {

    public override void AI(int[] checkValue)
    {
        // 踢: 怪物血量大於50%時，若骰子總和大於 6 ，攻擊加 5
        // 怪物血量小於50 %時，50%機率 : 補血補到滿
        // 發動困獸之鬥: 50% 機率 如果所有骰子都大於2，攻擊最後 *2
        int finalCheckValue = 0;
        int k = Random.Range(0, 15000);
        Character target = stateManager.getCharacter();
        MonsterSkillEffect mse = new MonsterSkillEffect();

        if (this.Hp > this.MaxHp / 2)
        {
            for (int i = 0; i < checkValue.Length; i++)
            {
                finalCheckValue += checkValue[i];
            }

            if (finalCheckValue >= 6)
            {
                finalCheckValue += 5;

                mse.setSkillActivated(true);
                mse.setDamage(finalCheckValue);
                mse.setUsingSkill(this.skill[0]);
                Debug.Log(mse.usingSkill.name + "發動成功! -" + finalCheckValue);
                Debug.Log(mse.usingSkill.description);
            }
        }

        else
        {
            if (k < 7500)
            {
                // heal 回復一半的血

                this.recoverHP(MaxHp / 2);

                mse.setSkillActivated(true);
                mse.setHeal(MaxHp / 2);
                mse.setUsingSkill(this.skill[1]);
                Debug.Log(mse.usingSkill.name + "發動成功! -" + finalCheckValue);
                Debug.Log(mse.usingSkill.description);
            }

            else
            {
                bool isAllBiggerthan2 = true;
                for (int i = 0; i < checkValue.Length; i++)
                {
                    if (checkValue[i] < 2)
                    {
                        isAllBiggerthan2 = false;
                    }
                    finalCheckValue += checkValue[i];
                }

                if (isAllBiggerthan2)
                {
                    finalCheckValue *= 2;
                    mse.setSkillActivated(true);
                    mse.setDamage(finalCheckValue);
                    mse.setUsingSkill(this.skill[1]);
                    Debug.Log(mse.usingSkill.name + "發動成功! -" + finalCheckValue);
                    Debug.Log(mse.usingSkill.description);
                }
            }
        }

        target.check(mse);
    }
}
