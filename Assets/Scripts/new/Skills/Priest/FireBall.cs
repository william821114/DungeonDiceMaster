using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall: Skill {

	public override int skillWeight(int[] checkValue){
        finalCheckValue = 0;
        for (int i = 0; i < checkValue.Length; i++)
        {
            finalCheckValue += checkValue[i];
        }

        if(finalCheckValue >= 8)
        {
            finalCheckValue += 5;
            Debug.Log("火球發動成功! - " + finalCheckValue);
        }

        return finalCheckValue;
	}
}