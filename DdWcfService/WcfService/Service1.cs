using MyLib;
using System.Collections.Concurrent;
using System.ServiceModel;

namespace WcfService
{
  [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
  public class Service1 : IService1
  {
    public ConcurrentDictionary<string, int> Create(string[] text)
    {
      return DictCreator.CreateParallel(text);
    }
  }
}
