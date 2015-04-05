using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


//[SerializeField]
public class Module
{
	//public List<IComponent> components;
	public Dictionary<Type, IComponent> componentDict;
	public GameObject gameObject;
	public SpringJoint2D connection;
	public Transform previousParent;
	
	public Module(GameObject go)
	{
		/*components = new List<IComponent>();
		
		components.Add (go.GetComponent<MovementComponent>());
		components.Add (go.GetComponent<JumpComponent>());
		components.Add (go.GetComponent<BoostComponent>());
		components.Add (go.GetComponent<BatteryComponent>());
*/
		gameObject = go;
		previousParent = go.transform.parent;

		componentDict = new Dictionary<Type, IComponent>();

		componentDict.Add (typeof(MovementComponent), go.GetComponent<MovementComponent>());
		componentDict.Add (typeof(JumpComponent), go.GetComponent<JumpComponent>());
		componentDict.Add (typeof(BoostComponent),go.GetComponent<BoostComponent>());
		componentDict.Add (typeof(BatteryComponent), go.GetComponent<BatteryComponent>());

		connection = go.GetComponent<SpringJoint2D>();


	}
	
	public void EnableModules(bool enable)
	{
		/*foreach(IComponent m in components)
		{
			if(m != null)
				m.Enable (enable);
		}*/

		foreach(KeyValuePair<Type, IComponent> m in componentDict)
		{
			if(m.Value != null)
				m.Value.Enable (enable);
		}
	}
	
	public void EnableModules(Module oldModules)
	{
		/*for(int i = 0; i < components.Count; i++)
		{
			if(components[i] != null && components[i].priority >= oldModules.components[i].priority)
			{
				if(oldModules.components[i] != null)
					oldModules.components[i].Enable(false);
				components[i].Enable (true);
			}
		}*/

		foreach(KeyValuePair<Type, IComponent> c in componentDict)
		{
			if(c.Value != null) 
			{
				if(oldModules.componentDict[c.Key] != null)
				{
					if(c.Value.priority >= oldModules.componentDict[c.Key].priority)
					{
						oldModules.componentDict[c.Key].Enable (false);
						c.Value.Enable (true);
					}
				}
				else
				{
					c.Value.Enable (true);
				}
			}
		}
	}

	public void ResetParent()
	{
		gameObject.transform.SetParent (previousParent);
	}
	
}
