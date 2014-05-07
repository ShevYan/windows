using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;

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
