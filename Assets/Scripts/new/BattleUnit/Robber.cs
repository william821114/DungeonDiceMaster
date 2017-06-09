using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robber : Monster
{

    public override void AI(int[] checkValue)
    {
        // 強盜: 低攻高防，每一回合都會使用綁架封印我方一顆骰子
        // 天生技: 攻擊減1/3
        // 技能1 : 綁架 封印一顆骰子
        int finalCheckValue = 0;
        Character target = stateManager.getCharacter();
        MonsterSkillEffect mse = new MonsterSkillEffect();

        for (int i = 0; i < checkValue.Length; i++)
        {
            finalCheckValue += checkValue[i];
        }

        int k = Random.Range(0, target.diceStates.Length - 1);

        mse.setDisable(k, 1);
        mse.setDamage(finalCheckValue * 2 / 3);
        mse.setSkillActivated(true);
        mse.setUsingSkill(this.skill[0]);

        target.check(mse);
    }
}
