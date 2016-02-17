using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {

	public int hp = 10, sp = 10;

	public int delay = 30;

	void Update()
	{
		if (sp < 10) {
			if (delay > 0)
				delay--;
			else {
				sp++;
				delay = 30;
			}
		}
	}
}
