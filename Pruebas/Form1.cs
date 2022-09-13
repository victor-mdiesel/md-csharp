using System;
using System.Windows.Forms;

namespace Pruebas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            string[] arg = { "4fi57psr", "0"};           
            

            var reg = new RegWindows.Registro();
            string exe = reg.GetEjecutable("EXCEL.EXE");
            if (exe != string.Empty)
            {
                string ruta = @"c:\Users\victorvalenzuela\OneDrive - Maestranza Diesel S.A\Documentos\Cierre procesos hoja ruta.xlsx";
                AbrirAplicacion.AbrirAppWindows.Abrir(ruta, exe);
            }

        }
    }
}
