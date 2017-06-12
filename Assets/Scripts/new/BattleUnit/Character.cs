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
    public int noHurtTurn; //幾回合迴避傷害
	public Dice[] battleDice;
    public DiceState[] diceStates;
    public Dice socialDice;
	public Dice exploreDice;
	public Skill[] skill;


    public StateManager stateManager;
    public BattleCheckManager bcManager;

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

        bcManager = (BattleCheckManager)FindObjectOfType(typeof(BattleCheckManager));

        diceStates = new DiceState[battleDice.Length];
        for (int i = 0; i < battleDice.Length; i++)
        {
            diceStates[i] = new DiceState();
        }

        //if (characterInstance == null) {
        //	characterInstance = this;
        //} else {
        //	DestroyObject(gameObject);
        //}

        _animator = this.GetComponent<Animator> ();
	}

	public Dice[] getBattleDice(){
        List<Dice> usableBattleDices = new List<Dice>();
        for(int i = 0; i < battleDice.Length; i ++)
        {
            if(diceStates[i].disableTurn == 0)
            {
                usableBattleDices.Add(battleDice[i]);
            }
        }

		return usableBattleDices.ToArray();
	}

    public Dice[] getDisableDice()
    {
        List<Dice> disableDices = new List<Dice>();
        for (int i = 0; i < battleDice.Length; i++)
        {
            if (diceStates[i].disableTurn > 0)
            {
                disableDices.Add(battleDice[i]);
            }
        }
        return disableDices.ToArray();
    }

	public Dice getExploreDice(){
		return exploreDice;
	}

	public Dice getSocialDice(){
		return socialDice;
	}

    public void check(int finalCheckValue)
    {
        TextMesh HurtValue = this.gameObject.transform.GetChild(1).gameObject.GetComponent<TextMesh>();
        HurtValue.text = "-0";
        // 檢查迴避勝幾回合 ， 0 的話可攻擊
        if (noHurtTurn > 0)
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
                //取得 player hurtvalue並預先更新數字
                HurtValue.text = "-" + damage.ToString();

                this.Hp -= damage;
            }
        }   
    }

    public void check(MonsterSkillEffect mse)
    {
        Debug.Log(mse.isDamage);
        if (mse.isSkillActivated)
        {
            Debug.Log("怪獸技能 " + mse.usingSkill.name + " 發動成功!");
        }

        if (mse.isDamage)
        {
            if (noHurtTurn > 0)
            {
                noHurtTurn -= 1;
                Debug.Log("迴避傷害 剩餘次數:" + noHurtTurn);
            }
            else
            {
                int damage = mse.damage - this.Def;
                if (damage > 0)
                {
                    willGetHurt = true;
                    Debug.Log("Character: get Hurt - " + damage);
                    //取得 player hurtvalue並預先更新數字
                    TextMesh HurtValue = this.gameObject.transform.GetChild(1).gameObject.GetComponent<TextMesh>();
                    HurtValue.text = "-" + damage.ToString();

                    this.Hp -= damage;
                }
            }
        }

        if(mse.isDisable)
        {
            this.diceStates[mse.disable].addDisableTurn(mse.disableTurns);

            for(int i = 0; i< this.diceStates.Length; i ++)
            {
                Debug.Log(this.diceStates[i].disableTurn);
            }
        }
    }

    public void recoverHP(int value)
    {
        if (!isDead)
        {
            Hp = (Hp + value) >= MaxHp ? MaxHp : (Hp + value);

            TextMesh HealValue = this.gameObject.transform.GetChild(2).gameObject.GetComponent<TextMesh>();
            HealValue.text = "+" + value.ToString();
        }
    }

    public void recoverMP(int value)
    {
        Mp = (Mp + value) >= MaxMp ? MaxMp : (Mp + value);
    }

    public void turnEnd()
    {
        for(int i = 0; i < this.diceStates.Length; i ++)
        {
            this.diceStates[i].decreaseDisableTurn();
        }
    }
}
