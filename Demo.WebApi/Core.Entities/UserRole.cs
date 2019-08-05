using System;

namespace Core.Entities
{
    public class UserRole
    {
        /// <summary>
        /// User identity
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Role identity
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// User
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Role
        /// </summary>
        public virtual Role Role { get; set; }
    }
}
