﻿using System.Collections;
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
    public int noHurtTurn; //幾回合迴避傷害
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
    public bool willGetHurt = false;

	private Animator _animator;

	void Awake(){
		DontDestroyOnLoad (this);

		//if (characterInstance == null) {
		//	characterInstance = this;
		//} else {
		//	DestroyObject(gameObject);
		//}

		_animator = this.GetComponent<Animator> ();
	}

	public Dice[] getBattleDice(){
		return battleDice;
	}

	public Dice getExploreDice(){
		return exploreDice;
	}

	public Dice getSocialDice(){
		return socialDice;
	}

    public void check(int finalCheckValue)
    {
        // 檢查迴避勝幾回合 ， 0 的話可攻擊
        if(noHurtTurn > 0)
        {
            noHurtTurn -= 1;
            Debug.Log("迴避傷害 剩餘次數:" + noHurtTurn);
        }
        else {
            int damage = finalCheckValue - this.Def;
            if (damage > 0)
            {
                willGetHurt = true;
                Debug.Log("Character: get Hurt - " + damage);
                this.Hp -= damage;
            }
        }   
    }

    public void recoverHP(int value)
    {
        if (!isDead)
        {
            Hp = (Hp + value) >= MaxHp ? MaxHp : (Hp + value);
        }
    }

    public void recoverMP(int value)
    {
        Mp = (Mp + value) >= MaxMp ? MaxMp : (Mp + value);
    }
}
