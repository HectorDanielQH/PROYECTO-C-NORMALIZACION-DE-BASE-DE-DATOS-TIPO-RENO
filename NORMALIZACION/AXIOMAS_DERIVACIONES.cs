using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
namespace NORMALIZACION
{
    public partial class AXIOMAS_DERIVACIONES : Form
    {
        CIERRE cierre;
        DataTable tablita = null,devuelve=null;
        public AXIOMAS_DERIVACIONES(DataTable tabla)
        {
            InitializeComponent();
            tablita = tabla;
            devuelve = tabla;
        }

        private void AXIOMAS_DERIVACIONES_Load(object sender, EventArgs e)
        {
            DataGridViewImageColumn COLUMNA = new DataGridViewImageColumn();
            COLUMNA.Image = imageList1.Images[0];
            dataGridView1.Columns.Add("COLUMNA 1", "COLUMNA 1");
            dataGridView1.Columns.Add(COLUMNA);
            dataGridView1.Columns.Add("COLUMNA 3", "COLUMNA 3");
            for(int i=0;i<tablita.Rows.Count;i++)
            {
                dataGridView1.Rows.Add(tablita.Rows[i].ItemArray[0].ToString(),null, tablita.Rows[i].ItemArray[2].ToString());
            }
        }
        public DataTable toma()
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
        public bool Selecciono()
        {
            bool sera = false;
            int cont = 0;
            for(int i=0;i<dataGridView1.Rows.Count;i++)
            {
                if (dataGridView1.Rows[i].Selected)
                    cont++;
            }
            if(cont==1)
            {
                sera = true;
            }
            return sera;
        }
        private void uSARPROYECTIVIDADToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("SEGURO QUE SELECCIONASTE LA FILA PARA USAR\nEL AXIOMA DE PROYECTIVIDAD?","PREGUNTA",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                if(Selecciono())
                {
                    int fila = 0;
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if (dataGridView1.Rows[i].Selected)
                        {
                            fila = i;
                            break;
                        }
                    }
                    String rich= "\nUSASTE UNA LA PROYECTIVIDAD\nAXIOMA DERIVADA D:1\n EN LA DEPENDENCIA: "+dataGridView1.Rows[fila].Cells[0].Value.ToString()+" -----> "+ dataGridView1.Rows[fila].Cells[2].Value.ToString() + "\n";
                    if(dataGridView1.Rows[fila].Cells[2].Value.ToString().Length>1)
                    {
                        char[] to = dataGridView1.Rows[fila].Cells[2].Value.ToString().ToCharArray();
                        String primera_dep = dataGridView1.Rows[fila].Cells[0].Value.ToString();
                        dataGridView1.Rows.RemoveAt(fila);
                        for(int i=0;i<to.Length;i++)
                        {
                            dataGridView1.Rows.Add(primera_dep,null,to[i]);
                            rich += primera_dep + " -----> " + to[i]+"\n";
                        }
                        String ri = richTextBox1.Text;
                        ri += rich;
                        richTextBox1.Text = ri;
                    }
                    else
                    {
                        MessageBox.Show("ツ NO SE PUEDE PROYECTAR SOLO TIENE UN ATRIBUTO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("ツ SELECCIONA BIEN", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("QUE ESPERAS? ☹ ☹ ☹ ☹ ☹ ☹ ☹ ☹ ☹", "PREGUNTA",MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
            catch
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            DataGridViewImageColumn COLUMNA = new DataGridViewImageColumn();
            COLUMNA.Image = imageList1.Images[0];
            dataGridView1.Columns.Add("COLUMNA 1", "COLUMNA 1");
            dataGridView1.Columns.Add(COLUMNA);
            dataGridView1.Columns.Add("COLUMNA 3", "COLUMNA 3");
            for (int i = 0; i < tablita.Rows.Count; i++)
            {
                dataGridView1.Rows.Add(devuelve.Rows[i].ItemArray[0].ToString(), null, tablita.Rows[i].ItemArray[2].ToString());
            }
            textBox1.Text = "";
            textBox2.Text = "";
            richTextBox1.Text = "";
        }

        private void sELECIONARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int si_mayo_a_dos_filas = 1;
            int cont = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Style.BackColor.Equals(Color.OrangeRed))
                {
                    si_mayo_a_dos_filas++;
                }
            }
            if (si_mayo_a_dos_filas <= 2)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Selected)
                        cont = i;
                }
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Rows[cont].Cells[i].Style.BackColor = Color.OrangeRed;
                }
                Desel.Enabled = true;
            }
            else
            {
                Desel.Enabled = true;
                MessageBox.Show("NO PUEDES SELECCIONAR MAS", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].Selected = true;
        }

        private void Desel_Click(object sender, EventArgs e)
        {
            int cont = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Selected)
                    cont = i;
            }
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Rows[cont].Cells[i].Style.BackColor = Color.White;
            }
            int contador = 0;
            for(int i=0;i<dataGridView1.Rows.Count;i++)
            {
                if(dataGridView1.Rows[i].Cells[0].Style.BackColor.Equals(Color.White))
                {
                    contador++;
                }
            }
            if(contador>0)
                Desel.Enabled = true;
            else
                Desel.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {

            //Analizar_axioma nuevo = new Analizar_axioma(richTextBox1,devuelve,textBox1.Text,textBox2.Text);
            //nuevo.Analizar();
        }

        private void uSARREFLEXIVIDADToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String rich = "\nUSASTE UNA LA REFLEXIVIDAD\nAXIOMA BASICO A:1\n DEPENDENCIA: " + textBox1.Text + "⊆" + textBox1.Text + " ENTONCES: " + textBox1.Text + " -----> " + textBox1.Text + "\n";
            dataGridView1.Rows.Add(textBox1.Text, null, textBox1.Text);
            String ri = richTextBox1.Text;
            ri += rich;
            richTextBox1.Text = ri;
        }

        private void uSARAUMENTATIVIDADToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("SEGURO QUE SELECCIONASTE LA FILA PARA USAR\nEL AXIOMA DE AUMENTATIVIDAD?", "PREGUNTA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (Selecciono())
                {
                    int fila = 0;
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if (dataGridView1.Rows[i].Selected)
                        {
                            fila = i;
                            break;
                        }
                    }
                    String valor = Interaction.InputBox("QUE DEPENDENCIA AUMENTARAS?\nHAZLO DE ESTA MANERA:\nX -> Y\nNO INLCUYAS NADA DE ESPACIOS HABRA ERRORES","MENSAJE","x -> y");
                    if (valor.Equals("") != true)
                    {
                        String rich = "\nUSASTE UNA LA AUMENTATIVIDAD\nAXIOMA BASICA A:2\n EN LA DEPENDENCIA: " + dataGridView1.Rows[fila].Cells[0].Value.ToString() + " -----> " + dataGridView1.Rows[fila].Cells[2].Value.ToString() + "\n";
                        String cadenaA = frente(valor);
                        String CadenaB = reves(valor);
                        rich += cadenaA+" ---> "+CadenaB+"\nPOR REGLA DE AUMENTATIVIDAD SERA:\n";
                        cadenaA += dataGridView1.Rows[fila].Cells[0].Value.ToString();
                        CadenaB += dataGridView1.Rows[fila].Cells[2].Value.ToString();
                        cadenaA = variables_repe(cadenaA);
                        CadenaB = variables_repe(CadenaB);
                        dataGridView1.Rows[fila].Cells[0].Value = cadenaA;
                        dataGridView1.Rows[fila].Cells[2].Value = CadenaB;
                        rich += cadenaA+" ----> "+CadenaB + "\n";
                        String ri = richTextBox1.Text;
                        ri += rich;
                        richTextBox1.Text = ri;
                    }
                }
                else
                {
                    MessageBox.Show("ツ SELECCIONA BIEN", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("QUE ESPERAS? ☹ ☹ ☹ ☹ ☹ ☹ ☹ ☹ ☹", "PREGUNTA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
        public String frente(String x)
        {
            String resul = "";
            char[] f = x.ToCharArray();
            for(int i=0;i<f.Length;i++)
            {
                if(((int)f[i]>=65 && (int)f[i] <= 90) || ((int)f[i] >= 97 && (int)f[i] <= 122))
                {
                    resul += f[i];
                }
                else
                {
                    break;
                }
            }
            return resul;
        }
        public String reves(String x)
        {
            String resul = "";
            char[] f = x.ToCharArray();
            for (int i = f.Length-1; i >=0; i--)
            {
                if (((int)f[i] >= 65 && (int)f[i] <= 90) || ((int)f[i] >= 97 && (int)f[i] <= 122))
                {
                    resul += f[i];
                }
                else
                {
                    break;
                }
            }
            return resul;
        }

        private void tRANSITIVIDADToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("PARA USAR ESTE AXIOMA BASICO DEBERAS HABER PINTADO POR LO MENOS 2 FILAS\n LO HICISTE?","INFORMACION",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                if (Pintaste())
                {
                    String[] todo = new String[4];
                    int[] fila = new int[2];
                    int up = 0,up_fila=0;
                    for(int i=0;i<dataGridView1.Rows.Count;i++)
                    {
                        if (dataGridView1.Rows[i].Cells[0].Style.BackColor.Equals(Color.OrangeRed))
                        {
                            fila[up_fila] = i;
                            todo[up] = dataGridView1.Rows[i].Cells[0].Value.ToString();
                            up++;
                            todo[up] = dataGridView1.Rows[i].Cells[2].Value.ToString();
                            up++;
                            up_fila++;
                        }
                    }
                    Analizar_axioma xxxx = new Analizar_axioma();
                    if(xxxx.Analizar_transitividad(todo[1], todo[2]))
                    {
                        dataGridView1.Rows.RemoveAt(fila[1]);
                        dataGridView1.Rows.RemoveAt(fila[0]);
                        dataGridView1.Rows.Add(todo[0], null, todo[3]);
                        String rich = "\nUSASTE UNA LA TRANSITIVIDAD\nAXIOMA BASICO A:3\n DEPENDENCIA: " +
                            todo[0] + " ---> " + todo[1]+" Y \n"+
                            todo[2] + " ---> " + todo[3] +"\nENTONCES: " +
                            todo[0] + " -----> " + todo[3] + "\n";
                        String ri = richTextBox1.Text;
                        ri += rich;
                        richTextBox1.Text = ri;
                    }
                    else
                    {
                        if (xxxx.Analizar_transitividad(todo[0],todo[3]))
                        {
                            dataGridView1.Rows.RemoveAt(fila[1]);
                            dataGridView1.Rows.RemoveAt(fila[0]);
                            dataGridView1.Rows.Add(todo[2], null, todo[1]);
                            String rich = "\nUSASTE UNA LA TRANSITIVIDAD\nAXIOMA BASICO A:3\n DEPENDENCIA: " +
                                todo[0] + " ---> " + todo[1] + " Y \n" +
                                todo[2] + " ---> " + todo[3] + "\nENTONCES: " +
                                todo[2] + " -----> " + todo[1] + "\n";
                            String ri = richTextBox1.Text;
                            ri += rich;
                            richTextBox1.Text = ri;
                        }
                        else {
                            MessageBox.Show("LA TRANSITIVIDAD NO ES EQUIVALENTE ES DECIR:\n" +
                                todo[1] + " no es igual a" + todo[2], "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("ツ SELECCIONA BIEN Y PINTA LAS FILAS ADECUADAS", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public bool Pintaste()
        {
            bool si = false;
            int con = 0;
            for(int i=0;i<dataGridView1.Rows.Count;i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Style.BackColor.Equals(Color.OrangeRed))
                    con++;
            }
            if (con == 2)
                si = true;
            else
                si = false;
            return si;
        }

        private void uNIONOADITIVIDADToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("PARA USAR ESTE AXIOMA DERIVADO DEBERAS HABER PINTADO POR LO MENOS 2 FILAS\n LO HICISTE?", "INFORMACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (Pintaste())
                {
                    String[] todo = new String[4];
                    int[] fila = new int[2];
                    int up = 0, up_fila = 0;
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if (dataGridView1.Rows[i].Cells[0].Style.BackColor.Equals(Color.OrangeRed))
                        {
                            fila[up_fila] = i;
                            todo[up] = dataGridView1.Rows[i].Cells[0].Value.ToString();
                            up++;
                            todo[up] = dataGridView1.Rows[i].Cells[2].Value.ToString();
                            up++;
                            up_fila++;
                        }
                    }
                    Analizar_axioma xxxx = new Analizar_axioma();
                    if (xxxx.Analizar_aditividad(todo[0], todo[2]))
                    {
                        dataGridView1.Rows.RemoveAt(fila[1]);
                        dataGridView1.Rows.RemoveAt(fila[0]);
                        String full = todo[1] + todo[3];
                        full = variables_repe(full);
                        dataGridView1.Rows.Add(todo[0], null, full);
                        String rich = "\nUSASTE UNA LA UNION O ADITIVIDAD\nAXIOMA DERIVADO D:2\n DEPENDENCIA: \n" +
                            todo[0] + " ---> " + todo[1] + "\n Y \n" +
                            todo[2] + " ---> " + todo[3] + "\nENTONCES: " +
                            todo[0] + " -----> " + full + "\n";
                        String ri = richTextBox1.Text;
                        ri += rich;
                        richTextBox1.Text = ri;
                    }
                    else
                    {
                        MessageBox.Show("LA UNION O ADITIVIDAD NO ES EQUIVALENTE ES DECIR:\n" +
                                todo[1] + " no esta en" + todo[2] + "\n O\n" + todo[3] + " no esta en" + todo[0], "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("ツ SELECCIONA BIEN Y PINTA LAS FILAS ADECUADAS", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pSEUDOTRANSITIVIDADToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("PARA USAR ESTE AXIOMA DERIVADO DEBERAS HABER PINTADO POR LO MENOS 2 FILAS\n LO HICISTE?", "INFORMACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (Pintaste())
                {
                    String[] todo = new String[4];
                    int[] fila = new int[2];
                    int up = 0, up_fila = 0;
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if (dataGridView1.Rows[i].Cells[0].Style.BackColor.Equals(Color.OrangeRed))
                        {
                            fila[up_fila] = i;
                            todo[up] = dataGridView1.Rows[i].Cells[0].Value.ToString();
                            up++;
                            todo[up] = dataGridView1.Rows[i].Cells[2].Value.ToString();
                            up++;
                            up_fila++;
                        }
                    }
                    Analizar_axioma xxxx = new Analizar_axioma();
                    if (xxxx.Analizar_pseudotransitividad(todo[1], todo[2]))
                    {
                        dataGridView1.Rows.RemoveAt(fila[1]);
                        dataGridView1.Rows.RemoveAt(fila[0]);
                        dataGridView1.Rows.Add(xxxx.Izquierda1(todo[0], todo[1], todo[2], todo[3]), null, todo[3]);
                        String rich = "\nUSASTE LA PSEUDOTRANSITIVIDAD\nAXIOMA DERIVADO D:3\n DEPENDENCIA: " +
                            todo[0] + " ---> " + todo[1] + " Y \n" +
                            todo[2] + " ---> " + todo[3] + "\nENTONCES: " +
                            xxxx.Izquierda1(todo[0], todo[1], todo[2], todo[3]) + " -----> " + todo[3] + "\n";
                        String ri = richTextBox1.Text;
                        ri += rich;
                        richTextBox1.Text = ri;
                    }
                    else
                    {
                        if (xxxx.Analizar_pseudotransitividad(todo[3], todo[0]))
                        {
                            dataGridView1.Rows.RemoveAt(fila[1]);
                            dataGridView1.Rows.RemoveAt(fila[0]);
                            dataGridView1.Rows.Add(xxxx.Izquierda2(todo[0], todo[1], todo[2], todo[3]), null, todo[1]);
                            String rich = "\nUSASTE LA PSEUDOTRANSITIVIDAD\nAXIOMA DERIVADO D:3\n DEPENDENCIA: " +
                                todo[0] + " ---> " + todo[1] + " Y \n" +
                                todo[2] + " ---> " + todo[3] + "\nENTONCES: " +
                                xxxx.Izquierda2(todo[0], todo[1], todo[2], todo[3]) + " -----> " + todo[1] + "\n";
                            String ri = richTextBox1.Text;
                            ri += rich;
                            richTextBox1.Text = ri;
                        }
                        else
                        {
                            MessageBox.Show("LA TRANSITIVIDAD NO ES EQUIVALENTE ES DECIR:\n" +
                                todo[1] + " no esta en" + todo[2]+"\n O\n"+todo[3] + " no esta en" + todo[0], "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("ツ SELECCIONA BIEN Y PINTA LAS FILAS ADECUADAS", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cierre = new CIERRE(textBox1.Text,textBox2.Text,toma(),toma());
            if(cierre.Existe_cierre())
            {

                richTextBox1.Text = cierre.getCierre() + " ?? ---> si \nENTONCES COMO EXISTE CIERRE, EXISTE LA DEMOSTRACION PARA ESTA DEPENDENCIA FUNCIONAL. \nSE REALIZARAN LOS SIGUIENTES PASOS: \n";
            }
            else
            {
                richTextBox1.Text = cierre.getCierre() + " ?? ---> no \nENTONCES COMO  NO EXISTE CIERRE, NO EXISTE LA DEMOSTRACION PARA ESTA DEPENDENCIA FUNCIONAL. \nNO EXISTE";
            }

        }
    }
}
