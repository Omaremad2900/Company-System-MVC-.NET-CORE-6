using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Models;
using Demo.PL.Helpers;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics;

namespace Demo.PL.Controllers
{
	[Authorize]
	public class EmployeeController : Controller
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EmployeeController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchValue)
        {
            IEnumerable<Employee> emp;
            if (string.IsNullOrEmpty(SearchValue))
            {
                 emp =await _unitOfWork.EmployeeRepository.GetAll();
                
            }
            else 
            {
                 emp=await _unitOfWork.EmployeeRepository.GetEmployeesByName(SearchValue);
                
            }
            var mapped = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(emp);
            return View(mapped);



        }
        [HttpGet]
        public IActionResult Create()
        {
            /*var departments = _employeeDepartmentRepository.GetAll();
            ViewBag.Departments = departments;*/

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {
            
            if (ModelState.IsValid)
            {
                if(employeeVM.Image is not null)
                employeeVM.ImageName=DocumentSetting.UploadFile(employeeVM.Image, "Images");
                Employee employee=_mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                await _unitOfWork.EmployeeRepository.Add(employee);
                int res =await _unitOfWork.Complete();
                if (res > 0)
                    TempData["Message"] = "Employeee is Created";
                return RedirectToAction(nameof(Index));

            }
            
            return View(employeeVM);
        }
        public async Task<IActionResult> Details(int? id,string ViewName="Details")
        {
            if (id is null)
                return BadRequest();
            var employee = await _unitOfWork.EmployeeRepository.GetById(id.Value);
            if (employee is null)
                return NotFound();
            var MappedEmployee=_mapper.Map<Employee, EmployeeViewModel>(employee);
            return View( ViewName,MappedEmployee);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {

            return await Details(id, "Edit");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeViewModel employee, [FromRoute] int id)
        {
            if (employee.Id!= id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    if (employee.Image is not null )
                    {
                        employee.ImageName = DocumentSetting.UploadFile(employee.Image, "Images");
                    }
                    var mapped=_mapper.Map<EmployeeViewModel, Employee>(employee);
                    _unitOfWork.EmployeeRepository.Update(mapped);
                    await _unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            
            var employee = await _unitOfWork.EmployeeRepository.GetById(id);
            if (employee != null)
            {
                string? image = employee.ImageName;
                _unitOfWork.EmployeeRepository.Delete(employee);
                int res=await _unitOfWork.Complete();
                if (res > 0 && image != null)
                {
                    DocumentSetting.DeleteFile(image, "Images");
                }    
                    

            }
            return RedirectToAction(nameof(Index));
        }


    }
}

