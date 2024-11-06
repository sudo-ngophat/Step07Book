using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Booking.Domain
{
    public class Villa
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên Villa không được để trống.")]
        [MaxLength(50, ErrorMessage = "Tên Villa không được vượt quá 50 ký tự.")]
        public required string Name { get; set; }
        public string? Description { get; set; }

        [Range(1000000, 1000000000, ErrorMessage = "Giá phải từ 1,000,000 đến 1,000,000,000 VND.")]
        public double Price { get; set; }
        public int Sqft { get; set; }

        [Range(1, 10)]
        public int Occupancy { get; set; }

        [Display(Name = "Image Url")]

        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }

        //ngày được tạo
        public DateTime? Created_Date { get; set; }

        //ngày villa được updated 
        public DateTime? Updated_Date { get; set; }

 
    }
}
