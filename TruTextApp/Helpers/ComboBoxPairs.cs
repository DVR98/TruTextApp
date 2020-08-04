using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruTextApp
{
    public class ComboBoxPairs
    {
        public int _Key { get; set; }
        public string _Value { get; set; }

        public ComboBoxPairs(int _key, string _value)
        {
            _Key = _key;
            _Value = _value;
        }
    }
}
