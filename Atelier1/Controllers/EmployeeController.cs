using Atelier1.Models.Repositories;
using Atelier1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Atelier1.Controllers
{
    public class EmployeeController : Controller
    {
        readonly IRepository<Employee> employeeRepository;

        //injection de dépendance
        public EmployeeController(IRepository<Employee> empRepository)
        {

            employeeRepository = empRepository;

        }
        // GET: EmployeeController
        public ActionResult Index()
        {
            var employees = employeeRepository.GetAll();
            ViewData["EmployeesCount"] = employees.Count();
            ViewData["SalaryAverage"] = employeeRepository.SalaryAverage();
            ViewData["MaxSalary"] = employeeRepository.MaxSalary();
            ViewData["HREmployeesCount"] = employeeRepository.HrEmployeesCount();

            return View(employees);
        }

        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
            var employee = employeeRepository.FindByID(id);

            return View(employee);
        }

        // GET: EmployeeController/Create
        public ActionResult Create(Employee e)
        {
            employeeRepository.Add(e);
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Edit/5
        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            var employee = employeeRepository.FindByID(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }


        // POST: EmployeeController/Edit/5
        // POST: EmployeeController/Edit/5
        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Employee updatedEmployee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingEmployee = employeeRepository.FindByID(id);
                    if (existingEmployee == null)
                    {
                        return NotFound();
                    }

                    // Update the employee fields
                    existingEmployee.Name = updatedEmployee.Name;
                    existingEmployee.Departement = updatedEmployee.Departement;
                    existingEmployee.Salary = updatedEmployee.Salary;

                    // Save the changes in the repository
                    employeeRepository.Update(id, existingEmployee);

                    return RedirectToAction(nameof(Index));
                }
                return View(updatedEmployee);
            }
            catch
            {
                return View(updatedEmployee);
            }
        }



        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            employeeRepository.Delete(id);
            return View();
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Search(string term)
        {

            var result = employeeRepository.Search(term);
            return View("Index", result);
        }
    }
}
