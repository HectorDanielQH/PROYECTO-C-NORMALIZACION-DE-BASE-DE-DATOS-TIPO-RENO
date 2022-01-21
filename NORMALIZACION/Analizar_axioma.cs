using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace NORMALIZACION
{
    class Analizar_axioma
    {
        public bool Analizar_transitividad(String a,String b)
        {
            bool si = false;
            if (a.Equals(b))
                si = true;
            else
                si = false;
            return si;
        }
        public bool Analizar_pseudotransitividad(String a, String b)
        {
            bool si = false;
            char[] aa = a.ToCharArray();
            char[] bb = b.ToCharArray();
            int con = 0;
            for(int i=0;i<aa.Length;i++)
            {
                for(int j=0;j<bb.Length;j++)
                {
                    if (aa[i].Equals(bb[j]))
                        con++;
                }
                if(con>0)
                {
                    si = true;
                }
                else
                {
                    si = false;
                    break;
                }
                con = 0;
            }
            return si;
        }
        public String Izquierda1(String a,String b, String c, String d)
        {
            String cambio = c;
            char[] bb = b.ToCharArray();
            for(int i=0;i<bb.Length;i++)
            {
                String f = "" + bb[i];
                cambio = cambio.Replace(f, "");
            }
            String todo = a + cambio;
            return todo;
        }
        public String Izquierda2(String a, String b, String c, String d)
        {
            String cambio = a;
            char[] dd = d.ToCharArray();
            for (int i = 0; i < dd.Length; i++)
            {
                String f = "" + dd[i];
                cambio = cambio.Replace(f, "");
            }
            String todo = c + cambio;
            return todo;
        }
        public bool Analizar_aditividad(String a, String b)
        {
            bool si = false;
            if (a.Equals(b))
                si = true;
            else
                si = false;
            return si;
        }
        private String variables_repe(String c)
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
    }
}
