using System.Collections.Generic;
using System.Linq;

namespace Atelier1.Models.Repositories
{
    public class EmployeeRepository : IRepository<Employee>
    {
        List<Employee> lemployees;

        public EmployeeRepository()
        {
            lemployees = new List<Employee>()
            {
                new Employee { Id = 1, Name = "Sofien ben ali", Departement = "comptabilité", Salary = 1000 },
                new Employee { Id = 2, Name = "Mourad triki", Departement = "RH", Salary = 1500 },
                new Employee { Id = 3, Name = "ali ben mohamed", Departement = "informatique", Salary = 1700 },
                new Employee { Id = 4, Name = "tarak aribi", Departement = "informatique", Salary = 1100 }
            };
        }

        public void Add(Employee entity)
        {
            lemployees.Add(entity);
        }

        public void Delete(int id)
        {
            var emp = FindByID(id);
            if (emp != null)
            {
                lemployees.Remove(emp);
            }
        }

        public Employee FindByID(int id)
        {
            return lemployees.FirstOrDefault(a => a.Id == id);
        }

        public IList<Employee> GetAll()
        {
            return lemployees;
        }

        public double SalaryAverage()
        {
            return lemployees.Average(x => x.Salary);
        }
        public double MaxSalary()
        {
            return lemployees.Max(x => x.Salary);
        }
        public int HrEmployeesCount()
        {
            return lemployees.Where(x => x.Departement == "HR").Count();
        }

        // Méthode de recherche unique
        public List<Employee> Search(string term)
        {
            if (!string.IsNullOrEmpty(term))
            {
                return lemployees.Where(a => a.Name.Contains(term) || a.Departement.Contains(term)).ToList();
            }
            else
            {
                return lemployees;
            }
        }

        public void Update(int id, Employee newEmployee)
        {
            var emp = FindByID(id);
            if (emp != null)
            {
                emp.Name = newEmployee.Name;
                emp.Departement = newEmployee.Departement;
                emp.Salary = newEmployee.Salary;
            }
        }

    }
}
