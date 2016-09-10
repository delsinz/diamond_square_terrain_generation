/* COMP30019 Project 1
 * name: Mingyang Zhang (Delsin)
 * login: mingyangz
*/

using UnityEngine;
using System.Collections;

public class LightDirection : MonoBehaviour {
	public float orbitSpeed = 50;

	void Update () {
		//this.transform.Rotate(Vector3.right * Time.deltaTime * orbitSpeed, Space.Self);
		this.transform.RotateAround(Vector3.zero, Vector3.right, Time.deltaTime * orbitSpeed);
	}
}
