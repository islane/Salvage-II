using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


public class ControllerRobot : MonoBehaviour {
	public Module modulesList;
	public Stack<Module> modulesStack;
	public Rigidbody2D rb;

	// Use this for initialization
	void Awake () {
		modulesStack = new Stack<Module>();
		Module m = new Module(gameObject);
		modulesStack.Push (m);

		m.EnableModules (true);

		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown (KeyCode.T))
		{
			GameObject go = FindClosest ("Module", 2.5f, modulesStack.Peek ().gameObject);
			if (go != null)
			{
				AddModule (go);
			}

		}

		if(Input.GetKeyDown (KeyCode.R))
		{
			RemoveModule ();
		}

		/*if(Input.GetKeyDown (KeyCode.Y))
		{
			modulesList.components[0].Disable ();
		}
		if(Input.GetKeyDown (KeyCode.U))
		{
			modulesList.components[0].Enable ();
		}*/

		//TODO: Quick fix for rolling ball
		if(modulesStack.Count > 1)
		{
			transform.localRotation = new Quaternion();
			rb.fixedAngle = true;
		}
		else
			rb.fixedAngle = false;
	}

	void AddModule(GameObject go)
	{
		Module m = new Module(go);

		if(m != null)
		{
			m.EnableModules (modulesStack.Peek ());
			m.connection.enabled = true;
			m.connection.connectedBody = gameObject.GetComponent<Rigidbody2D>();

			modulesStack.Peek ().gameObject.transform.SetParent (m.gameObject.transform);
			modulesStack.Peek ().gameObject.transform.localScale = m.gameObject.transform.localScale;
			modulesStack.Push (m);


		}

		Debug.Log (modulesStack.Peek ().gameObject);


	}

	void RemoveModule()
	{
		if (modulesStack.Count <= 1) return;

		Module m = modulesStack.Peek ();
		m.connection.connectedBody = null;
		m.connection.enabled = false;

		modulesStack.Pop().EnableModules (false);

		modulesStack.Peek().EnableModules (true);

		modulesStack.Peek ().ResetParent ();
	}

	public BatteryComponent GetBattery()
	{
		return(modulesStack.Peek().componentDict[typeof(BatteryComponent)] as BatteryComponent);
	}

	public IComponent GetTopComponent(Type cType)
	{
		IComponent c = null;

		foreach(Module m in modulesStack.Reverse ())
		{
			c = m.componentDict[cType] as IComponent;
			if (c != null && c.isEnabled)
				break;
		}

		return c;
	}

	public GameObject GetTopModule()
	{
		return(modulesStack.Peek ().gameObject);
	}

	public GameObject FindClosest(string tag, float minDistance, GameObject exception) {
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag(tag);
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject go in gos) {
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance && curDistance < minDistance && go != exception) {
				closest = go;
				distance = curDistance;
			}
		}
		return closest;
	}
}
