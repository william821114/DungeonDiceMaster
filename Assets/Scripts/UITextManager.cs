using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DiceMaster;

public class UITextManager : MonoBehaviour {

	public Animator _animator;
	public Text information;
	public Text title;
	public Image icon;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void showText(string text){
		this.gameObject.SetActive (true);
		information.text = text;
		_animator.SetTrigger ("ShowPanel");
	}

	public void showLootDetail(int lootType, Sprite image){
		this.gameObject.SetActive (true);
		this.icon.sprite = image;

		switch (lootType) {
		case 0:
			title.text = "生命藥水";
			information.text = "此道具可以恢復角色10點生命值。\n（拖移至角色圖像上使用）";
			break;
		case 1:
			title.text = "魔法藥水";
			information.text = "此道具可以恢復角色3點魔力值。\n（拖移至角色圖像上使用）";
			break;
		default:
			Debug.Log ("UITextManager - showLootDetail error");
			break;
		}

		_animator.SetTrigger ("ShowPanel");
	}

	public void showGambleSkillDetail(int skillType, Sprite image){
		this.gameObject.SetActive (true);
		this.icon.sprite = image;

		switch (skillType) {
		case 0:
			title.text = "盜高一尺";
			information.text = "盜取對方一顆骰子。\n\n下個回合，\n對方少擲一顆骰子，\n你多擲一顆骰子。";
			break;
		case 1:
			title.text = "投影";
			information.text = "所有數值未達3的骰子，\n全部轉變成3。";
			break;
		case 2:
			title.text = "口袋夾層";
			information.text = "多擲一顆六面骰，\n所得數值與前次擲骰的結果相加。";
			break;
		case 3:
			title.text = "鎖定";
			information.text = "選擇一個骰子保留其數值，\n重擲其他骰子。";
			break;
		case 4:
			title.text = "反轉";
			information.text = "數值為6的骰子，全部轉變成9。";
			break;
		case 5:
			title.text = "時光倒轉";
			information.text = "重擲所有骰子。";
			break;
		default:
			Debug.Log ("UITextManager - showLootDetail error");
			break;
		}

		_animator.SetTrigger ("ShowPanel");
	}

	public void showDiceDetail(Dice dice){
		
	}

	public void showBattleSkillDetail(string character, int skillType, Sprite image){
		this.gameObject.SetActive (true);
		this.icon.sprite = image;

		if (character == "Knight") {
			switch (skillType) {
			case 0:
				title.text = "迴旋斬";
				information.text = "若所有骰子點數大於 2，\n攻擊點數 +4";
				break;
			case 1:
				title.text = "噬血一擊";
				information.text = "若所有骰子點數加總大於 8， \n攻擊點數加倍 & 回復 2 HP";
				break;
			case 2:
				title.text = "疾風斬";
				information.text = "若骰子點數加總小於等於 4， \n迴避一回合傷害";
				break;
			default:
				Debug.Log ("UITextManager - showBattleSkillDetail error");
				break;
			}
		} else if (character == "Thief") {
		} else if (character == "Priest") {
		}

		_animator.SetTrigger ("ShowPanel");
	}

	public void showMonsterSkillDetail(string monster, int skillType, Sprite image){
		this.gameObject.SetActive (true);
		this.icon.sprite = image;

		if (monster == "Deer") {
			switch (skillType) {
			case 0:
				title.text = "犄角攻擊";
				information.text = "當 HP 大於 50% 以上時, \n若骰子總和大於 6 ，攻擊加 5";
				break;
			case 1:
				title.text = "治癒";
				information.text = "怪物血量小於 50% 時，\n50% 機率 ，回復 1/2 MaxHp";
				break;
			case 2:
				title.text = "困獸之鬥";
				information.text = "怪物血量小於 50% 時，\n50% 機率 如果所有骰子都大於 2，\n攻擊最後加倍";
				break;
			default:
				Debug.Log ("UITextManager - showBattleSkillDetail error");
				break;
			}
		} else if (monster == "Rogue") {
			switch (skillType) {
			case 0:
				title.text = "肉盾";
				information.text = "每一回合有 2/3 機率\n使用 綁架 封印我方一顆骰子";
				break;
			case 1:
				title.text = "搔癢";
				information.text = "天生攻擊力減少 1/3";
				break;
			case 2:
				title.text = "綁架";
				information.text = "隨機封印一顆骰子";
				break;
			default:
				Debug.Log ("UITextManager - showBattleSkillDetail error");
				break;
			}
		} else if (monster == "FireMagician") {
			switch (skillType) {
			case 0:
				title.text = "火球術";
				information.text = "若骰子全部小於等於 5，\n則最後傷害 + 5";
				break;
			case 1:
				title.text = "封印術";
				information.text = "若全部骰子為奇數，\n封印隨機一顆骰子 2 回合";
				break;
			case 2:
				title.text = "野火燎原";
				information.text = "每 3 倍數回合必用，\n發動強大的魔法，\n造成 40 傷害";
				break;
			default:
				Debug.Log ("UITextManager - showBattleSkillDetail error");
				break;
			}
		}

		_animator.SetTrigger ("ShowPanel");
	}
	
	public void hide(){
		this.gameObject.SetActive (false);
	}
}
