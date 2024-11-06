using Booking.Application.Common.Interface;
using Booking.Domain;
using Booking.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public VillaController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var villas = _unitOfWork.Villa.GetAll();
            //ToList(), Add() là phương thức LINQ được dùng để truy vấn data từ DB
            return View(villas);
        }
        //trả về toàn bộ các villas

        public IActionResult Create()
        {
            return View("Create");
        }
        //empty view for creating new villa 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Villa obj)
        {
            if (obj.Name == obj.Description)
            {
                ModelState.AddModelError("name", "The description cannot exactly match the Name.");
            }//thêm tùy chỉnh yêu cầu xác thực cho models

            if (ModelState.IsValid)
            {
                _unitOfWork.Villa.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "The villa has been created successfully.";
                return RedirectToAction("Index");
            }
            return View(obj); // Trả lại view với dữ liệu không hợp lệ để hiển thị thông báo lỗi
        }//lây dữ liệu sau khi người dùng nhập để lưu chuẩn bị lưu vào database

        // GET: Villa/Update/5
        public IActionResult Update(int villaId)
        {
            Villa? obj = _unitOfWork.Villa.Get(u => u.Id == villaId);

            //retrieve only one record with FirstOrDefault với ý nghĩa: trong csdl, tìm bảng Villas, tìm record có id
            //ngoài ra để retrieve một tập hợp các record còn có Where/Find 
            if (obj == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View("Update", obj); // Truyền đối tượng Villa vào View để cập nhật
        }

        // POST: Villa/Update
        [HttpPost]
        public IActionResult Update(Villa obj)
        {
            if (ModelState.IsValid && obj.Id > 0)
            {
                _unitOfWork.Villa.Update(obj);
                _unitOfWork.Save();// Lưu thay đổi vào cơ sở dữ liệu
                TempData["success"] = "The villa has been updated successfully.";
                return RedirectToAction("Index");
            }
            return View(); // Trả lại View nếu có lỗi xác thực
        }

        // GET 
        public IActionResult Delete(int villaId)
        {
            Villa? obj = _unitOfWork.Villa.Get(u => u.Id == villaId);
            // if (obj == null)
            // {
            //     return RedirectToAction("Error", "Home");
            // }
            return View("Delete", obj); // Truyền đối tượng Villa vào View để cập nhật
        }

        // POST: Villa/Update
        [HttpPost]
        public IActionResult DeleteConfirmed(Villa obj)
        {
            Villa? objFromDb = _unitOfWork.Villa.Get(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                _unitOfWork.Villa.Remove(objFromDb);
                _unitOfWork.Save();
                TempData["success"] = "The villa has been deleted successfully.";
            }
            else
            {
                TempData["error"] = "Failed to delete the villa.";
            }
            return RedirectToAction("Index");
        }

    }
}
