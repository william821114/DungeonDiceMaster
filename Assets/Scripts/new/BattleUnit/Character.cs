using System.Collections;
using System.Collections.Generic;
using DiceMaster;
using UnityEngine;

// 缺動畫(攻擊、死亡、受傷害)
public class Character : BattleUnit {

    public int MaxHp;
    public int MaxMp;
    public int originAtk;
    public int originDef;

	public int Hp;
	public int Mp;
	public int Atk;
	public int Def;
	public Dice[] battleDice;
	public Dice socialDice;
	public Dice exploreDice;
	public Skill[] skill;


    public StateManager stateManager;

	public Sprite characterPiece;
	public Sprite characterHalf;
	public Sprite characterFull;

	public Sprite[] battleSkillOn;
	public Sprite[] battleSkillOff;

	private bool isDead = false;


	void Awake() {
		DontDestroyOnLoad(this.gameObject);
	}
	/*
    void Start()
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
	*/

	public Dice[] getBattleDice(){
		return battleDice;
	}

	public Dice getExploreDice(){
		return exploreDice;
	}

	public Dice getSocialDice(){
		return socialDice;
	}

	/*
    public void PlayerTurn()
    {
        if (isDead)
            stateManager.setBattleEnd(false);
        else
        {
            if (!stateManager.getState().Equals(State.BattleState.BattleEnd))
            {
                stateManager.SendMessage("setTurn", State.BattleState.PlayerTurn);
            }
        }
    }

    public void onStateChange(State.BattleState state)
    {
        if(state.Equals(State.BattleState.PlayerTurn)) {
            Debug.Log("Alice : It's my turn!");
            stateManager.prepareForPlayerTurn();
        }
    }*/
}
