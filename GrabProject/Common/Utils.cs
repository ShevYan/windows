using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;
using System.Runtime.InteropServices;

namespace Common
{
    

    public class Utils
    {
        [Serializable]
        class CompressInfo
        {
            public string baseDir;
            public Dictionary<string, byte[]> compressData = new Dictionary<string, byte[]>();
           
        }

        public static DateTime UTCZero = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public static void ArchiveFiles(LinkedList<string> files, string baseDir, string target)
        {
            if (File.Exists(target)) {
                File.Delete(target);
            }

            CompressInfo ci = new CompressInfo();
            ci.baseDir = baseDir;
            foreach (string fileName in files)
            {
                byte[] data = System.IO.File.ReadAllBytes(fileName);
                ci.compressData[fileName.Replace(baseDir, "")] = data;
            }

            CreateParentDirs(target);
            FileStream fileStream = new FileStream(target, FileMode.Create);
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(fileStream, ci);
            fileStream.Close();
        }

        public static void DearchiveFiles(string src, string dir)
        {
            FileStream FileStream = new FileStream(src, FileMode.Open, FileAccess.Read, FileShare.Read);
            BinaryFormatter b = new BinaryFormatter();
            CompressInfo ci = b.Deserialize(FileStream) as CompressInfo;
            FileStream.Close();

            foreach (KeyValuePair<string, byte[]> pair in ci.compressData)
            {
                string target = dir + "/" + pair.Key;
                CreateParentDirs(target);
                System.IO.File.WriteAllBytes(target, pair.Value);
            }
        }

        public static void CreateParentDirs(string filePath)
        {
            string targetDir = filePath.Substring(0, filePath.LastIndexOf("/"));
            if (!Directory.Exists(targetDir))
            {
                System.IO.Directory.CreateDirectory(targetDir);
            }
        }

        public static string GetWorkingDir()
        {
            return System.Environment.CurrentDirectory;
        }

        public static string EncodingConvert(string fromString, Encoding fromEncoding, Encoding toEncoding)
        {
            byte[] fromBytes = fromEncoding.GetBytes(fromString);
            byte[] toBytes = Encoding.Convert(fromEncoding, toEncoding, fromBytes);

            string toString = toEncoding.GetString(toBytes);
            return toString;
        }

        public static string GBKToUtf8(string gbkString)
        {
            Encoding fromEncoding = Encoding.GetEncoding("GBK");
            Encoding toEncoding = Encoding.UTF8;
            return EncodingConvert(gbkString, fromEncoding, toEncoding);
        }

        public static string Utf8ToGBK(string utf8String)
        {
            Encoding fromEncoding = Encoding.UTF8;
            Encoding toEncoding = Encoding.GetEncoding("GBK");
            return EncodingConvert(utf8String, fromEncoding, toEncoding);
        }

        public static string GetMiddleString(string org, string left, string right)
        {
            int start = org.IndexOf(left);
            if (start == -1) {
                throw new ArgumentOutOfRangeException();
            }
            start += left.Length;
            int end = org.IndexOf(right, start);
            if (end == -1) {
                throw new ArgumentOutOfRangeException();
            }

            return org.Substring(start, end - start);
        }

        ///////////Test
        public static void TestArchive()
        {
            LinkedList<string> files = new LinkedList<string>();
            files.AddLast("c:/1.txt");
            files.AddLast("c:/2.txt");
            files.AddLast("c:/1.jpg");

            Common.Utils.ArchiveFiles(files, "c:", "c:/compress.dat");
            Common.Utils.DearchiveFiles("c:/compress.dat", "c:/uncompress");
        }

    }
}
