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
    public partial class Form1 : Form
    {
        public int fila = 0;
        public Form1()
        {
            InitializeComponent();
        }
        public bool no_existe(char comparacion)
        {
            bool valor = true;
            for(int i=0;i<listBox1.Items.Count;i++)
            {
                if (listBox1.Items[i].ToString()[0] == comparacion)
                    valor = false;
            }
            return valor;
        }
        public void conjuntos(String a, String b)
        {
            String todo = a + b;
            char[] veremos = todo.ToCharArray();
            for(int i=0;i<veremos.Length;i++)
            {
                if(no_existe(veremos[i]))
                {
                    listBox1.Items.Add(veremos[i]);
                }
            }
        }
        public bool existe_la_dp()
        {
            bool si = false;
            for (int i=0;i<dataGridView1.Rows.Count;i++)
            {
                if(textBox1.Text.Length == dataGridView1.Rows[i].Cells[0].Value.ToString().Length)
                {
                    if (textBox2.Text.Length == dataGridView1.Rows[i].Cells[2].Value.ToString().Length)
                    {
                        char[] uno = textBox1.Text.ToCharArray();
                        char[] dos = textBox2.Text.ToCharArray();
                        char[] uno_uno = dataGridView1.Rows[i].Cells[0].Value.ToString().ToCharArray();
                        char[] dos_dos = dataGridView1.Rows[i].Cells[2].Value.ToString().ToCharArray();
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
            for(int i=0;i<uno.Length;i++)
            {
                for(int j=0;j<uno_uno.Length;j++)
                {
                    if(uno[i].Equals(uno_uno[j]))
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
        private void button1_Click(object sender, EventArgs e)
        {
            if (existe_la_dp())
            {
                MessageBox.Show("YA EXISTE ESTA DEPENDENCIA", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (button1.Text.Equals("MODIFICAR"))
                {
                    dataGridView1.Rows[fila].Cells[0].Value = textBox1.Text;
                    dataGridView1.Rows[fila].Cells[2].Value = textBox2.Text;
                    String todo = "";
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        todo += dataGridView1.Rows[i].Cells[0].Value.ToString() + dataGridView1.Rows[i].Cells[2].Value.ToString();
                    }
                    conjuntos(todo, "");
                    textBox1.Text = "";
                    textBox2.Text = "";
                    button1.Text = "ACEPTAR";
                }
                else
                {
                    dataGridView1.Rows.Add(textBox1.Text, null, textBox2.Text, null, null);
                    conjuntos(textBox1.Text, textBox2.Text);
                    textBox1.Text = "";
                    textBox2.Text = "";
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("PRIMERO","COLUMNA 1");
            DataGridViewImageColumn COLUMNA = new DataGridViewImageColumn();
            COLUMNA.Image = imageList1.Images[0];
            DataGridViewButtonColumn modificar = new DataGridViewButtonColumn();
            DataGridViewButtonColumn eliminar = new DataGridViewButtonColumn();
            modificar.UseColumnTextForButtonValue = true;
            eliminar.UseColumnTextForButtonValue = true;
            modificar.Text = "MODIFICAR";
            eliminar.Text = "ELIMINAR";
            dataGridView1.Columns.Add(COLUMNA);
            dataGridView1.Columns.Add("TERCERO", "COLUMNA 3");
            dataGridView1.Columns.Add(modificar);
            dataGridView1.Columns.Add(eliminar);
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.ColumnIndex==3)
            {
                listBox1.Items.Clear();
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                fila = e.RowIndex;
                button1.Text = "MODIFICAR";
            }
            else
            {
                if(e.ColumnIndex == 4)
                {
                    listBox1.Items.Clear();
                    dataGridView1.Rows.RemoveAt(e.RowIndex);
                    String todo = "";
                    for(int i=0;i<dataGridView1.RowCount;i++)
                    {
                        todo += dataGridView1.Rows[i].Cells[0].Value.ToString() + dataGridView1.Rows[i].Cells[2].Value.ToString();
                    }
                    conjuntos(todo , "");
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataTable tabla = new DataTable();
            tabla.Columns.Add("COLUMNA 1");
            tabla.Columns.Add("COLUMNA 2");
            tabla.Columns.Add("COLUMNA 3");
            for (int i=0;i<dataGridView1.Rows.Count;i++)
            {
                tabla.Rows.Add(dataGridView1.Rows[i].Cells[0].Value.ToString(), "----->", dataGridView1.Rows[i].Cells[2].Value.ToString());
            }
            AXIOMAS_DERIVACIONES nuevo = new AXIOMAS_DERIVACIONES(tabla);
            nuevo.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataTable tabla = new DataTable();
            tabla.Columns.Add("COLUMNA 1");
            tabla.Columns.Add("COLUMNA 2");
            tabla.Columns.Add("COLUMNA 3");
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                tabla.Rows.Add(dataGridView1.Rows[i].Cells[0].Value.ToString(), "----->", dataGridView1.Rows[i].Cells[2].Value.ToString());
            }
            EQUIVALENCIA_DE_CONJUNTOS equi = new EQUIVALENCIA_DE_CONJUNTOS(tabla);
            equi.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataTable tabla = new DataTable();
            tabla.Columns.Add("COLUMNA 1");
            tabla.Columns.Add("COLUMNA 2");
            tabla.Columns.Add("COLUMNA 3");
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                tabla.Rows.Add(dataGridView1.Rows[i].Cells[0].Value.ToString(), "----->", dataGridView1.Rows[i].Cells[2].Value.ToString());
            }
            Dependencia_funcional_minima dep = new Dependencia_funcional_minima(tabla);
            dep.ShowDialog();
        }
    }
}
