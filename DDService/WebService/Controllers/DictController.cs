using Microsoft.AspNetCore.Mvc;
using MyLib;
using System.Collections.Concurrent;

namespace WebService.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class DictController : ControllerBase
  {
    [HttpPost("create")]
    public ConcurrentDictionary<string, int> CreateDict([FromBody] string[] text)
    {
      return DictCreator.CreateParallel(text);
    }
  }
}