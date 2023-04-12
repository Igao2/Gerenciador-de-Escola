using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Gerente
{
    public class Criptografia
    {
       public string[] Criptografar(string login,string senha)
        {
            string[] cripto = new string[2]; 
            MD5 mD5 = MD5.Create();
            byte[] valorLogin = mD5.ComputeHash(Encoding.Default.GetBytes(login));
            byte[] valorSenha = mD5.ComputeHash(Encoding.Default.GetBytes(senha));
            StringBuilder strL = new StringBuilder();
            StringBuilder strS = new StringBuilder();
            for(int i = 0;i<valorLogin.Length;i++)
            {
                strL.Append(valorLogin[i].ToString("X3"));
            }
            cripto[0] = strL.ToString();
            for(int j = 0;j<valorSenha.Length;j++)
            {
                strS.Append(valorSenha[j].ToString("X3"));
            }
            cripto[1] = strS.ToString();

            return cripto;
        }
     
    }
}
