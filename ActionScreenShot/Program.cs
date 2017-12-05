using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.IO;

namespace ActionScreenShot
{
    class Program
    {
        public static string date = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_").Replace(" ", "_"); // Obtener la fecha
        public static string path = AppDomain.CurrentDomain.BaseDirectory; // Definir la ruta del proyecto
        public static string workDirectory = path + "\\Screenshots";
        public static string fileName = path + "\\Screenshots\\screenshot" + date + ".jpg";

        public static void CapturarImagen()
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


        static void Main(string[] args)
        {
            try
            {                   
                if (!Directory.Exists(workDirectory))
                {
                    // Crear repositorio si no existe
                    Directory.CreateDirectory(workDirectory);
                    CapturarImagen();                    
                }
                else
                {
                    CapturarImagen();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error encontrado ", e);
            }
        }        
    }
}