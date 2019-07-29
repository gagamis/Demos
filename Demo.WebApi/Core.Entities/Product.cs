using System;
using System.Collections.Generic;

namespace Core.Entities
{
    /// <summary>
    /// Entity POCO class for Product
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Id of Product
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Product code name
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Product display name
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Product description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Category Id of product
        /// </summary>
        public Guid? ProductCategoryId { get; set; }

        /// <summary>
        /// Orders of product
        /// </summary>
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }

        /// <summary>
        /// Category of product
        /// </summary>
        public virtual ProductCategory ProductCategory { get; set; }

        public Product()
        {
            this.OrderProducts = new HashSet<OrderProduct>();
        }
    }
}
