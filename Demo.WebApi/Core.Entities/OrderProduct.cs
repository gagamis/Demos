using System;

namespace Core.Entities
{
    public class OrderProduct
    {
        /// <summary>
        /// Order Id
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// Product Id
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Amount of product
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Product
        /// </summary>
        public virtual Product Product { get; set; }

        /// <summary>
        /// Order
        /// </summary>
        public virtual Order Order { get; set; }
    }
}
