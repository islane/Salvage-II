using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//[SerializeField]
public class Module
{
	public List<IModule> components;
	public GameObject gameObject;
	public SpringJoint2D connection;
	
	public Module(GameObject go)
	{
		components = new List<IModule>();
		
		components.Add (go.GetComponent<MovementModule>());
		components.Add (go.GetComponent<JumpModule>());
		components.Add (go.GetComponent<BoostModule>());
		components.Add (go.GetComponent<BatteryModule>());
		
		connection = go.GetComponent<SpringJoint2D>();
		
	}
	
	public void EnableModules(bool enable)
	{
		foreach(IModule m in components)
		{
			if(m != null)
				m.Enable (enable);
		}
		
	}
	
	public void EnableModules(Module oldModules)
	{
		for(int i = 0; i < components.Count; i++)
		{
			if(components[i] != null && components[i].priority >= oldModules.components[i].priority)
			{
				if(oldModules.components[i] != null)
					oldModules.components[i].Enable(false);
				components[i].Enable (true);
			}
		}
		
	}
	
}
