using ModbusReader.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusReader.Services
{
    public interface IModbusService 
    {
        bool ToggleConnection(string ipAddress, int port, bool isStartMode);
        bool ToggleConnection(string serialPort, bool isStartMode);
        List<ModbusVariableViewModel> ReadVariables(string functionType, int startAddress, int quantityToRead);
    };
}
