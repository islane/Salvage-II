using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//[SerializeField]
public class Module
{
	public List<IComponent> components;
	public GameObject gameObject;
	public SpringJoint2D connection;
	
	public Module(GameObject go)
	{
		components = new List<IComponent>();
		
		components.Add (go.GetComponent<MovementComponent>());
		components.Add (go.GetComponent<JumpComponent>());
		components.Add (go.GetComponent<BoostComponent>());
		components.Add (go.GetComponent<BatteryComponent>());
		
		connection = go.GetComponent<SpringJoint2D>();
		
	}
	
	public void EnableModules(bool enable)
	{
		foreach(IComponent m in components)
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
