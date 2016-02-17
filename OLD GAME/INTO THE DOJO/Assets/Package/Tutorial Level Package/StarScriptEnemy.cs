using UnityEngine;
using System.Collections;

public class StarScriptEnemy : MonoBehaviour {

    public GameObject Enemy;
    public LoadNextLevel NextLevelScript;
	public SfxScript sfx;

	void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.tag == "NinjaStar")
        {
            HealthScript hs = this.gameObject.GetComponentInParent<HealthScript>();
            if (hs.hp > 3)
            {
                hs.hp -= 3;
				sfx.sfxSelector = 0;
            }
            else
            {
                NextLevelScript.EnemyList.Remove(Enemy);
				Enemy.GetComponent<Animator>().SetBool("dead", true);
            }
			GameObject.Destroy(obj.gameObject);
        }
    }
}
