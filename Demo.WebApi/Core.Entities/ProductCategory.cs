using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class ProductCategory
    {
        /// <summary>
        /// Product Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Product name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Product display name
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Products of category
        /// </summary>
        public ICollection<Product> Products { get; set; }

        public ProductCategory()
        {
            this.Products = new HashSet<Product>();
        }
    }
}
