using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	public GameObject box;

	GameObject player;

	public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		player = GameObject.FindGameObjectWithTag ("Player");

		float interpolation = speed * Time.deltaTime;

		Vector3 position = this.transform.position;
		position.y = Mathf.Lerp(this.transform.position.y, player.transform.position.y, interpolation);
		position.x = Mathf.Lerp(this.transform.position.x, player.transform.position.x, interpolation);

		this.transform.position = position;
	}
		
	void OnCollisionEnter2D(Collision2D coll){

	}

	public void Dead (){
		Instantiate (box, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}
}
