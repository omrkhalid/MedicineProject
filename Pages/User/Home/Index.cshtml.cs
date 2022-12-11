using MedicineProject.DataAccess.Repository.IRepository;
using MedicineProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedicineProject.Pages.User.Home
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Clinic> ClinicList { get; set; }
        public IEnumerable<Doctor> DoctorList { get; set; }
        public void OnGet()
        {
            ClinicList = _unitOfWork.Clinic.GetAll(includeProperties: "Doctor,Customer");
            DoctorList = _unitOfWork.Doctor.GetAll();
        }
    }
}
