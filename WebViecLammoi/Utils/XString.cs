using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Web;
using MaHoa_GiaiMa_TaiKhoan;
using System.Reflection;
using System.IO;
using System.Drawing;
using System.Net;
using System.Configuration;
using WebGrease.Activities;
using System.Text.RegularExpressions;

namespace WebViecLammoi.Utils
{
    public static class XString
    {
        public static string maplocal = ConfigurationManager.AppSettings["maplocal"];//"C:\\inetpub\\wwwfiles\\Upload\\";
        public static string mapweb = ConfigurationManager.AppSettings["mapweb"];//"E:\\webvl2024_new - Copy\\WebViecLammoi\\Content\\Upload\\";
        public static string DiaChi = ConfigurationManager.AppSettings["DiaChi"];
        public static string SDT = ConfigurationManager.AppSettings["SDT"];
        public static string EmailLienhe = ConfigurationManager.AppSettings["EmailLienhe"];
        public static string dowmain = ConfigurationManager.AppSettings["dowmain"];
        public static string tinh = ConfigurationManager.AppSettings["Tinh"];
        public static string TinhId = ConfigurationManager.AppSettings["TinhId"];
        public static string Huyen = ConfigurationManager.AppSettings["Huyen"];
        public static String EditString(this String n)
        {
            if (n != "" && n != null)
            {
                var str1 = n.Replace(" ", " -");
                var str2 = str1.Replace(".", ",");
                var str3 = str2.Replace(":", "");
                var str4 = str3.Replace("+", "-");
                var str5 = str4.Replace("#", "-");
                var str6 = str5.Replace("@", "-");
                var str7 = str6.Replace("*", "-");
                var str8 = str7.Replace("$", "-");
                var str9 = str8.Replace("%", "-");
                var str10 = str9.Replace("/", "-");
                var str11 = str10.Replace('"', ' ');
                var str = str11.Replace("&", "-");

                return str;
            }
            else return n;
        }
        public static string ConvertToUnSign(string text)
        {
            for (int i = 32; i < 48; i++)
            {
                text = text.Replace(((char)i).ToString(), " ");
            }

            text = text.Replace(".", "-");

            text = text.Replace(" ", "-");

            text = text.Replace(",", "-");

            text = text.Replace(";", "-");

            text = text.Replace(":", "-");
            Regex regex = new Regex(@"p{IsCombiningDiacriticalMarks}+");
            string strFormD = text.Normalize(System.Text.NormalizationForm.FormD);
            return regex.Replace(strFormD, String.Empty).Replace("u0111", "d").Replace("u0110", "D");

        }
        public static String EditStringtoid(this String n)
        {
            if (n != "" && n != null)
            {
                var str1 = n.Replace("-", "_");
                return str1;
            }
            else return n;
        }
        public static String EditStringCV(this String n)
        {
            if (n != "" && n != null)
            {
                var str1 = n.Replace('"', '\'');
                var str2 = str1.Replace("  ", " ");
                return str2;
            }
            return n;
        }
        public static string CutP_tostring(string s)
        {
            string kt = s.Trim().Substring(0, 3);
            if (kt == "<p>")
            {
                s = s.Trim().Substring(3, s.Length - 7);
            }
            return s;
        }
        public static String Cutstring(this String nn)
        {
            if (nn != null && nn != "")
            {
                string[] str1 = nn.Split('/');
                if (str1.Count() > 1)
                {
                    var ten = str1[1];
                    var ext = ten.Substring(ten.LastIndexOf('.'));
                    if (ext == ".jpg" || ext == ".png" || ext == ".jpeg" || ext == ".gif")
                    {
                        return str1[1];
                    }
                    else
                    {
                        return "";
                    }
                }
                else { return str1[0]; }

            }
            else return nn;

        }
        public static String Cutstring_getLastString(this String nn)
        {
            if (nn != null && nn != "")
            {
                string[] str1 = nn.Split('/');
                if (str1.Count() > 1)
                {
                    string tt = str1[str1.Count() - 1];
                    return str1[str1.Count() - 1];
                }
                else { return str1[0]; }

            }
            else return nn;
        }

