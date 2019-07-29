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

        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}
