using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2_Attack : MonoBehaviour {
	private Collider attack;
	private MeshRenderer attackeffect;



	public bool attackbutton = false;
	public bool damageBoxActive;

	public int frameCounter;
	public int frameActive;
	public int attackcd = 0;

	public int damageOfMove;



	private void Start()
	{
		attack = GetComponent<Collider>();
		attackeffect = GetComponent<MeshRenderer> ();
		attack.enabled = false;
		attackeffect.enabled = false;
	}

	public void initialize_attack (int frames)
	{
		damageBoxActive = true;
		frameCounter = 0;
		frameActive = frames;
	}

	public void Update()
	{
		attackeffect.enabled = attack.enabled;
		if (Input.GetButton ("Con2_Attack") && !attackbutton) {
			attackbutton = true;
			attackcd = 0;

			initialize_attack (10);
		}
		attackcd++;
		if (!Input.GetButton ("Con2_Attack") && attackbutton) {
			if (attackcd > 40) {
				attackbutton = false;
			}
		}

		if (damageBoxActive) {
			frameCounter++;
			if (frameCounter <= frameActive) {
				attack.enabled = true;
			} else {
				attack.enabled = false;
				damageBoxActive = false;
			}
		}
	}

	private void OnTriggerEnter(Collider col)
	{

		if (col.gameObject.layer == 9) {
			Player1Controller P1 = col.gameObject.GetComponent<Player1Controller> ();
			P1.hp -= 25;
			if (P1.hp <= 0) {
				GameObject Cam = GameObject.Find ("Main Camera");
				AgentRespawn NewStat = Cam.gameObject.GetComponent<AgentRespawn> ();
				NewStat.Agent2InHP += 5;
				NewStat.Agent2dmg += 2;
			}


		}
		if (col.gameObject.tag == "Agent1") {
			Agent1Controller A1 = col.gameObject.GetComponent<Agent1Controller> ();
			A1.HP -= 25;
		}

	}
}