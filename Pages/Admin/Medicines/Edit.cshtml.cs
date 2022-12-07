using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MedicineProject.Data;
using MedicineProject.Models;
using MedicineProject.DataAccess.Repository.IRepository;

namespace MedicineProject.Pages.Admin.Medicines
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public Medicine Medicines { get; set; } = default!;

        public void OnGet(int id)
        {
            Medicines = _unitOfWork.Medicine.GetFirstOrDefault(u => u.Id == id);
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _unitOfWork.Medicine.Update(Medicines);
            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }
}
