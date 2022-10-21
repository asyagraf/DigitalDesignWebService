using System;
using System.ServiceModel;

namespace Host
{
  internal class Program
  {
    static void Main(string[] args)
    {
      using(var host = new ServiceHost(typeof(WcfService.Service1)))
      {
        host.Open();
        Console.WriteLine("Хост стартовал. Для завершения работы нажмите любую клавишу.");
        Console.ReadLine();
      }
    }
  }
}
