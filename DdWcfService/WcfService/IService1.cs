using System.Collections.Concurrent;
using System.ServiceModel;

namespace WcfService
{
  [ServiceContract]
  public interface IService1
  {
    [OperationContract]
    ConcurrentDictionary<string, int> Create(string[] text);
  }
}
