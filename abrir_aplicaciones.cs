using System;
using System.Diagnostics;
using System.IO;

namespace AbrirAplicacion
{
    public class abrir_aplicaciones
    {
        private readonly string Aplicacion;
        private readonly int Numero;
                
        private readonly string Clave;
        public string Tipo_Orden { get; set; }
        public int NumeroHRU { get; set; }

        public abrir_aplicaciones(string NombreAplicacion)
        {
            Clave = Utilidades.BaseDatos.Parametro(Utilidades.BaseDatos.Parametros.ArgumentoAbrirApp);
            Aplicacion = NombreAplicacion;
        }
        public abrir_aplicaciones(string NombreAplicacion, int NumeroDocumento)
        {
            Clave = Utilidades.BaseDatos.Parametro(Utilidades.BaseDatos.Parametros.ArgumentoAbrirApp);
            Aplicacion = NombreAplicacion;
            Numero = NumeroDocumento;
        }
        public abrir_aplicaciones(string NombreAplicacion, string NumeroDocumento, string RutCliente)
        {
            Clave = Utilidades.BaseDatos.Parametro(Utilidades.BaseDatos.Parametros.ArgumentoAbrirApp);
            Aplicacion = NombreAplicacion;            
        }
        public void AbrirAplicacion(string[] args, ref string msg, bool sinClave = false)
        {
            try
            {
                string archivo = AppDomain.CurrentDomain.BaseDirectory;
                archivo += Aplicacion;
                string paramsApertura = "";
                string numeroDoc = Numero.ToString();

                ProcessStartInfo psi = new ProcessStartInfo();
                Process p;
                if (args.Length == 0)
                    paramsApertura = Clave;
                else if (args.Length == 1)
                    if (sinClave)
                        paramsApertura = args[0];
                    else
                        paramsApertura = Clave + " " + args[0];
                else if (args.Length == 2)
                {
                    if (sinClave)
                        paramsApertura = args[0] + " " + Clave + " " + args[1];
                    else
                        paramsApertura = Clave + " " + args[0] + " " + args[1];
                }

                else if (args.Length == 3)

                    if (sinClave)
                        paramsApertura = args[0] + " " + Clave + " " + args[1] + " " + args[2];
                    else
                        paramsApertura = Clave + " " + args[0] + " " + args[1] + " " + args[2];


                //paramsApertura = Clave + " " + args[0] + " " + args[1] + " " + args[2];
                else if (args.Length == 4)
                {
                    if (sinClave)
                        paramsApertura = args[0] + " " + args[1] + " " + args[2] + " " + args[3];
                    else
                        paramsApertura = Clave + " " + args[0] + " " + args[1] + " " + args[2] + " " + args[3];
                }

                else
                {
                    msg = "Demasiados parámetros";
                    return;
                }
                                    
                psi.Arguments = paramsApertura;
                psi.ErrorDialog = true;
                psi.FileName = archivo;

                p = Process.Start(psi);
                p.WaitForInputIdle(100);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
        }
        public void AbrirAplicacion3(bool conNumero, ref string msg)
        {
            try
            {
                string archivo = AppDomain.CurrentDomain.BaseDirectory + @"\";
                archivo += Aplicacion;
                string paramsApertura = "";
                string numeroDoc = Numero.ToString();

                ProcessStartInfo psi = new ProcessStartInfo();
                Process p;
                if (conNumero)
                    paramsApertura = Clave + " " + numeroDoc;
                else
                    paramsApertura = Clave;
                psi.Arguments = paramsApertura;
                psi.ErrorDialog = true;
                psi.FileName = archivo;

                p = Process.Start(psi);
                p.WaitForInputIdle(100);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
        }
        public void AbrirAplicacion2(ref string msg)
        {
            try
            {
                string archivo = AppDomain.CurrentDomain.BaseDirectory;                
                archivo += Aplicacion;
                string paramsApertura = "";
                string numeroDoc = Numero.ToString();

                ProcessStartInfo psi = new ProcessStartInfo();
                Process p;
                paramsApertura = Clave;
                psi.Arguments = paramsApertura;
                psi.ErrorDialog = true;
                psi.FileName = archivo;                
                p = Process.Start(psi);               
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
        }       
    }

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
                
            }

        }

        
    }
}
