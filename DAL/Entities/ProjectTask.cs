using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class ProjectTask : IComparable<ProjectTask>, IComparable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public ICollection<User> Participants { get; set; }

        public string TaskName { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public TaskPriority Priority { get; set; }

        public DateTime TaskStart { get; set; }

        public DateTime TaskEnd { get; set; }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            return obj is ProjectTask other
                ? CompareTo(other)
                : throw new ArgumentException($"Object must be of type {nameof(ProjectTask)}");
        }

        public int CompareTo(ProjectTask other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Id.CompareTo(other.Id);
        }
    }
}