using System;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.IO;

namespace ActionScreenShot
{
    class Program
    {
        public static string date = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_").Replace(" ", "_"); // Obtener la fecha
        public static string dateFolder = DateTime.Now.ToShortDateString().Replace("/", "-");
        public static string path = AppDomain.CurrentDomain.BaseDirectory; // Definir la ruta del proyecto
        public static string workDirectory = path + "\\Screenshots\\" + dateFolder; // Directorio para repositorio de imagenes
        public static string fileName = path + "\\Screenshots\\" + dateFolder + "\\screenshot_" + date + ".jpg"; // Guardar imagen en el directorio especifico
        public static string imagenReciente = "";


        public static void CapturarImagen()
        {
            try
            {
                // Hacer captura de pantalla
                Bitmap printscreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format24bppRgb);
                Graphics graphics = Graphics.FromImage(printscreen as Image);
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
                graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
                graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size);

                //Guardar captura de pantalla
                printscreen.Save(fileName, ImageFormat.Jpeg);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error encontrado: {0}", e.ToString());
            }
            
        }


        public static void ObtenerImagenReciente()
        {
            try
            {
                // Ubicar directorio de trabajo
                DirectoryInfo directory = new DirectoryInfo(workDirectory);

                // Ordenar imagenes de forma descendente
                FileInfo[] allFiles = directory.GetFiles().OrderByDescending(f => f.CreationTime).ToArray();                

                foreach (var f in allFiles)
                {
                    imagenReciente = f.Name;
                    break;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error encontrado: {0}", e.ToString());
            }            
        }


        public static void EscalarImagen()
        {

        }


        static void Main(string[] args)
        {
            try
            {          
                // Comprobar existencia del repositorio
                if (!Directory.Exists(workDirectory))
                {
                    // Crear repositorio si no existe
                    Directory.CreateDirectory(workDirectory);
                    CapturarImagen();
                    ObtenerImagenReciente();
                }
                else
                {
                    CapturarImagen();
                    ObtenerImagenReciente();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error encontrado: {0}", e.ToString());
            }
        }        
    }
}