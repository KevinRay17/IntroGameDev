﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent2Controller : MonoBehaviour {

		private Rigidbody agent2;
		private int hp = 10;
		private int dmg = 2;
		public Ray myRay;
		public RaycastHit myRCH;
		public int attacktimer = 45;

		int layerMask = 9 | 11;

		
		// Use this for initialization
		void Start () {
			agent2 = this.GetComponent <Rigidbody>();
			agent2.velocity = new Vector3 (-4, 0, 0);
			attacktimer = 45;

		}
	public int HP {
		get {
			return hp;
		}
		set {
			hp = value;
		}
	}
	public int DMG {
		get {
			return dmg;
		}
		set {
			dmg = value;
		}
	}
		// Update is called once per frame
		void Update () {

		//Stop if you see another AI within range and start moving again when they are gone
		myRay = new Ray (transform.position, Vector3.left);
		Physics.Raycast (myRay.origin, myRay.direction, out myRCH, 2.5f, layerMask);
		if (myRCH.collider != null) {
			if (myRCH.collider.tag == "Agent1" || myRCH.collider.tag == "Agent2") {
				agent2.velocity = new Vector3 (0, 0, 0);
			} else {
				agent2.velocity = new Vector3 (-4, 0, 0);
			} 
			//Attack
			if (myRCH.collider.tag == "Agent1") {
				attacktimer--;
				if (attacktimer <= 0) {
					Agent1Controller AI1 = myRCH.collider.gameObject.GetComponent<Agent1Controller> ();
					AI1.HP -= dmg;
					attacktimer = 45;
				}
			} else {
				attacktimer = 45;
			}


		} else {
			agent2.velocity = new Vector3 (-4, 0, 0);
		}
		if (hp <= 0) {
			AgentRespawn AR = GameObject.Find ("Main Camera").GetComponent<AgentRespawn>();
			AR.DeleteAgent (gameObject);
		}
		}
	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "CastleL") {
			GameObject Cam = GameObject.Find ("Main Camera");
			AgentRespawn CastleHP = Cam.gameObject.GetComponent<AgentRespawn> ();
			CastleHP.CastleL -= 1; 
			AgentRespawn AR = GameObject.Find ("Main Camera").GetComponent<AgentRespawn>();
			AR.DeleteAgent (gameObject);
		}
	}

}