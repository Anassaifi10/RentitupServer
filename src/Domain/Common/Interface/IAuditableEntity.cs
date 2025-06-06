using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Domain.Common.Interface;
public interface IAuditableEntity
{

    DateTimeOffset Created { get; set; }

    string? CreatedBy { get; set; }

    DateTimeOffset LastModified { get; set; }

    string? LastModifiedBy { get; set; }
}
