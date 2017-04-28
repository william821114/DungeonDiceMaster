using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 所有需要檢定的東西都是barrier，包括敵人怪物、交流的NPC對象、欲開啟的寶箱、要解除的陷阱等等。
public class Barrier : MonoBehaviour {

	public int targetValue; // 判定值，超過此值才算成功

	public virtual void check(int finalCheckValue){
		Debug.Log ("check Taget");
	}
}
