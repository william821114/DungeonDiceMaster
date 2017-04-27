// Copyright Michele Pirovano 2014-2016
using UnityEngine;
using DiceMaster;

/// <summary>
/// Rolls a dice by triggering a Thrower and/or a Spinner when the R key, a mouse button, or a touch is detected.
/// </summary>
public class Roller : MonoBehaviour
{

    Spinner spinner;
    Thrower thrower;
	private bool rollable = true;

    void Start()
    {
        spinner = GetComponent<Spinner>();
        thrower = GetComponent<Thrower>();

        if (spinner)
            spinner.autoDestroy = false;
        if (thrower)
            thrower.autoDestroy = false;
    }

    void Update()
    {
		// rollable是我自己新增的，為了讓骰子只能擲一次。
		if (rollable) {
			if (Input.GetKeyDown (KeyCode.R) || Input.GetMouseButtonDown (0)) {
				//  檢查點擊範圍也是自己新增的。
				if (Input.mousePosition.x > 0 && Input.mousePosition.x< 750 && Input.mousePosition.y > 20 && Input.mousePosition.y < 570)
				{
					if (thrower)
						thrower.Trigger ();
					if (spinner)
						spinner.Trigger ();
					rollable = false;
				}
			}
		}
    }
}
