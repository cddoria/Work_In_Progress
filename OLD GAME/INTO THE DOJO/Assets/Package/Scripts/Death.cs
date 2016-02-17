using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

	void DestroySelf()
	{
		GameObject.Destroy (this.gameObject);
	}
}
