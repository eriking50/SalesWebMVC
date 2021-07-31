using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        public readonly SellerService _sellerService;
        public readonly DepartmentService _departmentService;
        public SellersController(SellerService sellerService, DepartmentService department)
        {
            _sellerService = sellerService;
            _departmentService = department;
        }
        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var seller = _sellerService.FindById(id.Value);
            if (seller == null)
            {
                return NotFound();
            }
            return View(seller);
        }
        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel {Departments = departments};
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
