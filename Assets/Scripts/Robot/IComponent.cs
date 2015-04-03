public interface IComponent
{
	int priority{get;set;}
	bool isEnabled{get;set;}
	
	void Enable();
	void Enable(bool enable);
	void Disable();
}

