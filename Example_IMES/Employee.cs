using System;
namespace Example_IMES
{
    public class Employee
    {
        // Properties
        public int id { get; private set; }
        public string name { get; private set; }
        public string factory { get; private set; }
        public string departmentName { get; private set; }
        public string IMES { get; private set; }
        public string WorkerCodIMES { get; private set; }
        
        public Employee(string name, string factory, string departmentName, int id = -1, string IMES = "", string WorkerCodIMES = "")// Constructor
        {
            this.name = name;
            this.factory = factory;
            this.departmentName = departmentName;
            if (id != -1)
            {
                this.id = id;
            }
            if (IMES != null)
            {
                this.IMES = IMES;
            }
            if (WorkerCodIMES != null)
            {
                this.WorkerCodIMES = WorkerCodIMES;
            }
        }

        public void addIMES(string IMES)// Method to add IMES to employee
        {
            this.IMES = IMES;
        }

        public void AddWorkerCodIMES(string WorkerCodIMES)// Method to add Worker Code for IMES
        {
            this.WorkerCodIMES = WorkerCodIMES;
        }

    }
}
	