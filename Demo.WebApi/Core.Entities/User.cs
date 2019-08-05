using System;
using System.Collections.Generic;

namespace Core.Entities
{
    /// <summary>
    /// Entity POCO class for Users
    /// </summary>
    public class User
    {
        /// <summary>
        /// User Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// User display name
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Refresh tokens of user
        /// </summary>
        public ICollection<UserToken> Tokens { get; set; }

        /// <summary>
        /// Orders of user
        /// </summary>
        public virtual ICollection<Order>Orders { get; set; }

        /// <summary>
        /// Roles of user
        /// </summary>
        public virtual ICollection<UserRole> Roles { get; set; }

        public User()
        {
            this.Orders = new HashSet<Order>();
            this.Roles = new HashSet<UserRole>();
            this.Tokens = new HashSet<UserToken>();
        }
    }
}
