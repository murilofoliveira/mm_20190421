using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MindMiners.Application.Interface;

namespace MindMiners.Web.Controllers
{
    public class SubtitleController : Controller
    {
        private readonly IAppSubtitle appSubtitle;

        public SubtitleController(IAppSubtitle appSubtitle)
        {
            this.appSubtitle = appSubtitle;
        }


        public async Task<ActionResult> Index()
        {

            var list = this.appSubtitle.Listar();
            return View(list);
        }
        [HttpPost("AddTimeToStr")]
        public async Task<ActionResult> AddTimeToStr(List<IFormFile> files, string timeAdd)
        {

            if (files == null || files.Count == 0)
                throw new Exception("TS invalido");

            TimeSpan ts;
            TimeSpan.TryParse(timeAdd, CultureInfo.GetCultureInfo("fr"), out ts);
            if (ts == null || ts.TotalMilliseconds == 0)
            {
                throw new Exception("TS invalido");
            }

            var sb = new StringBuilder();
            var filePath = Path.GetTempFileName();
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    StreamReader reader = new StreamReader(formFile.OpenReadStream(), Encoding.GetEncoding("iso-8859-1"));
                    do
                    {
                        sb.AppendLine(reader.ReadLine());

                    } while (reader.Peek() != -1);
                    reader.Close();
                }


                this.appSubtitle.AddSubtitle(sb, formFile.FileName);
            }

            this.appSubtitle.AddTime(ts);


            return RedirectToAction("Index");


        }

        [HttpGet]
        public ActionResult Download(string name)
        {

            var sub = this.appSubtitle.GetSubtitle(name);

            using (var stream = new MemoryStream())
            {

                using (var sw = new StreamWriter(stream))
                {
                    foreach (var line in sub.StrFile)
                    {
                        sw.WriteLine(line);
                    }
                    sw.Flush();
                    stream.Flush();

                    MemoryStream downloadMs = new MemoryStream(stream.ToArray());
                    downloadMs.Flush();
                    return new FileStreamResult(downloadMs, new MediaTypeHeaderValue("text/plain"))
                    {
                        FileDownloadName = name,

                    };
                }

            }




        }
    }
}