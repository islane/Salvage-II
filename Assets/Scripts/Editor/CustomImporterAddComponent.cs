using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Tiled2Unity.CustomTiledImporter]
class CustomImporterAddComponent : Tiled2Unity.ICustomTiledImporter
{
	public void HandleCustomProperties(UnityEngine.GameObject gameObject,
	                                   IDictionary<string, string> props)
	{
		// Simply add a component to our GameObject
		if (props.ContainsKey("AddComponent"))
		{
			//UnityEngineInternal.APIUpdaterRuntimeServices.AddComponent(gameObject, "Assets/Scripts/Editor/CustomImporterAddComponent.cs (14,4)", props["AddComponent"]);
			gameObject.AddComponent (Type.GetType (props["AddComponent"]));
		}
	}
	
	public void CustomizePrefab(GameObject prefab)
	{
		// Do nothing
	}
}