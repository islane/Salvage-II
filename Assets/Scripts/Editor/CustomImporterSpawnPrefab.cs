using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[Tiled2Unity.CustomTiledImporter]
class CustomTiledImporterSpawnPrefab : Tiled2Unity.ICustomTiledImporter
{
	public void HandleCustomProperties(UnityEngine.GameObject gameObject,
	                                   IDictionary<string, string> props)
	{
		// Does this game object have a spawn property?
		if (!props.ContainsKey("spawn"))
			return;
		
		// Load the prefab assest and Instantiate it
		UnityEngine.Object spawn = 
			Resources.Load<UnityEngine.Object>(props["spawn"]);
		if (spawn != null)
		{
			GameObject spawnInstance = 
				(GameObject)GameObject.Instantiate(spawn);
			spawnInstance.name = spawn.name;
			
			// Use the position of the game object we're attached to
			//spawnInstance.transform.parent = gameObject.transform;
			//spawnInstance.transform.localPosition = Vector3.zero;
			spawnInstance.transform.parent = gameObject.transform.parent.transform;
			spawnInstance.transform.position = gameObject.transform.position;

			//Special Cases:			
			//Robots use the center of the sprite as their origin, so they have to be offest
			if(spawnInstance.GetComponent<Robot>())
			{
				float rWidth = 1;//16; //Change this as soon as I know how to access it from the exported data...
				float rHeight = 1;//spawnInstance.GetComponent<Renderer>().bounds.size.y / 2;
				spawnInstance.transform.position += new Vector3(rWidth, rHeight, 0.0f);
			}

		}
		GameObject.DestroyImmediate (gameObject);

	}
	
	public void CustomizePrefab(UnityEngine.GameObject prefab)
	{
		// Do nothing
	}
}