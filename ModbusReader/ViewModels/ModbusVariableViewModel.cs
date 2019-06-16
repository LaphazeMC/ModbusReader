using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusReader.ViewModels
{
    public class ModbusVariableViewModel
    {
        public int Address { get; set; }
        public string ValueInHex { get; set; }
        public int ValueInDecimal { get; set; }
        public string ValueInBinary { get; set; }
    }
}
