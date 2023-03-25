using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace etiquetas2
{
    public partial class TSCPrint : Form
    {
        Paciente paciente;

        [DllImport("TSCLIB.dll", EntryPoint = "about")]
        public static extern bool about();

        [DllImport("TSCLIB.dll", EntryPoint = "openport")]
        public static extern bool openport(string printer);

        [DllImport("TSCLIB.dll", EntryPoint = "barcode")]
        public static extern int barcode(string x, string y, string type,
        string height, string readable, string rotation,
        string narrow, string wide, string code);

        [DllImport("TSCLIB.dll", EntryPoint = "clearbuffer")]
        public static extern int clearbuffer();

        [DllImport("TSCLIB.dll", EntryPoint = "closeport")]
        public static extern int closeport();

        [DllImport("TSCLIB.dll", EntryPoint = "downloadpcx")]
        public static extern int downloadpcx(string filename, string image_name);

        [DllImport("TSCLIB.dll", EntryPoint = "formfeed")]
        public static extern int formfeed();

        [DllImport("TSCLIB.dll", EntryPoint = "nobackfeed")]
        public static extern int nobackfeed();

        [DllImport("TSCLIB.dll", EntryPoint = "printerfont")]
        public static extern int printerfont(string x, string y, string fonttype,
        string rotation, string xmul, string ymul,
        string text);

        [DllImport("TSCLIB.dll", EntryPoint = "printlabel")]
        public static extern int printlabel(string set, string copy);

        [DllImport("TSCLIB.dll", EntryPoint = "sendcommand")]
        public static extern int sendcommand(string printercommand);

        [DllImport("TSCLIB.dll", EntryPoint = "setup")]
        public static extern int setup(string width, string height,
        string speed, string density,
        string sensor, string vertical,
        String offset);

        [DllImport("TSCLIB.dll", EntryPoint = "windowsfont")]
        public static extern int windowsfont(int x, int y, int fontheight,
        int rotation, int fontstyle, int fontunderline,
        string szFaceName, string content);
        public TSCPrint(Paciente paciente)
        {
            this.paciente = paciente;
            InitializeComponent();
            preencheValores();
            //about();
        }

        private void preencheValores()
        {
            txtNomePaciente.Text = this.paciente.Nome;
            txtNomeSocial.Text = this.paciente.NomeSocial;
            txtNomeMae.Text = this.paciente.NomeMae;
            txtNumeroProntuario.Text = this.paciente.Prontuario.ToString();
            txtDataNascimento.Text = this.paciente.DataNascimento.ToString("dd/MM/yyyy");


        }


        private void button1_Click(object sender, EventArgs e)
        {
            string nomePaciente, nomeMae, numeroProntuario, dataNascimento, alergias, comorbidades, nomeSocial;
            nomePaciente = txtNomePaciente.Text;
            nomeMae = txtNomeMae.Text;
            numeroProntuario = txtNumeroProntuario.Text;
            dataNascimento = txtDataNascimento.Text;
            alergias = txtAlergias.Text;
            comorbidades = txtComorbidades.Text;
            nomeSocial = txtNomeSocial.Text;


            clearbuffer(); // Clear image buffer

            string cmdNomepaciente = "";
            if (nomeSocial.Length > 0) {
                cmdNomepaciente = string.Concat("TEXT 270,30, \"2\",90,1,1, \"PACIENTE: " + nomePaciente + " (" + nomeSocial + ") \"");
            }
            else {
                cmdNomepaciente = string.Concat("TEXT 270,30, \"2\",90,1,1, \"NOME DO PACIENTE: " + nomePaciente + "\"");
            }

            string cmdDataNascimento = string.Concat("TEXT 240, 30, \"2\", 90, 1, 1, \" NASC.:" + dataNascimento + "\"");
            string cmdNomeMae = string.Concat("TEXT 240, 350, \"2\", 90, 1, 1, \"MAE: " + nomeMae + "\"");
            string cmdAlergias = string.Concat("TEXT 210, 30, \"2\", 90, 1, 1, \"ALERGIAS: " + alergias + "\"");
            string cmdComorbidades = string.Concat("TEXT 180, 30, \"2\", 90, 1, 1, \"COMORBIDADES: " + comorbidades + "\"");


            string cmdNumeroProntuario = string.Concat("TEXT 150, 30, \"2\", 90, 1, 1, \"#" + numeroProntuario + "\"");
   
            string cmdRiscoQueda = string.Concat("TEXT 150, 350,\"2\",90,1,1,\"RISCO DE QUEDA\"");


            //SOMENTE PARA TESTES
            /*
			string cmdNomepaciente =	string.Concat("TEXT 280,70, \"2\",90,1,1,\"A1\"");
			string cmdDataNascimento =	string.Concat("TEXT 240,70, \"2\",90,1,1,\"A2\"");
			string cmdNomeMae =			string.Concat("TEXT 200,70, \"2\",90,1,1,\"A3\"");
			string cmdNomeSocial =		string.Concat("TEXT 160,70, \"2\",90,1,1,\"A4\"");
			string cmdNumeroProntuario= string.Concat("TEXT 280,370,\"2\",90,1,1,\"A5\"");
			string cmdAlergias =		string.Concat("TEXT 250,370,\"2\",90,1,1,\"A6\"");
			string cmdAlergias2 =		string.Concat("TEXT 220,370,\"2\",90,1,1,\"A7\"");
			string cmdComorbidades =	string.Concat("TEXT 190,370,\"2\",90,1,1,\"A8\"");
			string cmdComorbidades2 =	string.Concat("TEXT 160,370,\"2\",90,1,1,\"A9\"");
			string teste = string.Concat("TEXT 180,500, \"2\",90,1,1,\"uuuuuuu\"");
			*/
            /*
			MessageBox.Show("\"" + cmdNomepaciente + "\"");
			MessageBox.Show("Data de Nascimento: " + cmdDataNascimento);
			MessageBox.Show("Nome da mãe: " + cmdNomeMae);
			MessageBox.Show("Nome Social" + cmdNomeSocial);
			MessageBox.Show("Prontuário: " + cmdNumeroProntuario);
			MessageBox.Show("Alergias: " + cmdAlergias);
			MessageBox.Show("Comorbidades: " + cmdComorbidades);
			*/
            openport("TSC");
            //sendcommand("AUTODETECT");

            sendcommand("DIRECTION 0");
            sendcommand("CLS");
            //	sendcommand(teste);
            sendcommand(cmdNomepaciente);
            sendcommand(cmdDataNascimento);
            sendcommand(cmdNomeMae);
            sendcommand(cmdNumeroProntuario);
            sendcommand(cmdAlergias);
            sendcommand(cmdComorbidades);
            if (chkqueda.Checked == true)
                sendcommand(cmdRiscoQueda);

            sendcommand("PRINT 1,1");
            closeport();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openport("TSC");
            sendcommand("GAPDETECT");
            closeport();
            openport("TSC");
            sendcommand("AUTODETECT");
            closeport();
        }

        private void btnResetar_Click(object sender, EventArgs e)
        {
            openport("TSC");
            sendcommand("INITIALPRINTER");
            closeport();
        }
    }
}
