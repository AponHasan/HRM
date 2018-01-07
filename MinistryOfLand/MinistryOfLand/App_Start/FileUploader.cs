using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using XtrWebTools;

namespace MinistryOfLand.App_Start
{
    public class FileUploader
    {
        static NetworkCredential cred = new NetworkCredential("mrhintl", "xxxxxxxx1");
        public static List<string> FileUpload(ControllerContext context,string path)
        {
            //var path = "webfiles/" + context.RouteData.Values["Controller"].ToString() + "/" + context.RouteData.Values["Action"].ToString() + "/";
            var pathList = new List<string>();
            var files = context.HttpContext.Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
                var filedata = files[i];
                if (filedata != null && filedata.ContentLength > 0 && filedata.FileName != null)
                {
                    var filename = filedata.FileName.clearFileNameWithUniqueIdentifier();

                    if (context.HttpContext.Request.IsLocal)
                    {
                        if (XtrWebTools.XtrFtpClient.Upload(filedata, context.HttpContext.Server.MapPath("~/" + path), null, filename))
                        {
                            pathList.Add(path + filename);
                        }
                    }
                    else
                    {
                        var path2 = XtrWebTools.XtrFtpClient.FtpUrlBuilder("ftp://ftp.test.mrhintl.com/", path);
                        if (XtrWebTools.XtrFtpClient.Upload(filedata, path2, cred, filename))
                        {
                            pathList.Add(path + filename);
                        }

                    }
                }
            }

            return pathList;
        }
    }
}