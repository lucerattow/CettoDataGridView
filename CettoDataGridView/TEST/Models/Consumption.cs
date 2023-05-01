using CettoDataGridView.TEST.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CettoDataGridView.TEST.Models
{
    public class Consumption
    {
        public int Id { get; set; }
        public ConsumptionType Type { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}
