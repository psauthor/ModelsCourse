using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JurisTempus.Data.Entities
{
  public class Employee
  {
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Role { get; set; }
    public required string GovernmentId { get; set; }
    public decimal BillingRate { get; set; }

    public List<TimeBill> TimeBilling { get; set; } = new();
  }
}
