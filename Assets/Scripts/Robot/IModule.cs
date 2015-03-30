public interface IModule
{
	int priority{get;set;}
	
	void Enable();
	void Enable(bool enable);
	void Disable();
}

