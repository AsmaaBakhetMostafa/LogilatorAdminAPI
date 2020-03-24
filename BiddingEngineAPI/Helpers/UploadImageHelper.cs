using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiddingEngineAPI.Helpers
{
    public static class UploadImageHelper 
    {
        public static string SaveImage(string base64Image,string folderName)
        {
            string incoming = base64Image;//.Replace("data:image/jpeg;base64,/", String.Empty);

            incoming = incoming.Replace('_', '/').Replace('-', '+');
            switch (base64Image.Length % 4)
            {
                case 2: incoming += "=="; break;
                case 3: incoming += "="; break;
            }
            //incoming = incoming.Replace(" ", "+");
            //     incoming= Encoding.UTF8.GetString(Convert.FromBase64String(incoming));
            // full path to file in current project location
            string filedir = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files\" + folderName);
            try
            {
                var bytes = Convert.FromBase64String(incoming);

                if (!Directory.Exists(filedir))
                { //check if the folder exists;
                    Directory.CreateDirectory(filedir);
                }
                Guid name = Guid.NewGuid();
                string file = Path.Combine(filedir, name.ToString() + ".jpeg");

                if (bytes.Length > 0)
                {
                    using (var stream = new FileStream(file, FileMode.Create))
                    {
                        stream.Write(bytes, 0, bytes.Length);
                        stream.Flush();
                    }
                }
                return @$"Files\{folderName}\{name.ToString()}.jpeg";
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static string SaveFile(string type, string base64, string folderName)
        {
            string incoming = base64;//.Replace("data:image/jpeg;base64,/", String.Empty);

            incoming = incoming.Replace('_', '/').Replace('-', '+');
            switch (base64.Length % 4)
            {
                case 2: incoming += "=="; break;
                case 3: incoming += "="; break;
            }
            //incoming = incoming.Replace(" ", "+");
            //     incoming= Encoding.UTF8.GetString(Convert.FromBase64String(incoming));
            // full path to file in current project location
            string filedir = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files\" + folderName);
            string Ext = "";

            if (type == "application")
                Ext = ".pdf";
            else if (type == "application")
                Ext = ".text";
          
                try
                {
                    var bytes = Convert.FromBase64String(incoming);

                    if (!Directory.Exists(filedir))
                    { //check if the folder exists;
                        Directory.CreateDirectory(filedir);
                    }
                    Guid name = Guid.NewGuid();
                    string file = Path.Combine(filedir, name.ToString() + Ext);

                    if (bytes.Length > 0)
                    {
                        using (var stream = new FileStream(file, FileMode.Create))
                        {
                            stream.Write(bytes, 0, bytes.Length);
                            stream.Flush();
                        }
                    }
                    return @$"Files\{folderName}\{name.ToString()}"+ Ext;
                }
                catch (Exception ex)
                {
                    return "";
                }
        }

        public static string SavePdf(string base64File, string folderName)
        {
            
            try
            {
                var bytes = Convert.FromBase64String(base64File);

                // full path to file in current project location
                string filedir = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files\" + folderName);
                if (!Directory.Exists(filedir))
                { //check if the folder exists;
                    Directory.CreateDirectory(filedir);
                }
                Guid name = Guid.NewGuid();
                string file = Path.Combine(filedir, name.ToString() + ".pdf");

                if (bytes.Length > 0)
                {
                    using (var stream = new FileStream(file, FileMode.Create))
                    {
                        stream.Write(bytes, 0, bytes.Length);
                        stream.Flush();
                    }
                }
                return @$"Files\{folderName}\{name.ToString()}.pdf";
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
