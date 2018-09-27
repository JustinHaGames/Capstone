using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {

	public Transform player; 

	public static int row = 0; 
	public static int column = 0; 

	private Vector2[,] roomPosition = new Vector2[3,3];
	Vector2 roomSize = new Vector2(20,12);

	// Use this for initialization
	void Start () {

		//This forloop creates all of the positions that the roomManager object should be in
		for (int x = 0; x < roomPosition.GetLength (0); x++) {
			for (int y = 0; y < roomPosition.GetLength (1); y++) {
				roomPosition [x, y] = new Vector2 (x * roomSize.x, y * roomSize.y);
			}
		}
	}

	// Update is called once per frame
	void Update () {

		//Get the distance between the player and the room position
		float xDistance = Mathf.Abs (player.position.x - roomPosition [row, column].x);
		float yDistance = Mathf.Abs (player.position.y - roomPosition [row, column].y);

		//If the distance between the player and roomPosition is greater than half of the room
		//the player is leaving the room
		if (xDistance >= (roomSize.x * 0.5f)) {

			//Mathf.Sign changes the distance between the player and the roomPosition into either -1 or 1 (left or right)
			//then we add that to the row to change the room we're in 
			row += (int)Mathf.Sign (player.position.x - roomPosition [row, column].x); 
			Debug.Log (row);

			row = Mathf.Clamp (row,0,roomPosition.GetLength(0));

			changeRoom ();

		}
		else if (yDistance >= (roomSize.y * 0.5f)) {
			column += (int)Mathf.Sign (player.position.y - roomPosition [row, column].y); 
			column = Mathf.Clamp (column,0,roomPosition.GetLength(1));
			changeRoom ();
		}
			

	}

	//This method is called when we want to change the room 
	void changeRoom (){
		transform.position = roomPosition [row, column];
	}
}
