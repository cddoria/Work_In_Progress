using UnityEngine;
using System.Collections;

public class DamagePlayer : MonoBehaviour {

	public GameObject player;
	public int damage;
	private HealthScript hs;
	private Transform player_trans;
	private Transform enemy_trans;
	public string deathScreenName;

	void Start()
	{
		hs = player.GetComponent<HealthScript>();
		player_trans = player.GetComponent<Transform> ();
		enemy_trans = this.gameObject.GetComponent<Transform>();
	}

	void dealDamageToPlayer()
	{
		if((player_trans.position - enemy_trans.position).magnitude < 2.0)
			hs.hp -= damage;
		if (hs.hp <= 0)
			Application.LoadLevel (deathScreenName);
	}
}