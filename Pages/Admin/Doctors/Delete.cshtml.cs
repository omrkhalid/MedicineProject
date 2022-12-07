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

namespace MedicineProject.Pages.Doctors
{
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [BindProperty]
      public Doctor Doctor { get; set; }

        public void OnGet(int id)
        {
           Doctor = _unitOfWork.Doctor.GetFirstOrDefault(m => m.Id == id);
        }

        public async Task<IActionResult> OnPost()
        {
            var doctor = _unitOfWork.Doctor.GetFirstOrDefault(m => m.Id == Doctor.Id);
            if (doctor != null)
            {
                _unitOfWork.Doctor.Remove(doctor);
                _unitOfWork.Save();
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}
