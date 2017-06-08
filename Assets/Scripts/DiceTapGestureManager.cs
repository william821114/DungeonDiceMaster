using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;
using DiceMaster;

public class DiceTapGestureManager : MonoBehaviour {
	public TapGesture singleTap;
	public bool tapable = false;
	public GameObject selectIcon;

	private BattleCheckManager bcm;
	private GameObject icon;

	// Use this for initialization
	void Start () {


		singleTap.Tapped += (object sender, System.EventArgs e) => 
		{
			if(tapable)
			{
				Quaternion rotationTemp = Quaternion.identity;
				rotationTemp.eulerAngles = new Vector3(90f,0f,0f);

				icon = GameObject.Instantiate (selectIcon, Vector3.zero, rotationTemp) as GameObject;
				icon.transform.parent = this.gameObject.transform;
				icon.transform.localPosition = Vector3.zero;

				tapable = false;

				Rigidbody rigidbodyTemp = this.GetComponent<Rigidbody> ();
				rigidbodyTemp.constraints = RigidbodyConstraints.FreezeAll;

				bcm = (BattleCheckManager)FindObjectOfType (typeof(BattleCheckManager));
				bcm.lockSelectDice(true, this.GetComponent<Dice>().value);
			}
		};
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
