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
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
      public Medicine Medicines { get; set; }

        public void OnGet(int id)
        {
            Medicines = _unitOfWork.Medicine.GetFirstOrDefault(u => u.Id==id);
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var Medicine = _unitOfWork.Medicine.GetFirstOrDefault(u => u.Id == Medicines.Id);
            if (Medicine != null)
            {
                Medicines = Medicine;
                _unitOfWork.Medicine.Remove(Medicines);
                _unitOfWork.Save();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
