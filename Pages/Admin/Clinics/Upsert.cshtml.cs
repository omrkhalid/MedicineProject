using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MedicineProject.Data;
using MedicineProject.Models;
using MedicineProject.DataAccess.Repository.IRepository;
using NuGet.Packaging.Signing;

namespace MedicineProject.Pages.Admin.Clinics
{
    [BindProperties]
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public UpsertModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            Clinics = new();
            _hostEnvironment = hostEnvironment;
        }
        public Clinic Clinics { get; set; }
        public IEnumerable<SelectListItem> DoctorList { get; set; }
        public IEnumerable<SelectListItem> CustomerList { get; set; }

        public void OnGet(int? id)
        {
            if (id != null)
            {
                //Edit
                Clinics = _unitOfWork.Clinic.GetFirstOrDefault(u => u.Id == id);
            }
            DoctorList = _unitOfWork.Doctor.GetAll().Select(i => new SelectListItem()
            {
                Text = i.FirstName,
                Value = i.Id.ToString()
            });
            CustomerList = _unitOfWork.Customer.GetAll().Select(i => new SelectListItem()
            {
                Text = i.FirstName,
                Value = i.Id.ToString()
            });
        }

        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            string webRootPath = _hostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            if(Clinics.Id == 0)
            {
                //create
                string fileName_new = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"images\clinics");
                var extension = Path.GetExtension(files[0].FileName);
                using (var fileStream = new FileStream(Path.Combine(uploads, fileName_new + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                Clinics.Image = @"images\clinics\" + fileName_new + extension;
                _unitOfWork.Clinic.Add(Clinics);
                _unitOfWork.Save();
            }
            else
            {
                //edit
                var objFromDb = _unitOfWork.Clinic.GetFirstOrDefault(u => u.Id == Clinics.Id);
                if (files.Count > 0)
                {
                    string fileName_new = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\clinics");
                    var extension = Path.GetExtension(files[0].FileName);
                    
                    //delet the old image
                    var oldImagePath = Path.Combine(webRootPath, objFromDb.Image);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                    // new upload
                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName_new + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    Clinics.Image = @"\images\clinics\" + fileName_new + extension;
                }
                else 
                {
                    Clinics.Image = objFromDb.Image;
                }
                _unitOfWork.Clinic.Update(Clinics);
                _unitOfWork.Save();
            }
            return RedirectToPage("./Index");
        }
    }
}
