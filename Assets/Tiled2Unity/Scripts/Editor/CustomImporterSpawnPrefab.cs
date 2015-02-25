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
		}
		GameObject.DestroyImmediate (gameObject);

	}
	
	public void CustomizePrefab(UnityEngine.GameObject prefab)
	{
		// Do nothing
	}
}