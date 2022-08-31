using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Splitter
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length > 0)
            {
                if(args[0].ToUpper() == "SPLIT")
                {
                    if(CheckFile(args[1]) == true)
                    {
                        SplitFile(args[1], Convert.ToInt32(args[2]));
                    }
                }
                else if(args[0].ToUpper() == "MERGE")
                {
                    if(CheckFolder(args[1]) == true)
                    {
                        MergeFile(args[1]);
                    }
                }
                else
                {
                    Console.WriteLine("Usage:");
                    Console.WriteLine("SPLIT [filename] [Number of splits]");
                    Console.WriteLine("MERGE [folder name]");
                }
                
            }
            else
            {
                Console.WriteLine("Usage:");
                Console.WriteLine("SPLIT [filename] [Number of splits]");
                Console.WriteLine("MERGE [folder name]");

            }
                
            
        }


        public static bool CheckFolder(string fn)
        {
            try
            {
               if (Directory.Exists(fn))
               {
                    return true;
               }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }

          
        }


        public static bool CheckFile(string fn)
        {
            try
            {
                FileStream fs = new FileStream(fn, FileMode.Open, FileAccess.Read);
                int FileLength = (int)fs.Length / 1024;
                string name = Path.GetFileName(fn);
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }

            return true;
        }


        public static void SplitFile(string SourceFile, int nNoofFiles)
        {
            
            try
            {
                FileStream fs = new FileStream(SourceFile, FileMode.Open, FileAccess.Read);
                int SizeOfEachFile = (int)Math.Ceiling((double)fs.Length / nNoofFiles);

                for (int i = 0; i < nNoofFiles; i++)
                {
                    string baseFileName = Path.GetFileNameWithoutExtension(SourceFile);
                    string Extension = Path.GetExtension(SourceFile);
                    FileStream outputFile = new FileStream(Path.GetDirectoryName(SourceFile) + "\\" + baseFileName + "." +
                      i.ToString().PadLeft(5, Convert.ToChar("0")) + Extension + ".tmp", FileMode.Create, FileAccess.Write);



                    int bytesRead = 0;
                    byte[] buffer = new byte[SizeOfEachFile];

                    if ((bytesRead = fs.Read(buffer, 0, SizeOfEachFile)) > 0)
                    {
                        outputFile.Write(buffer, 0, bytesRead);
                        string packet = baseFileName + Extension.ToString() + "." + i.ToString().PadLeft(5, Convert.ToChar("0")) + ".tmp";
                        Console.WriteLine("File Created: " + packet);
                    }

                    outputFile.Close();
                   
                }

                fs.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }


        public static void MergeFile(string inputfildername1)
        {
            try
            {
                string[] tmpfiles = Directory.GetFiles(inputfildername1, "*.tmp");

                FileStream outPutFile = null;
                string PrevFileName = "";

                foreach (string tempFile in tmpfiles)
                {
                    string fileName = Path.GetFileNameWithoutExtension(tempFile);
                    string baseFileName = fileName.Substring(0, fileName.IndexOf(Convert.ToChar(".")));
                    string extension = Path.GetExtension(fileName);

                    if (!PrevFileName.Equals(baseFileName))
                    {
                        if (outPutFile != null)
                        {
                            outPutFile.Flush();
                            outPutFile.Close();
                        }
                        outPutFile = new FileStream(inputfildername1 + "\\" + baseFileName + extension, FileMode.OpenOrCreate, FileAccess.Write);
                    }

                    int bytesRead = 0;
                    byte[] buffer = new byte[1024];
                    FileStream inputTempFile = new FileStream(tempFile, FileMode.OpenOrCreate, FileAccess.Read);

                    while ((bytesRead = inputTempFile.Read(buffer, 0, 1024)) > 0)
                        outPutFile.Write(buffer, 0, bytesRead);

                    inputTempFile.Close();
                    //File.Delete(tempFile);
                    PrevFileName = baseFileName;
                }
                outPutFile.Close();
                Console.WriteLine("Files have been merged and saved at location " + inputfildername1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }




        }
}
