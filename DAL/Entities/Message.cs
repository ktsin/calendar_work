using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Message
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
