using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[Tiled2Unity.CustomTiledImporter]
class CustomImporterAddComponent : Tiled2Unity.ICustomTiledImporter
{
	public void HandleCustomProperties(UnityEngine.GameObject gameObject,
	                                   IDictionary<string, string> props)
	{
		// Simply add a component to our GameObject
		if (props.ContainsKey("AddComponent"))
		{
			gameObject.AddComponent(props["AddComponent"]);
		}
	}
	
	public void CustomizePrefab(GameObject prefab)
	{
		// Do nothing
	}
}