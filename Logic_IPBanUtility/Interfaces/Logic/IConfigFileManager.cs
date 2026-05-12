using Logic_IPBanUtility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_IPBanUtility.Interfaces.Logic
{
     public interface IConfigFileManager
     {
          List<String> Context { get; }

          void UpdateContex();
          Key GetKey(KeyNames keyName);
          List<Key> CreateKeys();
          List<KeyIdenti> ReadKeyIndentis();
          void WriteKeyIdentiChanged(KeyIdenti keyIdenti);
          void WriteKey(Key key);
          void WriteKeys(IEnumerable<Key> keys);
     }
}