        public static String CutstringSpace(this String nn)
        {
            if (nn != null && nn != "")
            {
                string[] str1 = nn.Split(' ');
                return str1[0];
            }
            else return nn;

        }
        public static String[] Cutstringcomcoma(this String nn)
        {
            if (nn != null && nn != "")
            {
                string[] str = nn.Split(',');
                return str;
            }
            else return null;
        }
        public static String getStringpdf(this String pdf)
        {
            if (pdf != null && pdf.Trim() != "")
            {
                string[] str1 = pdf.Trim().Split('.');
                if (str1.Count() == 3 && str1[2] == "pdf")
                {
                    return str1[1] + "." + str1[2];
                }
                else if (str1.Count() == 2 && str1[1] == "pdf")
                {
                    return pdf.Trim();
                }
                else return "Nopdf";
            }
            else return "Nopdf";
        }
        //giống Cutstring_getLastString
        public static String EnCodeFilename_kh(string filename)
        {
            var ext = filename.Substring(filename.LastIndexOf('.'));
            filename = Guid.NewGuid().ToString() + ext;
            return filename;
        }
        public static String getext(string filename)
        {
            var ext = filename.Substring(filename.LastIndexOf('.'));
            return ext;
        }
        public static String uppercase(string nn)
        {
            return nn.ToUpper();
        }

        public static string Truncate(string input, int length)
        {
            string tr = "";
            if (input != null)
            {
                if (input.Length <= length)
                {
                    tr = input;
                }
                else
                {
                    tr = input.Substring(0, length) + "...";
                }
            }
            return tr;
        }
        //04/2020 lấy img thứ stt của ImagePath2 để show ra
        public static String CutstringToNew(this String nn, int stt)
        {
            if (nn != null && nn != "")
            {
                nn = nn.Trim();
                nn = nn.Replace("  ", " ");
                string[] str1 = nn.Split(' ');
                for (int i = 0; i < str1.Count(); i++)
                {
                    if (str1[i].Length > 1)
                    {
                        string a = str1[i];
                        string[] img = a.Split('.');
                        if (img[0] == stt.ToString() && img[2] != "pdf")
                        {
                            return img[1] + "." + img[2];
                        }
                        else if (img[0] == stt.ToString() && img[2] == "pdf") { return ""; }
                    }
                }
                return "";

            }
            else return nn;

        }
        public static String GetImgBase64(string foder, string img)
        {
            ////Cac file web khong cho phep, thi xoa luon
            var path = mapweb + foder + "\\" + img;
            if (!File.Exists(path))
            {
                //tự động lấy các file cần thiết 
                var ext = getext(img);
                byte[] imageArray = System.IO.File.ReadAllBytes(maplocal + foder + "\\" + img);
                //string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                if (ext.ToLower() == ".jpg" || ext.ToLower() == ".png" || ext.ToLower() == ".gif" || ext.ToLower() == ".pdf" || ext.ToLower() == ".xls"
                    || ext.ToLower() == ".xlsm" || ext.ToLower() == ".doc" || ext.ToLower() == ".docx" || ext.ToLower() == ".rar" || ext.ToLower() == ".zip" || ext.ToLower() == ".pptx")
                {
                    File.WriteAllBytes(mapweb + foder + "\\" + img, imageArray);
                }
                else
                {
                    Xoahinhcu(foder, img);
                }
            }
            return "";
            //return $"data: application/jpg ; base64,{base64ImageRepresentation}";
        }
        public static bool Xoahinhcu(string foder, string img)
        {
            try
            {
                var pathweb = mapweb + foder + "\\" + img;
                var pathlocal = maplocal + foder + "\\" + img;
                //xoa local truoc
                if (File.Exists(pathlocal))
                {
                    File.Delete(pathlocal);
                }
                if (File.Exists(pathweb))
                {
                    File.Delete(pathweb);
                }
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }

        }
    }
}