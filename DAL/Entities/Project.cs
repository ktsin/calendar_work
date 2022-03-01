using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Project : IComparable<Project>, IComparable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public ICollection<User> Participants { get; set; }

        public ICollection<ProjectTask> Tasks { get; set; }

        public string ProjectOwner { get; set; }

        public string Name { get; set; }

        [Column(TypeName = "varchar(50)")]
        public DateTime ProjectStart { get; set; }

        [Column(TypeName = "varchar(50)")]
        public DateTime ProjectEnd { get; set; }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            return obj is Project other
                ? CompareTo(other)
                : throw new ArgumentException($"Object must be of type {nameof(Project)}");
        }

        public int CompareTo(Project other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Id.CompareTo(other.Id);
        }
    }
}