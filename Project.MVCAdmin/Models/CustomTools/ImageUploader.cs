using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace Project.MVCAdmin.Models.CustomTools
{
    public static class ImageUploader
    {
        public static string UploadImage(HttpPostedFileBase file, string name)
        {
            string serverPath = "/Pictures/"; // Hedef klasör yolu
            if (file != null)
            {
                Guid uniqueName = Guid.NewGuid();
               

                string extension =Path.GetExtension(name);

                string fileName = $"{uniqueName}_{Path.GetFileNameWithoutExtension(name)}{extension}";

                if (extension == ".jpg" || extension == ".gif" || extension == ".png" || extension == ".jpeg")
                {
                    if (File.Exists(HttpContext.Current.Server.MapPath(serverPath+fileName)))
                    {
                        return "1";
                    }
                    else
                    {
                        string filePath = HttpContext.Current.Server.MapPath(serverPath+ fileName);
                        file.SaveAs(filePath);
                        return fileName;
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

        //public static string UploadImage(string serverPath, HttpPostedFileBase file, string name)
        //{
        //    if (file != null)
        //    {
        //        Guid uniqueName = Guid.NewGuid();

        //        string[] fileArrey = file.FileName.Split('.');

        //        string extansion = fileArrey[fileArrey.Length - 1].ToLower();

        //        string fileName = $"{uniqueName}.{name}.{extansion}";

        //        if (extansion == "jpg" || extansion == "gif" || extansion == "png" || extansion == "jpeg")
        //        {
        //            if (File.Exists(HttpContext.Current.Server.MapPath(serverPath + fileName)))
        //            {
        //                return "1";
        //            }
        //            else
        //            {
        //                string filePath = HttpContext.Current.Server.MapPath(serverPath + fileName);
        //                file.SaveAs(filePath);
        //                return $"{serverPath}{fileName}";
        //            }
        //        }
        //        else
        //        {
        //            return "2";
        //        }

        //    }
        //    else
        //    {
        //        return "3";
        //    }

        //}





        //public static List<string> GetImagePaths(string serverPath)
        //{
        //    string physicalPath = HttpContext.Current.Server.MapPath(serverPath);
        //    if (!Directory.Exists(physicalPath))
        //    {
        //        Directory.CreateDirectory(physicalPath);
        //    }

        //    return Directory.GetFiles(physicalPath, "*.*")
        //                    .Where(file => file.ToLower().EndsWith(".jpg") ||
        //                                   file.ToLower().EndsWith(".png") ||
        //                                   file.ToLower().EndsWith(".gif") ||
        //                                   file.ToLower().EndsWith(".jpeg"))
        //                    .ToList();
        //}

        //private static string GetImageUrl(string filePath)
        //{
        //    string appPath = HttpContext.Current.Request.ApplicationPath;
        //    if (!appPath.EndsWith("/"))
        //    {
        //        appPath += "/";
        //    }

        //    string relativePath = filePath.Replace(HttpContext.Current.Server.MapPath("~"), "").Replace("\\", "/");
        //    return $"{appPath}{relativePath}";
        //}
    }
}
