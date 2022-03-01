#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class User : IComparable<User>, IComparable
    {
        [Key] public string Id { get; set; }

        public string FullName { get; set; }

        [DataType(DataType.Text)] public DateTime Birthday { get; set; }

        public ICollection<Group> Groups { get; set; }

        public ICollection<Project> InProjects { get; set; }

        public ICollection<Project> UserProjects { get; set; }

        public ICollection<ProjectTask> Tasks { get; set; }

        public int CompareTo(object? obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            return obj is User other
                ? CompareTo(other)
                : throw new ArgumentException($"Object must be of type {nameof(User)}");
        }

        public int CompareTo(User? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return string.Compare(Id, other.Id, StringComparison.Ordinal);
        }
    }
}