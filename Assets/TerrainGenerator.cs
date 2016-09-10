/* COMP30019 Project 1
 * name: Mingyang Zhang (Delsin)
 * login: mingyangz
*/

using UnityEngine;
using System.Collections;

public class TerrainGenerator : MonoBehaviour {

	float[,] heightMap;
	TerrainData terrainData;
	int terrainSize;
	int maxIndex;
	System.Random rngesus;



	void Start(){
		terrainData = GetComponent<Terrain> ().terrainData;
		terrainSize = terrainData.heightmapResolution;
		maxIndex = terrainSize - 1;

		heightMap = new float[terrainSize,terrainSize];
		for (int x = 0; x < terrainSize; x++) {
			for (int y = 0; y < terrainSize; y++) {
				heightMap[x,y] = 0.5f;
			}
		}

		GenerateTerrain ();

	}



	void Update(){
		if(Input.GetKey("r"))
		{
			this.GenerateTerrain ();
		}
	}



	private void GenerateTerrain(){
		rngesus = new System.Random();
		DiamondSquare (maxIndex);
		terrainData.SetHeights (0, 0, GaussianBlur());
		MapTexture ();
	}


	private void MapTexture(){
		float[,,] alphaMap = new float[terrainData.alphamapWidth,terrainData.alphamapHeight,5];
		for (int y = 0; y < terrainData.alphamapHeight; y++) {
			for (int x = 0; x < terrainData.alphamapWidth; x++) {
				if (heightMap [x, y] <= 0.4f) {
					alphaMap [x, y, 0] = 1f;
				} else if (heightMap [x, y] <= 0.425f) {
					alphaMap [x, y, 0] = 0.5f;
					alphaMap [x, y, 1] = 0.5f;
				} else if (heightMap [x, y] <= 0.475f) {
					alphaMap [x, y, 1] = 1f;
				} else if (heightMap [x, y] <= 0.525f) {
					alphaMap [x, y, 1] = 0.5f;
					alphaMap [x, y, 2] = 0.5f;
				} else if (heightMap [x, y] <= 0.575f) {
					alphaMap [x, y, 2] = 1f;
				} else if (heightMap [x, y] <= 0.625f) {
					alphaMap [x, y, 2] = 0.5f;
					alphaMap [x, y, 3] = 0.5f;
				} else if (heightMap [x, y] < 0.675f) {
					alphaMap [x, y, 3] = 1f;
				} else if (heightMap [x, y] <= 0.725f) {
					alphaMap [x, y, 3] = 0.5f;
					alphaMap [x, y, 4] = 0.5f;
				} else {
					alphaMap [x, y, 4] = 1f;
				}

//				if (heightMap [x, y] <= 0.4f) {
//					alphaMap [x, y, 0] = 1f;
//				} else if (heightMap [x, y] <= 0.425f) {
//					alphaMap [x, y, 0] = (0.425f-heightMap[x,y])/0.025f;
//					alphaMap [x, y, 1] = 1-(0.425f-heightMap[x,y])/0.025f;
//				} else if (heightMap [x, y] <= 0.5f) {
//					alphaMap [x, y, 1] = 1f;
//				} else if (heightMap [x, y] <= 0.525f) {
//					alphaMap [x, y, 1] = (0.525f-heightMap[x,y])/0.025f;
//					alphaMap [x, y, 2] = 1f - (0.525f-heightMap[x,y])/0.025f;
//				} else if (heightMap [x, y] <= 0.6f) {
//					alphaMap [x, y, 2] = 1f;
//				} else if (heightMap [x, y] <= 0.625f) {
//					alphaMap [x, y, 2] = (0.625f-heightMap[x,y])/0.025f;
//					alphaMap [x, y, 3] = 1f - (0.625f-heightMap[x,y])/0.025f;
//				} else if (heightMap [x, y] <= 0.7f) {
//					alphaMap [x, y, 3] = 1f;
//				} else if (heightMap [x, y] <= 0.725f) {
//					alphaMap [x, y, 3] = (0.725f-heightMap[x,y])/0.025f;
//					alphaMap [x, y, 4] = 1f - (0.725f-heightMap[x,y])/0.025f;
//				} else {
//					alphaMap [x, y, 4] = 1f;
//				}

			}
		}
		terrainData.SetAlphamaps (0, 0, alphaMap);
	}



	private void DiamondSquare(int stepSize){
		int half = stepSize / 2;
		if (half < 1) {
			return;
		}
		for (int y = half; y < maxIndex; y += stepSize) {
			for (int x = half; x < maxIndex; x += stepSize) {
				Square(x, y, half, ((float)rngesus.NextDouble() - 0.5f)/(this.terrainSize/stepSize));
			}
		}
		for (int y = 0; y <= maxIndex; y += half) {
			for (int x = (y + half) % stepSize; x <= maxIndex; x += stepSize) {
				Diamond(x, y, half, ((float)rngesus.NextDouble() - 0.5f)/(this.terrainSize/stepSize));
			}
		}
		DiamondSquare(half);
	}



	private float[,] GaussianBlur(){
		float[,] blurMap = new float[terrainSize, terrainSize];
		for (int x = 0; x < terrainSize; x++) {
			for (int y = 0; y < terrainSize; y++) {
				blurMap [x, y] = (float)(GetHeight (x - 1, y - 1) + GetHeight (x + 1, y + 1) + GetHeight (x + 1, y - 1) + GetHeight (x - 1, y + 1) + GetHeight (x + 1, y) * 2 + GetHeight (x, y + 1) * 2 + GetHeight (x - 1, y) * 2 + GetHeight (x, y - 1) * 2 + GetHeight (x, y) * 4) / 16;
			}
		}
		return blurMap;
	}



	private void Diamond(int x, int y, int size, float offset){
		float ave = (
			GetHeight(x, y - size)+
			GetHeight(x + size, y)+
			GetHeight(x, y + size)+
			GetHeight(x - size, y))/4;

		heightMap[x, y] = ave + offset;
	}



	private void Square(int x, int y, int size, float offset){
		float ave = (
			GetHeight (x - size, y - size)+
			GetHeight (x + size, y - size)+
			GetHeight (x + size, y + size)+
			GetHeight (x - size, y + size))/4;

		heightMap[x, y] = ave + offset;
	}



	private float GetHeight(int x, int y){
		if (x < 0 || x > maxIndex || y < 0 || y > maxIndex) {
			return 0;
		}
		return heightMap[x, y];
	}
		
}