using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Client
{
  internal class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Введите путь к файлу:");

      string path;
      bool exist;
      FileInfo fi;

      do
      {
        path = Console.ReadLine();
        fi = new FileInfo(path);
        exist = fi.Exists;

        if (!exist)
        {
          Console.WriteLine("Некорректный путь, повторите ввод.");
        }
      } while (!exist);

      string[] text = File.ReadAllLines(path);

      ServiceWcf.Service1Client client = new ServiceWcf.Service1Client();
      Dictionary<string, int> dictionary = client.Create(text);

      if (!(dictionary is null))
      {
        string outputPath = Path.Combine(
          fi.DirectoryName, DateTime.Now.ToString("yyyyMMddHHmmssffff") + "output.txt");

        Dictionary<string, int> sortedDict = dictionary
          .OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

        using (StreamWriter writer = new StreamWriter(outputPath))
        {
          foreach (string keyWord in sortedDict.Keys)
          {
            writer.WriteLine($"{keyWord}: {sortedDict[keyWord]}");
          }
        }
      }
    }
  }
}
