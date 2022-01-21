using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NORMALIZACION
{
    public partial class Dependencia_funcional_minima : Form
    {
        String mensaje = "";
        DataTable tabla, de_origen;
        public Dependencia_funcional_minima(DataTable tan)
        {
            InitializeComponent();
            tabla = tan;
            de_origen = tan;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private bool sigue_habiendo()
        {
            bool si = false;
            for(int i=0;i<dataGridView1.RowCount;i++)
            {
                if(dataGridView1.Rows[i].Cells[2].Value.ToString().Length>1)
                {
                    si = true;
                    break;
                }
            }
            return si;
        }
        private void Eliminacion_dep_iguales()
        {
            /*
             * primero ordenamos los caracteres de manera ascendente
             * 
             */
            for(int i=0;i<dataGridView1.RowCount;i++)
            {
                char[] uno = dataGridView1.Rows[i].Cells[0].Value.ToString().ToCharArray();
                char[] dos = dataGridView1.Rows[i].Cells[2].Value.ToString().ToCharArray();
                dataGridView1.Rows[i].Cells[0].Value = ordenado(uno);
                dataGridView1.Rows[i].Cells[2].Value = ordenado(dos);
            }
            /*
             * AHORA ELIMINAMOS DEPENDENCIAS QUE SEAN IGUALES
             */
            ELIM_FILAS();
        }
        public void ELIM_FILAS()
        {
            String mensaje1 = "";
            for(int i=0;i<dataGridView1.RowCount;i++)
            {
                String a=dataGridView1.Rows[i].Cells[0].Value.ToString(), b = dataGridView1.Rows[i].Cells[2].Value.ToString();
                for (int j = dataGridView1.RowCount - 1; j > i; j--)
                {
                    if (dataGridView1.Rows[j].Cells[0].Value.ToString().Equals(a))
                    {
                        if (dataGridView1.Rows[j].Cells[2].Value.ToString().Equals(b))
                        {
                            mensaje1 += "SE REMUEVE LA DF: \n" + dataGridView1.Rows[j].Cells[0].Value.ToString() + " ------> " + dataGridView1.Rows[j].Cells[2].Value.ToString() + "\nPORQUE ESTA REPETIDA\n";
                            dataGridView1.Rows.RemoveAt(j);
                        }
                    }
                }
            }
            mensaje += mensaje1;
        }
        public String ordenado(char[] vavaLOR)
        {
            String todo="";
            char[] valor = vavaLOR;
            for(int i=0;i<valor.Length;i++)
            {
                for(int j=0;j<valor.Length; j++)
                {
                    if((int)valor[j]>(int)valor[i])
                    {
                        char a = valor[i];
                        valor[i] = valor[j];
                        valor[j] = a;
                    }
                }
            }
            for(int i=0;i<valor.Length;i++)
            {
                todo += valor[i];
            }
            return todo;
        }
        private void Verificar_Elementales()
        {
            String mensaje1 = "";
            Verificar_trivialidad();
            for(int i=dataGridView1.RowCount-1;i>=0;i--)
            {
                if(dataGridView1.Rows[i].Cells[0].Value.ToString().Length>1)
                {
                    String izq = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    String der = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    int indice=i;
                    if(Sera_que_existe(izq,der,indice))
                    {
                        mensaje1 += "LA DF: \n" + dataGridView1.Rows[i].Cells[0].Value.ToString() + " ---->" + dataGridView1.Rows[i].Cells[2].Value.ToString() + "\n\n NO ES ELEMENTAL\n\n";
                        dataGridView1.Rows.RemoveAt(i);
                    }
                }
            }
            mensaje += mensaje1;
        }
        public bool Sera_que_existe(String izq,String der,int indice)
        {
            bool si = false;
            char[] izquierda = izq.ToCharArray();
            for(int i=0;i<indice;i++)
            {
                if(dataGridView1.Rows[i].Cells[0].Value.ToString().Length==1)
                {
                    for(int j=0;j<izquierda.Length;j++)
                    {
                        if ((dataGridView1.Rows[i].Cells[0].Value.ToString()[0].Equals(izquierda[j]))&&(dataGridView1.Rows[i].Cells[2].Value.ToString().Equals(der)))
                        {
                            si = true;
                            break;
                        }
                        else
                        {
                            si = false;
                        }
                    }
                }
                if (si)
                    break;
            }
            return si;
        }
        private void Verificar_trivialidad()
        {
            mensaje += "PRIMERO VERIFICAMOS TRIVIALIDAD\n\n";
            String mensaje1 = "";
            bool si = false;
            for(int i= dataGridView1.RowCount-1; i>=0;i--)
            {
                char[] pri = dataGridView1.Rows[i].Cells[0].Value.ToString().ToCharArray();
                char[] seg = dataGridView1.Rows[i].Cells[2].Value.ToString().ToCharArray();
                for(int j=0;j<pri.Length;j++)
                { 
                    if(seg[0]==pri[j])
                    {
                        si = true;
                        break;
                    }
                }
                if(si)
                {
                    mensaje1 += "EXISTE TRIVIALIDAD EN LA DF\n"+dataGridView1.Rows[i].Cells[0].Value.ToString()+" ----> "+ dataGridView1.Rows[i].Cells[2].Value.ToString()+"\n POR LO TANTO SE ELIMINA LA DF \n\n";
                    dataGridView1.Rows.RemoveAt(i);
                }
                si = false;
            }
            mensaje += mensaje1;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            while (sigue_habiendo())
            {
                int fila = 0;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    if (dataGridView1.Rows[i].Cells[2].Value.ToString().Length > 1)
                    {
                        fila = i;
                        break;
                    }
                }
                char[] valores = dataGridView1.Rows[fila].Cells[2].Value.ToString().ToCharArray();
                String valor= dataGridView1.Rows[fila].Cells[0].Value.ToString();
                dataGridView1.Rows.RemoveAt(fila);
                for(int i=0;i<valores.Length;i++)
                {
                    dataGridView1.Rows.Add(valor, null,valores[i]);
                }
            }
            //ELIMINAR IGUALES
            mensaje += "ELIMINAMOS DF QUE SEAN IGUALES \n";
            Eliminacion_dep_iguales();
            mensaje += "\nVERIFICAMOS SI NO EXISTEN DEPENDENCIAS ELEMENTALES\n";
            Verificar_Elementales();
            //ELIMINAR ELEMENTOS EXTRANOS POR LA IZQUIERDA
            mensaje += "ENTRAMOS AL PASO 1: \n Eliminar atributos extraños por la izquierda \n\n";
            Eliminar_por_la_izquierda();
            //ELIMINAMOS ELEMENTOS REDUNDANTES
            mensaje += "ENTRAMOS AL PASO 2: \n Eliminar dependencias funcionales redundantes \n\n";
            Eliminar_elementos_redundantes();
            mensaje += "AHORA COMO RESULTADO TENEMOS A NUESTRA DFmin = { ";
            String mensaje1 = "";
            for(int fina=0;fina<dataGridView1.RowCount;fina++)
            {
                mensaje1 += dataGridView1.Rows[fina].Cells[0].Value.ToString() + " ---> " + dataGridView1.Rows[fina].Cells[2].Value.ToString() + ",";
            }
            mensaje += mensaje1+"}\n";
            richTextBox1.Text = mensaje;
            mensaje = "";
        }
        public void Eliminar_elementos_redundantes()
        {
            String mensaje1="";
            CIERRE cierre;
            DataTable tabla = new DataTable();
            tabla.Columns.Add("COLUMNA 1");
            tabla.Columns.Add("COLUMNA 2");
            tabla.Columns.Add("COLUMNA 3");
            for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
            {
                for (int j = 0; j < dataGridView1.Rows.Count; j++)
                {
                    if (i != j)
                    {
                        tabla.Rows.Add(dataGridView1.Rows[j].Cells[0].Value.ToString(), "---->", dataGridView1.Rows[j].Cells[2].Value.ToString());
                    }
                    else
                    {
                        mensaje1 += "PARA LA DF: "+dataGridView1.Rows[i].Cells[0].Value.ToString()+" -----> " + dataGridView1.Rows[i].Cells[2].Value.ToString()+"\n\n";
                    }
                }
                cierre = new CIERRE(dataGridView1.Rows[i].Cells[0].Value.ToString(), dataGridView1.Rows[i].Cells[2].Value.ToString(), tabla, tabla);
                if (cierre.Existe_cierre())
                {
                    mensaje1 += cierre.getCierre()+"??--> SI entonces se elimina la DF\n\n";
                    dataGridView1.Rows.RemoveAt(i);
                }
                else
                {
                    mensaje1 += cierre.getCierre() + "??--> NO entonces no se elimina la DF\n\n";
                }
                tabla.Rows.Clear();
            }
            mensaje += mensaje1;
        }
        public void Eliminar_por_la_izquierda()
        {
            String mensaje1 = "";
            CIERRE cierre;
            DataTable tabla = new DataTable();
            tabla.Columns.Add("COLUMNA 1");
            tabla.Columns.Add("COLUMNA 2");
            tabla.Columns.Add("COLUMNA 3");
            for (int i=0;i<dataGridView1.Rows.Count;i++)
            {
                if(dataGridView1.Rows[i].Cells[0].Value.ToString().Length>1)
                {
                    mensaje1 += "VERIFICAMOS LA DF: \n" + dataGridView1.Rows[i].Cells[0].Value.ToString() + " ---->" + dataGridView1.Rows[i].Cells[2].Value.ToString() + "\n\n";
                    for (int j = 0; j < dataGridView1.Rows.Count; j++)
                    {
                        tabla.Rows.Add(dataGridView1.Rows[j].Cells[0].Value.ToString(), "---->", dataGridView1.Rows[j].Cells[2].Value.ToString());
                    }
                    String valor_final = "";
                    String valores = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    int reiniciador = 0;
                    while(reiniciador<valores.Length)
                    {
                        String ssss = valores[reiniciador] + "";
                        String compa = valores.Replace(ssss,"");
                        mensaje1 += "VERIFICAMOS PARA" + ssss + " SI ES EXTRAÑO\n";
                        cierre = new CIERRE(compa, dataGridView1.Rows[i].Cells[2].Value.ToString(), tabla, tabla);
                        if (cierre.Existe_cierre() == false)
                        {
                            mensaje1 += cierre.getCierre()+"?? --> NO \n\n";
                            valor_final += valores[reiniciador];
                        }
                        else
                        {
                            mensaje1 += cierre.getCierre() + "?? --> SI ENTONCES "+ssss+" ES EXTRAÑO ENTONCES SE ELIMINA \n\n";
                        }
                        reiniciador++;
                    }
                    dataGridView1.Rows[i].Cells[0].Value = valor_final;
                }
                tabla.Rows.Clear();
            }
            mensaje += mensaje1;
        }
        private void Dependencia_funcional_minima_Load(object sender, EventArgs e)
        {
            DataGridViewImageColumn coli = new DataGridViewImageColumn();
            coli.Image = imageList1.Images[0];
            dataGridView1.Columns.Add("COL1", "COLUMNA 1");
            dataGridView1.Columns.Add(coli);
            dataGridView1.Columns.Add("COL3", "COLUMNA 3");
            for(int i=0;i<tabla.Rows.Count;i++)
            {
                dataGridView1.Rows.Add(tabla.Rows[i].ItemArray[0].ToString(),null, tabla.Rows[i].ItemArray[2].ToString());
            }
        }
    }
}
