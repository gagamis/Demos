using System;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Products
{
    public class AddProductRequest
    {
        [Required]
        [MaxLength(256)]
        public string Code { get; set; }

        [Required]
        [MaxLength(512)]
        public string DisplayName { get; set; }

        [Required]
        [MaxLength(1024)]
        public string Description { get; set; }

        public Guid? ProductCategoryId { get; set; }
    }
}
