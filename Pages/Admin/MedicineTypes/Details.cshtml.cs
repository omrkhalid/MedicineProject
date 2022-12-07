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

namespace MedicineProject.Pages.Admin.MedicineTypes
{
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public DetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

      public MedicineType MedicineTypes { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _unitOfWork.MedicineType == null)
            {
                return NotFound();
            }

            var MedicineType = _unitOfWork.MedicineType.GetFirstOrDefault(m => m.Id == id);
            if (MedicineType == null)
            {
                return NotFound();
            }
            else 
            {
                MedicineTypes = MedicineType;
            }
            return Page();
        }
    }
}
