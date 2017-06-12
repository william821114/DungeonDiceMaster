using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipBarManager : MonoBehaviour {
	public Text tip;
	private Animator _animator;


	// Use this for initialization
	void Start () {
		_animator = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void showTips(){
		tip.text = "快速撥劃來投擲骰子";
		_animator.SetTrigger ("ShowTipBar");
	}

	public void hideTips(){
		_animator.SetTrigger ("HideTipBar");
	}

	public void showGambleSkillTip(int skillType){
		switch (skillType) {
		case 0:
			tip.text = "你成功扒走對方一顆骰子一回合！";
			break;
		case 1:
			tip.text = "數值小於3的骰子都變成3了！";
			break;
		case 2:
			tip.text = "請擲一顆六面骰";
			break;
		case 3:
			tip.text = "點擊欲保留點數的骰子，\n接著快速撥劃來重擲其他骰子";
			break;
		case 4:
			tip.text = "數值為6的骰子都變成9了！";
			break;
		case 5:
			tip.text = "快速撥劃下畫面來重擲骰子";
			break;
		default:
			Debug.Log ("UITextManager - showLootDetail error");
			break;
		}

		_animator.SetTrigger ("ShowTipBar");
	}
}
