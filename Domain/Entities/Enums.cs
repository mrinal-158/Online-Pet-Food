using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public enum UserRole
    {
        Customer,
        Admin
    }
    public enum PaymentStatus
    {
        Pending,
        Completed,
        Failed
    }
}
