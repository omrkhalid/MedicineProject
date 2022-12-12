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
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public DetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

      public Category Categories { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null || _unitOfWork.Category == null)
            {
                return NotFound();
            }

            var category = _unitOfWork.Category.GetFirstOrDefault(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            else 
            {
                Categories = category;
            }
            return Page();
        }
    }
}
