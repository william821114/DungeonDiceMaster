using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Excalibur : Skill {
	
	private bool success;

    public override int skillWeight(int[] checkValue){
		// 如果所有值皆大於等於3，則總值再加3

		for (int i = 0; i < checkValue.Length; i++) {
			if (checkValue[i] < 3)
				success = false;

			finalCheckValue += checkValue [i];
		}

		if (success)
			finalCheckValue += 3;
		
		return finalCheckValue;
	}
}
