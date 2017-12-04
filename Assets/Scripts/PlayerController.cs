using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerController : MonoBehaviour {

	public float m_moveSpeed = 1.0f;

	

	public float m_jumpforce = 10.0f;
	public int deviceID;
	private bool m_onGround = false;

	private float m_originJumpForce;

	private Rigidbody m_rb;

	private bool m_stoppedJumping = true;
	private float h;

	public float direction = 1;
	private Vector3 qStartDis;
	private Vector3 qEndDis;
	private float qDis;
	private float qTime = 3f;
	private float qCurrentTime = 0;
	private bool CanMove = true;
	private bool isAttacking = false;
	private Vector3 previousPos;
	


	

	
	

	void Awake(){
		m_originJumpForce = m_jumpforce;
		
	}
	// Use this for initialization
	void Start () {
		
		m_rb = this.GetComponent<Rigidbody>();
		qDis = 3 * direction;
	
	}

	
	// Update is called once per frame
	void Update () {
		
		float moveMag = Mathf.Sqrt(previousPos.sqrMagnitude);
		previousPos = qStartDis - qStartDis;
		Ray Boxer = new Ray(transform.position, transform.right * direction);
		Debug.DrawRay(Boxer.origin, Boxer.direction, Color.red);
		qCurrentTime += Time.deltaTime;
		var player1 = InputManager.Devices[0];
	if(gameObject.CompareTag("P1")){
		if(player1.DPadRight){
		transform.position += new Vector3 (player1.DPadRight / 10, 0);
		direction = 1;
		}
	if(player1.DPadLeft){
		transform.position -= new Vector3(player1.DPadLeft / 10, 0); 
		direction = -1;
	}
			if(player1.DPadUp && m_onGround) {
				
				m_stoppedJumping = false;
				m_rb.AddForce(Vector3.up * m_jumpforce, ForceMode.Impulse);
				Debug.Log("Pressed");
				m_onGround = false;
			}

			if (player1.DPadUp == 0 && !m_stoppedJumping){
				if(m_rb.velocity.y > 0){
					Vector3 velocity = m_rb.velocity;
					velocity.y = 0;
					m_rb.velocity = velocity;

				}
				m_stoppedJumping = true;
				m_jumpforce = m_originJumpForce;
			}
			if(player1.Action3 && CanMove){
				RaycastHit hit;
				isAttacking = true;
				if(Physics.Raycast(Boxer, out hit,3f)){
					hit.rigidbody.AddForce(Vector3.right * direction * 10, ForceMode.Impulse);
					qEndDis = hit.point - (previousPos / moveMag);
				}
				qStartDis = m_rb.transform.position;
				qEndDis = m_rb.transform.position + Vector3.right  * direction *  qDis;

				

				if(qCurrentTime >= 1){
				CanMove = false;
				}
				

				float Perc = qCurrentTime/qTime;
				m_rb.transform.position = Vector3.MoveTowards(qStartDis, qEndDis, qTime);
				
				
				
				Debug.Log("att");
				m_rb.AddForce(Vector3.right * h * 10, ForceMode.Impulse);
			}

			if(!player1.Action3){
				CanMove = true;
				isAttacking = false;
				//qCurrentTime = 0;
			}
		
	}
	
		
			

			
		
			
			
		
		/* 
		if(gameObject.CompareTag("P2")){

			

			if(Input.GetAxis("HorizontalP2") != 0){
				Vector3 pos = gameObject.transform.position;
				pos.x += Input.GetAxis("HorizontalP2") * m_moveSpeed * Time.deltaTime;
				gameObject.transform.position = pos;
			}
			
			if(Input.GetAxis("JumpP2") == 1 && m_onGround) {
				
				m_stoppedJumping = false;
				m_rb.AddForce(Vector3.up * m_jumpforce, ForceMode.Impulse);
				Debug.Log("Pressed");
				m_onGround = false;
			}

			if (Input.GetAxis("JumpP2") == 0 && !m_stoppedJumping){
				if(m_rb.velocity.y > 0){
					Vector3 velocity = m_rb.velocity;
					velocity.y = 0;
					m_rb.velocity = velocity;

				}
				m_stoppedJumping = true;
				m_jumpforce = m_originJumpForce;
			}

		}*/
	}

	
	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag == "Ground"){
			m_onGround = true;
Debug.Log("Ground");
		}else{
			//TODO: check for collsion
			Debug.Log("not ground");
		}
		if(isAttacking == true && other.gameObject.tag == "P2"){
			other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.right * direction * 10, ForceMode.Impulse);
			Debug.Log("ugh!");
		}
	}
}
