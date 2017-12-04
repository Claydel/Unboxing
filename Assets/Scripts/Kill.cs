using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour {
public Transform destruct;
void OnTriggerEnter(Collider other){
	Debug.Log("Just touched: " + other.name);
	if (other.gameObject != null){
	Destroy(other.gameObject);
	other = null;
	}else{
	return;
	}
	if (transform.position.x > destruct.position.x){
	
	Debug.Log("destroyed");
}
}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
