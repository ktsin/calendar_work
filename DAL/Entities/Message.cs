using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Message : IComparable<Message>, IComparable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string Sender { get; set; }

        public string Recipient { get; set; }

        [DataType(DataType.Text)] public DateTime Sended { get; set; }

        public string MessageBody { get; set; }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            return obj is Message other
                ? CompareTo(other)
                : throw new ArgumentException($"Object must be of type {nameof(Message)}");
        }

        public int CompareTo(Message other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return string.Compare(Id, other.Id, StringComparison.Ordinal);
        }
    }
}