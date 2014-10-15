using System;

namespace LanExchange.Plugin.Notify
{
    public class MessageEventArgs : EventArgs
    {
        public MessageEventArgs(byte[] data)
        {
            Data = data;
        }

        public byte[] Data { get; private set; }
    }
}