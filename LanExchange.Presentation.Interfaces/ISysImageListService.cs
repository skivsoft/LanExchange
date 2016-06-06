using System;
using System.Drawing;

namespace LanExchange.Presentation.Interfaces
{
    public interface ISysImageListService : IDisposable
    {
        void Create(SysImageListSize size);
        Size Size { get; }
        Icon GetIcon(int index);

        int GetIconIndex(string fileName);
    }
}
