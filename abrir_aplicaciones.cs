using System;
using System.Diagnostics;

namespace AbrirAplicacion
{
    public class AbrirApp
    {
        private readonly string Aplicacion;
        private readonly string[] Parametros;
        private const string Espacio = " ";
        private readonly bool ConClaveEnParametro;
        private string Clave;
        public string Mensaje { get; set; }
        public string RutaEjecutables { get; set; }

        private void Inicializar()
        {
            Mensaje = string.Empty;
            Clave = Utilidades.BaseDatos.Parametro(Utilidades.BaseDatos.Parametros.ArgumentoAbrirApp);
        }
        public AbrirApp(string nombreAplicacion, string[] parametros, bool conClaveenParam = true)
        {
            Inicializar();            
            Aplicacion = nombreAplicacion ;
            Parametros = parametros;
            ConClaveEnParametro = conClaveenParam;            
            Abrir();            
        }

        public AbrirApp(string nombreAplicacion, string[] parametros, string ruta, bool conClaveenParam = true)
        {
            Inicializar();            
            Aplicacion = nombreAplicacion;
            Parametros = parametros;
            ConClaveEnParametro = conClaveenParam;
            Abrir(ruta);
        }

        public AbrirApp()
        {
            Inicializar();            
        }

        private void Abrir()
        {
            try
            {
                string archivo = AppDomain.CurrentDomain.BaseDirectory;
                archivo += Aplicacion;
                archivo=@"" + archivo + string.Empty;
                string paramsApertura = string.Empty;
                RutaEjecutables = archivo;
                int i = 0;
                for (i = 0; i < Parametros.Length; i++) 
                {
                    paramsApertura += Parametros[i] + Espacio;                    
                }
                if (ConClaveEnParametro)
                    paramsApertura = Clave + Espacio + paramsApertura;
                paramsApertura = paramsApertura.TrimEnd();

                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        Arguments = paramsApertura,
                        ErrorDialog = true,
                        FileName = archivo
                    }
                };
                process.Start();
                process.WaitForInputIdle(100);                        
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
            }
           
        }
        private void Abrir(string ruta)
        {
            try
            {                
                ruta += Aplicacion;
                ruta = @"" + ruta + string.Empty;
                string paramsApertura = string.Empty;
                
                ProcessStartInfo psi = new ProcessStartInfo();
                Process p;
                int i = 0;
                for (i = 0; i < Parametros.Length; i++)
                {
                    paramsApertura += Parametros[i] + Espacio;
                }
                if (ConClaveEnParametro)
                    paramsApertura = Clave + Espacio + paramsApertura;
                paramsApertura = paramsApertura.TrimEnd();
                psi.Arguments = paramsApertura;
                psi.ErrorDialog = true;
                psi.FileName = ruta;
                p = Process.Start(psi);
                p.WaitForInputIdle(100);               
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
            }

        }


    }    

    public static class AbrirAppWindows
    {
        public static string Mensaje { get; set; }
        public static void Abrir(string rutaArchivo, string rutaAplicacion)
        {
            try
            {
                string paramsApertura = string.Empty;
                ProcessStartInfo psi = new ProcessStartInfo();
                Process p;
                psi.UseShellExecute = false;
                psi.Arguments = "\"" + rutaArchivo + "\"";
                psi.ErrorDialog = true;
                psi.FileName = rutaAplicacion;
                p = Process.Start(psi);
                p.Close();

            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
            }

        }

        
    }
}
