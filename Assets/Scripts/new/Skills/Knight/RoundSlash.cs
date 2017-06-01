using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundSlash : Skill {

	public override int skillWeight(int[] checkValue){
        bool isTrigger = true;
        finalCheckValue = 0;
        for (int i = 0; i < checkValue.Length; i++)
        {
            finalCheckValue += checkValue[i];
            if (checkValue[i] < 2) isTrigger = false;
        }

        if (isTrigger)
        {
            finalCheckValue += 4;
            Debug.Log("迴旋斬發動成功! - " + finalCheckValue);
        }

        return finalCheckValue;
	}
}