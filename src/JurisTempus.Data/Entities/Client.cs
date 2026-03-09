using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JurisTempus.Data.Entities
{
  public class Client
  {
    public int Id { get; set; }
    public required string Name { get; set; }
    public required Address Address { get; set; }
    public required string Phone { get; set; }
    public required string Contact { get; set; }

    public List<Case> Cases { get; set; } = new();
    public List<Invoice> Invoices { get; set; } = new();
  }
}
