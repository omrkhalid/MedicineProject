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
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
      public MedicineType MedicineTypes { get; set; }

        public void OnGet(int id)
        {
            MedicineTypes = _unitOfWork.MedicineType.GetFirstOrDefault(u => u.Id==id);
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var MedicineType = _unitOfWork.MedicineType.GetFirstOrDefault(u => u.Id == MedicineTypes.Id);
            if (MedicineType != null)
            {
                MedicineTypes = MedicineType;
                _unitOfWork.MedicineType.Remove(MedicineTypes);
                _unitOfWork.Save();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
