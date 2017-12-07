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
        public static string temp = path + "\\Temp"; // Directorio temporal
        public static string tempImage = temp + "\\screenshot_" + date + ".jpg"; // Guardar imagen en el directorio temporal

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


        public static void RenderizarImagen()
        {
            try
            {
                // Ubicar directorio de trabajo
                DirectoryInfo directory = new DirectoryInfo(workDirectory);

                // Ordenar imagenes de forma descendente
                FileInfo[] allFiles = directory.GetFiles().OrderByDescending(f => f.CreationTime).ToArray();

                string imagenReciente = "";

                // Seleccionar la primera imagen
                foreach (var f in allFiles)
                {
                    imagenReciente = f.Name;
                    break;
                }

                // Ubicar imagen reciente en su directorio correspondiente
                using (var image = Image.FromFile(workDirectory + "\\" + imagenReciente))

                // Usar funcion para escalar la imagen seleccionada
                using (var newImage = EscalarImagen(image, 600, 600))
                {
                    // Crear directorio temporal
                    if (!Directory.Exists(temp))
                        Directory.CreateDirectory(temp);

                    // Guardar la imagen en el directorio temporal
                    newImage.Save(tempImage, ImageFormat.Jpeg);
                }

                // Eliminar la imagen original ubicada en el directorio correspondiente
                string imagenDelete = workDirectory + "\\" + imagenReciente;
                File.Delete(imagenDelete);

                // Funcion para escalado de imagen
                Image EscalarImagen(Image image, int maxWidth, int maxHeight)
                {
                    var ratioX = (double)maxWidth / image.Width;
                    var ratioY = (double)maxHeight / image.Height;
                    var ratio = Math.Min(ratioX, ratioY);

                    var newWidth = (int)(image.Width * ratio);
                    var newHeight = (int)(image.Height * ratio);

                    var newImage = new Bitmap(newWidth, newHeight);

                    using (var graphics = Graphics.FromImage(newImage))
                    graphics.DrawImage(image, 0, 0, newWidth, newHeight);

                    return newImage;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error encontrado: {0}", e.ToString());
            }
        }


        public static void MoverImagen()
        {
            string sourcePath = temp;
            string targetPath = workDirectory;
            string[] files = Directory.GetFiles(sourcePath, "*.jpg");

            foreach (string file in files)
            {
                string filename = Path.GetFileName(file);
                string destino = Path.Combine(targetPath, filename);

                File.Move(file, destino);
            }
        }


        static void Main(string[] args)
        {
            try
            {          
                // Comprobar existencia del repositorio
                if (!Directory.Exists(workDirectory))
                    Directory.CreateDirectory(workDirectory);

                CapturarImagen();
                RenderizarImagen();
                MoverImagen();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error encontrado: {0}", e.ToString());
            }
        }        
    }
}