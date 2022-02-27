#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class User : IComparable
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
            return Id.CompareTo((obj as User)?.Id);
        }
    }
}