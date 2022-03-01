using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class CalendarEvent : IComparable<CalendarEvent>, IComparable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string OwnerId { get; set; }

        [Column(TypeName = "varchar(50)")]
        public DateTime EventDate { get; set; }
        
        [Column(TypeName = "varchar(50)")]
        public TimeSpan Duration { get; set; }

        public string Comment { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            return obj is CalendarEvent other
                ? CompareTo(other)
                : throw new ArgumentException($"Object must be of type {nameof(CalendarEvent)}");
        }

        public int CompareTo(CalendarEvent other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return string.Compare(Id, other.Id, StringComparison.Ordinal);
        }
    }
}