using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyModbus;
using ModbusReader.ViewModels;

namespace ModbusReader.Services.Implementation
{
    public class ModbusService : IModbusService
    {
        private static ModbusClient modbusClient;
        private const int connectionTimeoutInMs = 2500;

        public bool ToggleConnection(string ipAddress, int port, bool isStartMode)
        {
            if (isStartMode)
            {
                modbusClient = new ModbusClient(ipAddress, port);
                if (modbusClient.Available(connectionTimeoutInMs))
                {
                    try
                    {
                        modbusClient.Connect();
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }
            else
            {
                modbusClient.Disconnect();
                return true;
            }
            return false;
        }
        public bool ToggleConnection(string serialPort, bool isStartMode)
        {
            if (isStartMode)
            {
                modbusClient = new ModbusClient(serialPort);
                {
                    try
                    {
                        modbusClient.Connect();
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }
            else
            {
                modbusClient.Disconnect();
                return true;
            }
        }

        public List<ModbusVariableViewModel> ReadVariables(string functionType, int startAddress, int quantityToRead)
        {
            throw new NotImplementedException();
        }

    }
}
