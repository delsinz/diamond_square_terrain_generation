using UnityEngine;
using System.Collections;

public class Wave : MonoBehaviour {

	Vector3[] vertices;
	Mesh waterMesh;
	float prevTime = 0f;



	void Start()
	{
		waterMesh = GetComponent<MeshFilter>().mesh;
	}



	void Update()
	{
		if (Time.time - prevTime > 0.2f)
		{
			CalculateWaves();
			prevTime = Time.time;
		}
	}



	void CalculateWaves()
	{

		if (vertices == null)
			vertices = waterMesh.vertices;

		Vector3[] waveVertices = new Vector3[vertices.Length];
		for (int i = 0; i < waveVertices.Length; i++)
		{
			Vector3 vertex = vertices[i];
			vertex.y += Random.Range(-0.1f, 0.1f);
			waveVertices[i] = vertex;
		}
		waterMesh.vertices = waveVertices;
		waterMesh.RecalculateNormals();
	}
}
