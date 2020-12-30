using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DeleteEmptyFolder
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            count = 0;
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                string folderPath = folderBrowser.SelectedPath;
                DirectoryInfo directory = new DirectoryInfo(folderPath);
                deleteEmptyFolder(directory);
            }
            Console.WriteLine("清理完成,共清理"+count.ToString()+"个空文件夹");
            Console.ReadLine();
        }
        static int count=0;
        static void deleteEmptyFolder(DirectoryInfo directory)
        {
            try
            {
                DirectoryInfo[] directories = directory.GetDirectories();
                foreach (DirectoryInfo info in directories)
                {
                    deleteEmptyFolder(info);
                }
                if (directory.GetFiles().Length == 0 && directory.GetDirectories().Length == 0)
                {
                    try
                    {
                        Console.WriteLine("删除文件夹:" + directory.FullName);
                        directory.Delete();
                        count++;
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            catch(System.UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
