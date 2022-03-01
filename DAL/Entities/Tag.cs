using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace DAL.Entities
{
    public class Tag : IComparable<Tag>, IComparable
    {
        [Key] public int Id { get; set; }

        public string Name { get; set; }

        public string OwnerId { get; set; }

        public KnownColor TagColor { get; set; } = KnownColor.Silver;

        public ICollection<CalendarEvent> CalendarEvents { get; set; }

        public ICollection<ProjectTask> ProjectTasks { get; set; }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            return obj is Tag other
                ? CompareTo(other)
                : throw new ArgumentException($"Object must be of type {nameof(Tag)}");
        }

        public int CompareTo(Tag other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Id.CompareTo(other.Id);
        }
    }
}