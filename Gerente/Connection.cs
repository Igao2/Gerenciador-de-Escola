using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Gerente
{
     class Connection
    {
        public SQLiteConnection sq = new SQLiteConnection("Data Source = C:\\Users\\Igor\\Desktop\\Gerente Escolar\\Gerente\\GEBD.db");

        public void conectar()
        {
            sq.Open();
        }
        public void desconectar()
        {
            sq.Close();
        }
    }
}
