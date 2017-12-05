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
        public static string path = AppDomain.CurrentDomain.BaseDirectory; // Definir la ruta
        public static string fileName = path + "\\Screenshots\\screenshot" + date + ".jpg";

        public static void CapturarImagen()
        {
            // Hacer captura de pantalla
            Bitmap printscreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);

            Graphics graphics = Graphics.FromImage(printscreen as Image);
            graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size);

            //Guardar captura de pantalla
            printscreen.Save(fileName, ImageFormat.Jpeg);
        }


        static void Main(string[] args)
        {
            try
            {                   
                if (!Directory.Exists(path + "\\Screenshots"))
                {
                    // Crear repositorio si no existe
                    Directory.CreateDirectory(path + "\\Screenshots");
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