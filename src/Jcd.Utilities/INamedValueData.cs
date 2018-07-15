using System.Collections.Generic;

namespace Jcd.Utilities
{
   public interface INamedValueData
   {
      object DataSource { get; set; }
      IEnumerable<string> Names { get; }
      IEnumerable<object> Values { get; }
      object GetValue(string name);
      object this[string name] { get; }
      IEnumerable<KeyValuePair<string,object>> NamedValues { get; }
   }
}