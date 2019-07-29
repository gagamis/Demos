using System;
using System.Collections.Generic;

namespace Core.Entities
{
    /// <summary>
    /// Entity POCO class for Orders
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Id of order
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// User Id of order
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Date of order
        /// </summary>
        public DateTime OrderDate { get; set; }


        /// <summary>
        /// User of order
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Products of order
        /// </summary>
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }


        public Order()
        {
            this.OrderProducts = new HashSet<OrderProduct>();
        }

    }
}
