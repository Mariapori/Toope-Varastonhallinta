using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toope_Varastonhallinta.Mallit
{
    public class Kirjaus
    {
        public int ID { get; set; }
        public Varasto Varasto { get; set; }
        public Tuote Tuote { get; set; }
        public double maara { get; set; }
    }
}
