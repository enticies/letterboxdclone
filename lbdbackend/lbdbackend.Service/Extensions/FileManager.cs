using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace P225Allup.Extensions {
    public static class FileManager {
        public static bool CheckFileContentType(this IFormFile file, string contentType) {
            return file.ContentType != contentType;
        }

        public static bool CheckFileSize(this IFormFile file, double size) {
            return (file.Length / 1024) > size;
        }

        //public async static Task<string> CreateFileAsync(this IFormFile file, IWebHostEnvironment env, params string[] folders) {
        //    string fileName = Guid.NewGuid().ToString() + "_" + DateTime.Now.ToString("yyyyMMddHHmmssff") + "_" + file.FileName;

        //    string fullPath1 = "C:\\Users\\lasau\\Projects\\lbdfrontend\\adminpanel";
        //    string fullPath2 = "C:\\Users\\lasau\\Projects\\lbdfrontend\\website";

        //    foreach (string folder in folders) {
        //        fullPath1 = Path.Combine(fullPath1, folder);
        //        fullPath2 = Path.Combine(fullPath2, folder);
        //    }

        //    fullPath1 = Path.Combine(fullPath1, fileName);
        //    fullPath2 = Path.Combine(fullPath2, fileName);

        //    using (FileStream stream = new FileStream(fullPath1, FileMode.Create)) {
        //        await file.CopyToAsync(stream);
        //    }
        //    using (FileStream stream = new FileStream(fullPath2, FileMode.Create)) {
        //        await file.CopyToAsync(stream);
        //    }
        //    fullPath2 = Path.Combine(fullPath2, fileName);

        //    return fileName;
        //}
        public async static Task<string> CreateFileAsync(this IFormFile file, IWebHostEnvironment env, params string[] folders) {
            string fileName = Guid.NewGuid().ToString() + "_" + DateTime.Now.ToString("yyyyMMdd") + "_" + file.FileName;

            string fullPath = Path.Combine(env.WebRootPath);

            foreach (string folder in folders) {
                fullPath = Path.Combine(fullPath, folder);
            }

            fullPath = Path.Combine(fullPath, fileName);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create)) {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }
    }
}
