using Microsoft.AspNetCore.Mvc;
using Demo.BLL.Repositories;

using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace Demo.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<IActionResult> IndexAsync()
        {
            var dps =await _unitOfWork.DepartmentRepository.GetAll();
            return View(dps);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {

                await _unitOfWork.DepartmentRepository.Add(department);
                int res = await _unitOfWork.Complete();
                if (res > 0)
                    TempData["Message"] = "Employeee is Created";

                return RedirectToAction(nameof(IndexAsync));

            }
            return View(department);
            
        }
        public async Task<IActionResult> Details(int? id) 
        {
            if (id is null)
                return BadRequest();
            var dep = await _unitOfWork.DepartmentRepository.GetById(id.Value);
            
            if(dep is null)
                return NotFound();
            return View(dep);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        { if (id is null)
                return BadRequest();
            var dep = await _unitOfWork.DepartmentRepository.GetById(id.Value);
            if (dep is null)
                return NotFound();
            return View(dep);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Department department,[FromRoute] int id )
        {
            if(department.Id!= id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.DepartmentRepository.Update(department);
                    await _unitOfWork.Complete();
                    return RedirectToAction(nameof(IndexAsync));
                }
                catch (Exception ex) { 
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(department);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var department =await _unitOfWork.DepartmentRepository.GetById(id);
            if (department != null)
            {
                _unitOfWork.DepartmentRepository.Delete(department);
               await _unitOfWork.Complete();
                
            }
            return RedirectToAction(nameof(IndexAsync));
        }


    }


}