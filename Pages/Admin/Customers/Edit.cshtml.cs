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

namespace MedicineProject.Pages.Admin.Customers
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public Customer Customers { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _unitOfWork.Customer == null)
            {
                return NotFound();
            }

            var customer = _unitOfWork.Customer.GetFirstOrDefault(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            Customers = customer;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _unitOfWork.Customer.Update(Customers);
            _unitOfWork.Save();
            TempData["success"] = "sucessfully";
            return RedirectToPage("./Index");
        }
    }
}
