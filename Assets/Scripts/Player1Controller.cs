﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour {	
	public int hp = 100;


	public float speed = 50.0f;
	public float jumpVel = 24.0f;
	public float wallJumpVel = 24.0f;
	public float wallJumpPercent = 0.0f;
	public float wallJumpDecay = 1.0f;
	public float wallJumpDir = 0;

	public bool wallJump = false;
	private bool jumpbutton = false;
	private bool  hasAirJumped = false;
	public bool isGrounded = false;
	public bool touchopp = false;

	private Rigidbody playerrb;
	// Use this for initialization
	void Start () {
		Physics.gravity = new Vector3 (0f, -75.0f, 0f);
		playerrb = this.GetComponent <Rigidbody>();
	}

	// Update is called once per frame
	void Update () {

		GameObject opponent = GameObject.Find ("Player 2");
		if (opponent == null && touchopp == true) {
			wallJump = false;
			touchopp = false;
		}
		//Movement and Jumping
		float verticalVel = playerrb.velocity.y;

		if (Input.GetButton ("Con1_Jump") && !jumpbutton) {
			jumpbutton = true;        

			if (isGrounded) {
				verticalVel = jumpVel;
			} else if (!hasAirJumped) {
				verticalVel = jumpVel;
				hasAirJumped = true;
			}
				
			if (wallJump) {
				wallJumpPercent = 1.0f;
			}
		}
			
		if (!Input.GetButton ("Con1_Jump") && jumpbutton) {
			jumpbutton = false;        
		}

		float strafe = Input.GetAxis ("Con1_Horizontal") * speed;
		float moveHorizontal = Input.GetAxisRaw ("Con1_Horizontal");

		if (moveHorizontal > 0)
			transform.rotation = Quaternion.FromToRotation (Vector3.right, new Vector3 (1, 0, 0));
		else if (moveHorizontal < 0)
			transform.rotation = Quaternion.FromToRotation (Vector3.right, new Vector3 (-1, 0, 0));
			

		float xMovement = strafe * (1.0f - wallJumpPercent) + wallJumpVel * wallJumpDir * wallJumpPercent;


		if (wallJumpPercent > 0) {
			wallJumpPercent = Mathf.Max (0, wallJumpPercent - wallJumpDecay * Time.deltaTime);
		}
			
		bool overlap = false;
		Collider[] hitColliders = Physics.OverlapSphere (playerrb.position, .75f);
		foreach (Collider c in hitColliders) {
			if (c.gameObject.layer == 8) {
				overlap = true;
			}
		}

		if (playerrb.velocity.y > 0) {
			Physics.IgnoreLayerCollision (8, 9, true);
		} else if (!overlap) {                         
			Physics.IgnoreLayerCollision (8, 9, false);
		}
		
		playerrb.velocity = new Vector3(xMovement, verticalVel ,0);




		if (hp <= 0) {
			Destroy (gameObject);


		}
	}


	//More Jump Stuff
	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Floor" || col.gameObject.tag == "Wall" || col.gameObject.tag == "Player") {
			isGrounded = true;
			hasAirJumped = false;
		}
		if (col.gameObject.tag == "Wall" || col.gameObject.tag == "Player") {
			ContactPoint contact = col.contacts [0];
			wallJumpDir = (int)contact.normal.x;        
			wallJump = true;
		} 
		if (col.gameObject.tag == "Player") {
			touchopp = true;
		}
	}


	void OnCollisionExit(Collision col){
		if (col.gameObject.tag == "Floor" || col.gameObject.tag == "Wall" || col.gameObject.tag == "Player") {
			isGrounded = false;
		}
		if (col.gameObject.tag == "Wall" || col.gameObject.tag == "Player") {
			wallJump = false;
		}
		if (col.gameObject.tag == "Player") {
			touchopp = false;
		}
	}
}

