using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Group : IComparable<Group>, IComparable
    {
        public int Id { get; set; }

        public string CommandOwner { get; set; }

        public string CommandName { get; set; }

        public ICollection<User> GroupParticipants { get; set; }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            return obj is Group other
                ? CompareTo(other)
                : throw new ArgumentException($"Object must be of type {nameof(Group)}");
        }

        public int CompareTo(Group other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Id.CompareTo(other.Id);
        }
    }
}