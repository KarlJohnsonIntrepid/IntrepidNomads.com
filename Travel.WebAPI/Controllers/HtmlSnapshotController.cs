using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Travel.WebAPI.Controllers
{
    public class HtmlSnapshotController : Controller
    {
     
        public ActionResult returnHTML(string url)
        {
            string appRoot = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);

            var startInfo = new ProcessStartInfo
            {
                Arguments = String.Format("{0} {1}", Path.Combine(appRoot, "client\\seo\\createSnapshot.js"), url),
                FileName = Path.Combine(appRoot, "bin\\phantomjs.exe"),
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                StandardOutputEncoding = System.Text.Encoding.UTF8
            };
            var p = new Process();
            p.StartInfo = startInfo;
            p.Start();
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            ViewBag.snapshot = output;
            return View();
        }


        public ActionResult ogGraph()
        {
            return View();
        }
    }

}