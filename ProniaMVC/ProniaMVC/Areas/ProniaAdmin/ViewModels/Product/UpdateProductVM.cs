using ProniaMVC.Models;
using System.ComponentModel.DataAnnotations;

namespace ProniaMVC.Areas.ProniaAdmin.ViewModels
{
    public class UpdateProductVM
    {
        public string Name { get; set; }

        public string Description { get; set; }
        [Required]
        public int? CategoryId { get; set; }
        public decimal Price { get; set; }
        public string SKU { get; set; }
        public List<Category>? Categories { get; set; }
    }
}
