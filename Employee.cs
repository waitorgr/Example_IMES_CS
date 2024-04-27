using System;

public class Employee
{
	private int id = 0; {get};
	private string name; {get};
	private string IMES; {get};
	public Employee(string name,string IMES)
	{
		this.name = name;
		this.IMES = IMES;
		id+=1
	}

}
