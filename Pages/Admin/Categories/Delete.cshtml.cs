using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MedicineProject.Data;
using MedicineProject.Models;
using MedicineProject.DataAccess.Repository.IRepository;

namespace MedicineProject.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [BindProperty]
      public Category Categories { get; set; }

        public void OnGet(int id)
        {
            Categories = _unitOfWork.Category.GetFirstOrDefault(m => m.Id == id);
        }

        public async Task<IActionResult> OnPost()
        {
            var doctor = _unitOfWork.Category.GetFirstOrDefault(m => m.Id == Categories.Id);
            if (doctor != null)
            {
                _unitOfWork.Category.Remove(Categories);
                _unitOfWork.Save();
                TempData["success"] = "sucessfully";
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}
