using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JurisTempus.Data.Entities
{
  public class Invoice
  {
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string InvoiceNumber { get; set; }
    public DateTime DueDate { get; set; }
    public string PublicComments { get; set; }

    public ICollection<TimeBill> TimeBills { get; set; }
    public Client Client { get; set; }

  }
}
