using System.Collections;
using System.Collections.Generic;
using DiceMaster;
using UnityEngine;

// 缺動畫(攻擊、死亡、受傷害)
public class Character : MonoBehaviour {

    public int MaxHp;
    public int MaxMp;
    public int originAtk;
    public int originDef;

	public int Hp;
	public int Mp;
	public int Atk;
	public int Def;
	public Dice[] battleDice;
	public Dice[] socialDice;
	public Dice[] exploreDice;
	public Skill[] skill;

    public StateManager stateManager;

	private bool isDead = false;

    private void Start()
    {
        Hp = MaxHp;
        Mp = MaxMp;
        Atk = originAtk;
        Def = originDef;
    }

    // 呼叫此function來扣角色HP
    public void getHurt(int damage)
	{
        Debug.Log("Alice: Ouch! " + damage);
		if (!isDead) {
			if (damage - Def > 0)
            {
                Hp = Hp - (damage - Def);
                Debug.Log("Alice: My Hp:" + Hp);
            }

            if (Hp <= 0)
            {
                isDead = true;
                stateManager.setBattleEnd(false);
            }
				
		}
	}

    // call this function to heal hp
    public void getHeal(int value)
    {
        if (!isDead)
        {
            if(Hp + value >= MaxHp)
            {
                Hp = MaxHp;
            }
            else
            {
                Hp += value;
            }
        }
    }

	// 呼叫此function來取得這個角色裝備的骰子
	public Dice[] getDice(int checkEvent){
		// battle event = 0
		// social event = 1
		// explore event = 2

		if (checkEvent == 0) {
			return battleDice;
		} else if (checkEvent == 1) {
			return socialDice;
		} else if (checkEvent == 2) {
			return exploreDice;
		} else
			return null;
	}

    public void onStateChange(State.BattleState state)
    {
        if(state.Equals(State.BattleState.PlayerTurn)) {
            Debug.Log("Alice : It's my turn!");
        }
    }
}
