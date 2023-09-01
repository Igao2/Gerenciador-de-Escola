using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Gerente
{
    public class Jason
    {
        public string transformardt(DataTable table)
        {
            string json = JsonConvert.SerializeObject(table, Formatting.Indented);
            return json;
        }
    }
    
}
