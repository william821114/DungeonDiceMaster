using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DiceMaster;


// 目前還缺受傷的動畫、攻擊的動畫
public class Monster : BattleUnit
{

    public int MaxHp;
    public int MaxMp;
    public int originAtk;
    public int originDef;

    public int Hp;
    public int Mp;
    public int Atk;
    public int Def;

    public Dice[] battleDice;
    public Dice exploreDice;
    public bool willGetHurt = false;

    private bool isDead = false;
    private Animator monsterAnimator;

    public Character actioningCharacter;
    public StateManager stateManager;
    public Animator actioningCharacterAnimator;
    //public TextMesh monsterHurtValueText;


    void Start()
    {
        stateManager = (StateManager)FindObjectOfType(typeof(StateManager));
    }
    /*
    void Start(){
		monsterAnimator = this.GetComponent<Animator> ();

        Hp = MaxHp;
        Atk = originAtk;
        Def = originDef;
    }

	public override void check(int finalCheckValue){
		int damage = finalCheckValue - this.Def;
		Debug.Log (damage);

		if (damage > 0)
			getHurt (damage);
	}

	public void getHurt(int damage){
		if (!isDead) {
			Hp -= damage;
            PlayHurtAnimation();



            if (Hp <= 0) {
				isDead = true;
				PlayDieAnimation ();
                stateManager.setBattleEnd(true);

            }
		}
	}*/

    public void check(int finalCheckValue)
    {
        int damage = finalCheckValue - this.Def;
        if (damage > 0)
        {
            willGetHurt = true;
            Debug.Log("Monster: get Hurt - " + damage);

            //取得 monster底下的 hurtvalue並預先更新數字
            TextMesh HurtValue = this.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMesh>();
            HurtValue.text = "-" + damage.ToString();


            this.Hp -= damage;

            if (this.Hp <= 0) this.Hp = 0;
            //monsterHurtValueText.text = "-" + damage;
        }
    }

    public void AI(int[] checkValue)
    {
        // 怪物血量大於50%時，若骰子總和大於 6 ，攻擊加 5
        // 怪物血量小於50 %時，50%機率 : 補血補到滿
        // 50% 機率 發動困獸之鬥: 如果所有骰子都大於2，攻擊最後 *2
        int finalCheckValue = 0;
        int k = Random.Range(0, 15000);
        Character target = stateManager.getCharacter();

        if (this.Hp > this.MaxHp / 2)
        {
            for (int i = 0; i < checkValue.Length; i++)
            {
                finalCheckValue += checkValue[i];
            }

            if (finalCheckValue >= 6)
            {
                finalCheckValue += 5;
                Debug.Log("猛獸踢發動成功! -" + finalCheckValue);
            }
        }

        else
        {
            if (k < 7500)
            {
                // heal
                Debug.Log("Monster Heal");
            }

            else
            {
                bool isAllBiggerthan2 = true;
                for (int i = 0; i < checkValue.Length; i++)
                {
                    if (finalCheckValue < 2)
                    {
                        isAllBiggerthan2 = false;
                    }
                    finalCheckValue += checkValue[i];
                }

                if (isAllBiggerthan2)
                {
                    finalCheckValue *= 2;
                    Debug.Log("困獸之鬥發動成功! -" + finalCheckValue);
                }
            }
        }

        target.check(finalCheckValue);


    }

    public Dice[] getBattleDice()
    {
        return battleDice;
    }

    public Dice getExploreDice()
    {
        return exploreDice;
    }

    /*
	// 播死亡的fade out動畫
	private void PlayDieAnimation(){
		monsterAnimator.SetTrigger ("Die");
	} 

    private void PlayHurtAnimation()
    {
        monsterAnimator.SetTrigger("Hurt");
    }

	// fade out的最後一個frame會呼叫這個，來清掉物件
	private void cleanDeadBody(){
		Destroy (this.gameObject);
	}

    */

    public void damageMp(int value)
    {
        this.Mp = (Mp - value > 0) ? (Mp - value) : 0;
    }
}
