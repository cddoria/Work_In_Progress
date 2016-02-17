using UnityEngine;
using System.Collections;

public class SetSFXSelector : MonoBehaviour {

	public SfxScript sfx;

	void setSelector(int sel)
	{
		sfx.sfxSelector = sel;
	}
}
