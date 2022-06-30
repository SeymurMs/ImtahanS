using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ImtahanLast.Utilize
{
    public static class FileManager
    {
        public static bool CheckType(this IFormFile file , string  path)
        {
            if (file.ContentType.Contains(path))
            {
                return true;
            }
            return false;
        }
        public static bool CheckSize(this IFormFile file, int kb)
        {
            if (file.Length / 1024 >kb)
            {
                return true;
            }
            return false;
        }
        public static string SaveImg(this IFormFile file,string path)
        {
            string fileName = Guid.NewGuid().ToString() + file.FileName;
            string fullPath = Path.Combine(path, fileName);
            using (FileStream fs = new FileStream(Path.Combine(fullPath),FileMode.Create))
            {
                file.CopyTo(fs);
            }
            return fileName;
        }
        public static void DeleteFile(string path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}
