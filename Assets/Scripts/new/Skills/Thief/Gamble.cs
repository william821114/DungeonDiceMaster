using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamble: Skill {

	public override int skillWeight(int[] checkValue){
        finalCheckValue = 0;
        for (int i = 0; i < checkValue.Length; i++)
        {
            finalCheckValue += checkValue[i];
        }

        if (finalCheckValue >= 12)
        {
            finalCheckValue *= 2;
            Debug.Log("賭術發動成功! -" + finalCheckValue);
        } else
        {
            finalCheckValue = 0;
        }

        return finalCheckValue;
	}
}