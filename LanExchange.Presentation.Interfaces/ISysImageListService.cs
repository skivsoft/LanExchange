using System;
using System.Drawing;

namespace LanExchange.Presentation.Interfaces
{
    public interface ISysImageListService : IDisposable
    {
        Size Size { get; }

        void Create(SysImageListSize size);

        Icon GetIcon(int index);

        int GetIconIndex(string fileName);
    }
}
