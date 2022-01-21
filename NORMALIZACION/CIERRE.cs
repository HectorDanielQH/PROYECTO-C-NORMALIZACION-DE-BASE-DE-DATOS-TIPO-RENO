using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NORMALIZACION
{
    class CIERRE
    {
        DataTable tablita = null, devuelve = null;
        private String cierre = "", textBox1="", textBox2="";
        public CIERRE(String T1,String T2,DataTable tabli,DataTable devu)
        {
            textBox1 = T1;
            textBox2 = T2;
            tablita = tabli;
            devuelve = devu;
        }
        public String getCierre()
        {
            return cierre;
        }
        public String variables_repe(String c)
        {
            String valorc = "";
            char[] car = c.ToCharArray();
            int con = 0;
            for (int i = 0; i < c.Length; i++)
            {
                for (int j = 0; j < valorc.Length; j++)
                {
                    if (car[i] == valorc[j])
                        con++;
                }
                if (con == 0)
                {
                    valorc += car[i];
                }
                con = 0;
            }

            return valorc;
        }
        public bool Si_esta_dependencia(String x, String busca)
        {
            bool si = true;
            char[] Acomparar = x.ToCharArray();
            char[] comparar = busca.ToCharArray();
            int conta = 0;
            for (int i = 0; i < Acomparar.Length; i++)
            {
                for (int j = 0; j < comparar.Length; j++)
                {
                    if (Acomparar[i] == comparar[j])
                        conta++;
                }
                if (conta == 0)
                {
                    si = false;
                    break;
                }
                conta = 0;
            }
            return si;
        }
        public bool Existe_cierre()
        {
            cierre += textBox1+ " ----> " + textBox2 + "\n";
            cierre += textBox1 + "+ = ";
            String valor = textBox1;
            int ca = tablita.Rows.Count;
            for (int i = 0; i < ca; i++)
            {
                for (int j = 0; j < tablita.Rows.Count; j++)
                {
                    if (Si_esta_dependencia(tablita.Rows[j].ItemArray[0].ToString(), valor))
                    {
                        valor += tablita.Rows[j].ItemArray[2];
                        tablita.Rows.RemoveAt(j);
                    }
                }
            }
            valor = variables_repe(valor);
            cierre += valor + "\n";
            bool sip = false;
            if (Existe_Valor(textBox2, valor))
                sip = true;
            else
                sip = false;
            cierre += textBox2+ " Existe en: " + textBox1 + "+";
            return sip;
        }
        public bool Existe_Valor(String Valor1, String Valor2)
        {
            bool si = false;
            char[] va1 = Valor1.ToCharArray();
            char[] va2 = Valor2.ToCharArray();
            int cont = 0;
            for (int i = 0; i < va1.Length; i++)
            {
                for (int j = 0; j < va2.Length; j++)
                {
                    if (va1[i] == va2[j])
                    {
                        cont++;
                    }
                }
                if (cont > 0)
                {
                    si = true;
                }
                else
                {
                    si = false;
                    break;
                }
                cont = 0;
            }
            return si;
        }
    }
}
