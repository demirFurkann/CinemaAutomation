using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Project.MVCAdmin.Models.CustomTools
{
    public static class ImageUploader
    {
        public static string UploadImage(string serverPath, HttpPostedFileBase file, string name)
        {
            if (file != null)
            {
                Guid uniqueName = Guid.NewGuid();

                string[] fileArrey = file.FileName.Split('.');
                string extansion = fileArrey[fileArrey.Length - 1].ToLower();

                string fileName = $"{uniqueName}.{name}.{extansion}";

                if (extansion == "jpg" || extansion == "gif" || extansion == "png" || extansion == "jpeg")
                {
                    if (File.Exists(HttpContext.Current.Server.MapPath(serverPath + fileName)))
                    {
                        return "1";
                    }
                    else
                    {
                        string filePath = HttpContext.Current.Server.MapPath(serverPath + fileName);
                        file.SaveAs(filePath);
                        return $"{serverPath} {fileName}";
                    }
                }
                else
                {
                    return "2";
                }

            }
            else
            {
                return "3";
            }
        }
    }
}