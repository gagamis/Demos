using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Role
    {
        /// <summary>
        /// Id of role
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Name of role
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Display name of role
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Users of role
        /// </summary>
        public virtual ICollection<UserRole> UserRoles { get; set; }


        public Role()
        {
            this.UserRoles = new HashSet<UserRole>();
        }
    }
}
