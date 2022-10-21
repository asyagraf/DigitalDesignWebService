using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLib
{
  public static class DictCreator
  {
    public static ConcurrentDictionary<string, int> CreateParallel(string[] text)
    {
      char[] toTrim = new char[] { '-', '\'' };
      char[] toSplit = new char[] {
        ' ', '!', '.', ',', '?', '\n', '\t', ':', ';', '\"',
        '\\', '/', '<', '>', '[', ']', '(', ')', '='};

      ConcurrentDictionary<string, int> dictionary = new ConcurrentDictionary<string, int>();

      Parallel.ForEach(text, line =>
      {
        string[] words = line.Split(toSplit);

        foreach (string word in words)
        {
          string keyWord = word.ToLower().Trim(toTrim);

          if (!String.IsNullOrEmpty(keyWord))
          {
            dictionary.AddOrUpdate(keyWord, 1, (key, value) => value + 1);
          }
        }
      });

      return dictionary;
    }

    private static Dictionary<string, int> Create(string[] text)
    {
      Dictionary<string, int> dictionary = new Dictionary<string, int>();
      char[] toTrim = new char[] { '-', '\'' };
      char[] toSplit = new char[] {
        ' ', '!', '.', ',', '?', '\n', '\t', ':', ';', '\"',
        '\\', '/', '<', '>', '[', ']', '(', ')', '='};

      foreach (string line in text)
      {
        string[] words = line.Split(toSplit);

        foreach (string word in words)
        {
          string keyWord = word.ToLower().Trim(toTrim);

          if (String.IsNullOrEmpty(keyWord))
          {
            continue;
          }

          if (!dictionary.ContainsKey(keyWord))
          {
            dictionary.Add(keyWord, 1);
          }
          else
          {
            dictionary[keyWord]++;
          }
        }
      }

      return dictionary;
    }
  }
}
