using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ControllerRobot : MonoBehaviour {
	public Module modulesList;
	public Stack<Module> modulesStack;

	public int batteryIndex = 3;

	// Use this for initialization
	void Awake () {
		modulesStack = new Stack<Module>();
		Module m = new Module(gameObject);
		modulesStack.Push (m);
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown (KeyCode.T))
		{
			GameObject go = FindClosest ("Module");
			transform.position = go.transform.position;// + go.GetComponent<SpringJoint2D>().anchor;//.transform.FindChild ("SnapA").transform.position;
			
			AddModule (go);

		}

		if(Input.GetKeyDown (KeyCode.R))
		{
			RemoveModule ();
		}

		if(Input.GetKeyDown (KeyCode.Y))
		{
			modulesList.components[0].Disable ();
		}
		if(Input.GetKeyDown (KeyCode.U))
		{
			modulesList.components[0].Enable ();
		}
	}

	void AddModule(GameObject go)
	{
		Module m = new Module(go);

		m.EnableModules (modulesStack.Peek ());

		if(m != null)
		{
			m.connection.enabled = true;
			m.connection.connectedBody = gameObject.GetComponent<Rigidbody2D>();

		}

		modulesStack.Push (m);


	}

	void RemoveModule()
	{
		if (modulesStack.Count <= 1) return;

		Module m = modulesStack.Peek ();
		m.connection.connectedBody = null;
		m.connection.enabled = false;

		modulesStack.Pop().EnableModules (false);

		modulesStack.Peek().EnableModules (true);

	}

	public BatteryModule GetBattery()
	{
		IModule m = modulesStack.Peek().components[batteryIndex];
		BatteryModule b = m as BatteryModule;
		return(b);
	}

	public GameObject FindClosest(string tag) {
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag(tag);
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject go in gos) {
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) {
				closest = go;
				distance = curDistance;
			}
		}
		return closest;
	}
}
