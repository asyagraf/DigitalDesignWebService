using System.Collections.Concurrent;
using System.Net.Http.Headers;

Console.WriteLine("Введите путь к файлу:");

string path;
bool exist;
FileInfo fi;

HttpClient client = new();
client.BaseAddress = new Uri("http://localhost:5000/");
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/json"));

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

HttpResponseMessage response = await client.PostAsJsonAsync("dict/create", text);

Dictionary<string, int> dictionary = new();

if (response.IsSuccessStatusCode)
{
  var responseDict = await response.Content.ReadAsAsync<ConcurrentDictionary<string, int>>();
  dictionary = responseDict.ToDictionary(x => x.Key, x => x.Value);
}

if (dictionary is not null)
{
  string outputPath = Path.Combine(
    fi.DirectoryName, DateTime.Now.ToString("yyyyMMddHHmmssffff") + "output.txt");
  Dictionary<string, int> sortedDict = dictionary
  .OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

  using StreamWriter writer = new(path);

  foreach (string keyWord in sortedDict.Keys)
  {
    writer.WriteLine($"{keyWord}: {sortedDict[keyWord]}");
  }
}