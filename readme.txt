DIAMOND SQUARE ALGORITHM TERRAIN GENERATION



1. Detail:
name: Mingyang Zhang
login: mingyangz



2. How it works:

A height map is first generated through diamond square algorithm, and then
applied to the Terrain game object in Unity.

The implementation of the diamond square algorithm is based on these sources:
https://en.wikipedia.org/wiki/Diamond-square_algorithm
http://www.playfuljs.com/realistic-terrain-in-130-lines/

Unity standard assets are imported for terrain textures, namely rock, sand
and grass.
Snow texture is from Unity assets store:
https://www.assetstore.unity3d.com/en/#!/content/54630



3. Control scheme:

Mouse to control camera view.

W, S, A, D to control camera movement.

Q, E to rotate camera around view direction.

R to generate new terrain in game.
