/* COMP30019 Project 1
 * name: Mingyang Zhang (Delsin)
 * login: mingyangz
*/

using UnityEngine;
using System.Collections;

public class Navigation : MonoBehaviour {

	public float rollSpeed = 50;

	public float moveSpeed = 200f;

	public float mouseSensitivity = 10f;




	void Start(){
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}



	private void Update()
	{


		/* Keyboard control */
		if (Input.GetKey ("w")) {
			transform.position += transform.forward * moveSpeed * Time.deltaTime;
		}

		if (Input.GetKey ("s")) {
			transform.position += -transform.forward * moveSpeed * Time.deltaTime;
		}

		if (Input.GetKey ("d")) {
			transform.position += transform.right * moveSpeed * Time.deltaTime;
		}

		if (Input.GetKey ("a")) {
			transform.position += -transform.right * moveSpeed * Time.deltaTime;
		}

		if(Input.GetKey("q"))
		{
			transform.Rotate(0, 0, rollSpeed*Time.deltaTime);
		}

		if(Input.GetKey("e"))
		{
			transform.Rotate(0, 0, -rollSpeed*Time.deltaTime);
		}



		/* Mouse control */
		Vector3 eulerAngles = transform.eulerAngles;
		eulerAngles.x += -Input.GetAxis ("Mouse Y")*mouseSensitivity;
		eulerAngles.y += Input.GetAxis("Mouse X")*mouseSensitivity;
		if (eulerAngles.x < 89 || eulerAngles.x > 271){
			transform.eulerAngles = eulerAngles;
		}

	}

}
