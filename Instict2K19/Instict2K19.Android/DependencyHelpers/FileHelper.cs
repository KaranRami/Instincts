﻿using System;
using System.IO;
using Instict2K19.Droid.DependencyHelpers;
using Instict2K19.Interface;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace Instict2K19.Droid.DependencyHelpers
{

    public class FileHelper:IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string dbPath = Path.Combine(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).AbsolutePath, filename);
            return dbPath;
        }

        private static void CopyDatabaseIfNotExists(string dbPath,string database)
        {
            if (!File.Exists(dbPath))
            {
                using (var br = new BinaryReader(new FileStream(database, FileMode.Open)))
                {
                    using (var bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
                    {
                        byte[] buffer = new byte[2048];
                        int length = 0;
                        while ((length = br.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            bw.Write(buffer, 0, length);
                        }
                    }
                }
            }
        }
    }
}