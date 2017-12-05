using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.IO;

namespace ActionScreenShot
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string date = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_").Replace(" ", "_"); // Obtener la fecha                
                string path = AppDomain.CurrentDomain.BaseDirectory; // Definir la ruta
                string fileName = path + "\\Screenshots\\screenshot" + date + ".jpg";
                                
                if (!Directory.Exists(path + "\\Screenshots"))
                {
                    // Crear repositorio si no existe
                    Directory.CreateDirectory(path + "\\Screenshots");
                    
                    // Hacer captura de pantalla
                    Bitmap printscreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
                    
                    Graphics graphics = Graphics.FromImage(printscreen as Image);
                    graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size);

                    //Guardar captura de pantalla
                    printscreen.Save(fileName, ImageFormat.Jpeg);
                }
                else
                {
                    // Hacer captura de pantalla
                    Bitmap printscreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
                    
                    Graphics graphics = Graphics.FromImage(printscreen as Image);
                    graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size);

                    //Guardar captura de pantalla
                    printscreen.Save(fileName, ImageFormat.Jpeg);
                }


                
                

                
                //Console.WriteLine(date);
                //Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error encontrado ", e);
            }
        }
    }
}