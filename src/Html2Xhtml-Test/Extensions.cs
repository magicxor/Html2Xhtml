using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfTest
{
    public static class Extensions
    {
        public static Stream OpenDownload(this Uri address)
        {
            var web = new WebClient();
            web.Headers["Referer"] = "http://www.google.com/";
            web.Headers["Accept"] = "application/xml,application/xhtml+xml,text/html;q=0.9,text/plain;q=0.8,image/png,*/*;q=0.5";
            web.Headers["User-Agent"] = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US) AppleWebKit/534.2 (KHTML, like Gecko) Chrome/6.0.453.1 Safari/534.2";
            web.Headers["Accept-Language"] = "en-US,en;q=0.8";
            web.Headers["Accept-Charset"] = "utf-8,ISO-8859-1;q=0.7,*;q=0.3";
            return web.OpenRead(address);
        }
    }
}
