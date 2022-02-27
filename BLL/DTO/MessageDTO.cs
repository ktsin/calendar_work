using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.DTO
{
    public class MessageDTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string Sender { get; set; }

        public string Recipient { get; set; }

        [DataType(DataType.Text)]
        public DateTime Sended { get; set; }

        public string MessageBody { get; set; }
    }
}
