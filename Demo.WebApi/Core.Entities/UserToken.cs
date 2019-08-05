using System;

namespace Core.Entities
{
    public class UserToken
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public DateTime ValidFor { get; set; }

        public virtual User User { get; set; }
    }
}
