using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1_Attack : MonoBehaviour {
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

		if (Input.GetButton ("Con1_Attack") && !attackbutton) {
			attackbutton = true;
			attackcd = 0;

			initialize_attack (5);
		}
		attackcd++;
		if (!Input.GetButton ("Con1_Attack") && attackbutton) {
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

		if (col.gameObject.layer == 10) {
			Player2Controller P2 = col.gameObject.GetComponent<Player2Controller> ();
			P2.hp -= 25;

			// new minion stats
			if (P2.hp <= 0) {
				GameObject Cam = GameObject.Find ("Main Camera");
				AgentRespawn NewStat = Cam.gameObject.GetComponent<AgentRespawn> ();
				NewStat.Agent1InHP += 5;
				NewStat.Agent1dmg += 2;


			
			}
		}
		if (col.gameObject.tag == "Agent2") {
			Agent2Controller A2 = col.gameObject.GetComponent<Agent2Controller> ();
			A2.HP -= 25;
		}
 
	}
}

