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
    public partial class EQUIVALENCIA_DE_CONJUNTOS : Form
    {
        public int fila = 0;
        DataTable tablita, devuelve;
        public EQUIVALENCIA_DE_CONJUNTOS(DataTable tabla)
        {
            InitializeComponent();
            tablita = tabla;
            devuelve = tabla;

        }
        public bool existe_la_dp()
        {
            bool si = false;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if (textBox1.Text.Length == dataGridView2.Rows[i].Cells[0].Value.ToString().Length)
                {
                    if (textBox2.Text.Length == dataGridView2.Rows[i].Cells[2].Value.ToString().Length)
                    {
                        char[] uno = textBox1.Text.ToCharArray();
                        char[] dos = textBox2.Text.ToCharArray();
                        char[] uno_uno = dataGridView2.Rows[i].Cells[0].Value.ToString().ToCharArray();
                        char[] dos_dos = dataGridView2.Rows[i].Cells[2].Value.ToString().ToCharArray();
                        if (si_uno_con_uno(uno, uno_uno))
                        {
                            if (si_uno_con_uno(dos, dos_dos))
                            {
                                si = true;
                            }
                            else
                            {
                                si = false;
                            }
                        }
                        else
                        {
                            si = false;
                        }
                    }
                }
            }
            return si;
        }
        private bool si_uno_con_uno(char[] uno, char[] uno_uno)
        {
            bool si = false;
            int cont = 0;
            for (int i = 0; i < uno.Length; i++)
            {
                for (int j = 0; j < uno_uno.Length; j++)
                {
                    if (uno[i].Equals(uno_uno[j]))
                    {
                        cont++;
                    }
                }
                if (cont == 0)
                {
                    si = false;
                    break;
                }
                else
                {
                    si = true;
                }
                cont = 0;
            }
            return si;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (existe_la_dp())
            {
                MessageBox.Show("YA EXISTE ESTA DEPENDENCIA", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (button2.Text.Equals("MODIFICAR"))
                {
                    dataGridView2.Rows[fila].Cells[0].Value = textBox1.Text;
                    dataGridView2.Rows[fila].Cells[2].Value = textBox2.Text;
                    textBox1.Text = "";
                    textBox2.Text = "";
                    button2.Text = "ACEPTAR";
                }
                else
                {
                    dataGridView2.Rows.Add(textBox1.Text, null, textBox2.Text, null, null);
                    textBox1.Text = "";
                    textBox2.Text = "";
                }
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                textBox1.Text = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox2.Text = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
                fila = e.RowIndex;
                button2.Text = "MODIFICAR";
            }
            else
            {
                if (e.ColumnIndex == 4)
                    dataGridView2.Rows.RemoveAt(e.RowIndex);
            }
        }
        public DataTable toma_grid1()
        {
            DataTable tabla222 = new DataTable();
            tabla222.Columns.Add("COLUMNA 1");
            tabla222.Columns.Add("COLUMNA 2");
            tabla222.Columns.Add("COLUMNA 3");
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                tabla222.Rows.Add(dataGridView1.Rows[i].Cells[0].Value.ToString(), "----->", dataGridView1.Rows[i].Cells[2].Value.ToString());
            }
            return tabla222;
        }
        public DataTable toma_grid2()
        {
            DataTable tabla222 = new DataTable();
            tabla222.Columns.Add("COLUMNA 1");
            tabla222.Columns.Add("COLUMNA 2");
            tabla222.Columns.Add("COLUMNA 3");
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                tabla222.Rows.Add(dataGridView2.Rows[i].Cells[0].Value.ToString(), "----->", dataGridView2.Rows[i].Cells[2].Value.ToString());
            }
            return tabla222;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            String texto = "SIGUIENDO EL ALGORITMO DE EQUIVALENCIA \n\n1.- Si para toda dependencia X→ Y de DF2 se cumple Y ⊆ X + DF1\n\n";
            bool si = false,salto=false;
            for(int i=0;i<dataGridView2.Rows.Count;i++)
            {
                for(int j = 0; j < dataGridView1.Rows.Count; j++)
                {
                    CIERRE cierre = new CIERRE(dataGridView2.Rows[i].Cells[0].Value.ToString(), dataGridView2.Rows[i].Cells[2].Value.ToString(),toma_grid1(),toma_grid1());
                    if(cierre.Existe_cierre())
                    {
                        texto +="PARA LA DEPENDENCIA: "+ dataGridView2.Rows[i].Cells[0].Value.ToString()+" ----> "+ dataGridView2.Rows[i].Cells[2].Value.ToString()+"\n";
                        texto += dataGridView2.Rows[i].Cells[0].Value.ToString() + "+ = "+cierre.getCierre()+"\n";
                        texto += dataGridView2.Rows[i].Cells[2].Value.ToString() + "?? Existe en " + dataGridView2.Rows[i].Cells[0].Value.ToString() + "+ ---> " + "SI\n\n";
                        salto = false;
                        si = true;
                        break;
                    }
                    else
                    {
                        texto += "PARA LA DEPENDENCIA: " + dataGridView2.Rows[i].Cells[0].Value.ToString() + " ----> " + dataGridView2.Rows[i].Cells[2].Value.ToString() + "\n";
                        texto += dataGridView2.Rows[i].Cells[0].Value.ToString() + "+ = " + cierre.getCierre() + "\n";
                        texto += dataGridView2.Rows[i].Cells[2].Value.ToString() + "?? Existe en " + dataGridView2.Rows[i].Cells[0].Value.ToString() + "+ ---> " + "NO\n\n";
                        salto = true;
                        break;
                    }
                }
                if (salto)
                {
                    si = false;
                    break;
                }
            }
            if(si)
            {
                texto += "<---------------------------------------------------------------->\n\n2.- Si para toda dependencia X→ Y de DF2 se cumple Y ⊆ X + DF1\n\n";
                si = false;
                salto = false;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView2.Rows.Count; j++)
                    {
                        CIERRE cierre = new CIERRE(dataGridView1.Rows[i].Cells[0].Value.ToString(), dataGridView1.Rows[i].Cells[2].Value.ToString(), toma_grid2(), toma_grid2());
                        if (cierre.Existe_cierre())
                        {
                            texto += "PARA LA DEPENDENCIA: " + dataGridView1.Rows[i].Cells[0].Value.ToString() + " ----> " + dataGridView1.Rows[i].Cells[2].Value.ToString() + "\n";
                            texto += dataGridView1.Rows[i].Cells[0].Value.ToString() + "+ = " + cierre.getCierre() + "\n";
                            texto += dataGridView1.Rows[i].Cells[2].Value.ToString() + "?? Existe en " + dataGridView1.Rows[i].Cells[0].Value.ToString() + "+ ---> " + "SI\n\n";
                            salto = false;
                            si = true;
                            break;
                        }
                        else
                        {
                            texto += "PARA LA DEPENDENCIA: " + dataGridView1.Rows[i].Cells[0].Value.ToString() + " ----> " + dataGridView1.Rows[i].Cells[2].Value.ToString() + "\n";
                            texto += dataGridView1.Rows[i].Cells[0].Value.ToString() + "+ = " + cierre.getCierre() + "\n";
                            texto += dataGridView1.Rows[i].Cells[2].Value.ToString() + "?? Existe en " + dataGridView1.Rows[i].Cells[0].Value.ToString() + "+ ---> " + "NO\n\n";
                            salto = true;
                            break;
                        }
                    }
                    if (salto)
                    {
                        si = false;
                        break;
                    }
                }
                if(si)
                {
                    texto += "\n \nSi se cumplen 1 y 2, DF1 y DF2 son mutuamente recubrimientos y, por tanto,son equivalentes";
                }
                else
                {
                    texto += "\n \n LOS CONJUNTOS NO SON EQUIVALENTES";
                }
            }
            else
            {
                texto += "\n \n LOS CONJUNTOS NO SON EQUIVALENTES";
            }
            richTextBox1.Text = texto;
        }

        private void EQUIVALENCIA_DE_CONJUNTOS_Load(object sender, EventArgs e)
        {
            dataGridView2.Columns.Add("PRIMERO", "COLUMNA 1");
            DataGridViewImageColumn COLUMNA = new DataGridViewImageColumn();
            COLUMNA.Image = imageList1.Images[0];
            DataGridViewButtonColumn modificar = new DataGridViewButtonColumn();
            DataGridViewButtonColumn eliminar = new DataGridViewButtonColumn();
            modificar.UseColumnTextForButtonValue = true;
            eliminar.UseColumnTextForButtonValue = true;
            modificar.Text = "MODIFICAR";
            eliminar.Text = "ELIMINAR";
            dataGridView2.Columns.Add(COLUMNA);
            dataGridView2.Columns.Add("TERCERO", "COLUMNA 3");
            dataGridView2.Columns.Add(modificar);
            dataGridView2.Columns.Add(eliminar);
            /*---------------------------------------*/
            dataGridView1.Columns.Add("PRIMERO", "COLUMNA 1");
            DataGridViewImageColumn COLUMNA1 = new DataGridViewImageColumn();
            COLUMNA1.Image = imageList1.Images[0];
            dataGridView1.Columns.Add(COLUMNA1);
            dataGridView1.Columns.Add("TERCERO", "COLUMNA 3");
            for(int i=0;i<tablita.Rows.Count;i++)
            {
                dataGridView1.Rows.Add(tablita.Rows[i].ItemArray[0], null, tablita.Rows[i].ItemArray[2]);
            }
        }
    }
}
