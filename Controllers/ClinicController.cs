using MedicineProject.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;


namespace MedicineProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ClinicController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment) 
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var clinicList = _unitOfWork.Clinic.GetAll(includeProperties: "Doctor,Customer");
            return Json(new {data = clinicList});
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Clinic.GetFirstOrDefault(u => u.Id == id);
            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, objFromDb.Image.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.Clinic.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new {success = true, message = "Delete successful."});
        }
    }
}
