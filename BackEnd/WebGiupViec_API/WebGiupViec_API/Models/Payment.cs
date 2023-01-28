using System;
using System.Collections.Generic;

#nullable disable

namespace WebGiupViec_API.Models
{
    public partial class Payment
    {
        public Payment()
        {
            JobPayments = new HashSet<JobPayment>();
        }

        public int PaymentId { get; set; }
        public string PaymentName { get; set; }
        public string Mota { get; set; }

        public virtual ICollection<JobPayment> JobPayments { get; set; }
    }
}
