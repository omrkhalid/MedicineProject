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

namespace MedicineProject.Pages.Admin.Medicines
{
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public DetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

      public Medicine Medicines { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _unitOfWork.Medicine == null)
            {
                return NotFound();
            }

            var Medicine = _unitOfWork.Medicine.GetFirstOrDefault(m => m.Id == id);
            if (Medicine == null)
            {
                return NotFound();
            }
            else 
            {
                Medicines = Medicine;
            }
            return Page();
        }
    }
}
