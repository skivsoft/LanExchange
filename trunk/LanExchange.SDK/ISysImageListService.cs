using System;
using System.Drawing;

namespace LanExchange.SDK
{
    public interface ISysImageListService : IDisposable
    {
        void Create(SysImageListSize size);
        Size Size { get; }
        Icon GetIcon(int index);

        int GetIconIndex(string fileName);
    }
}
