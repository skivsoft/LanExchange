﻿using System.Drawing;

namespace LanExchange.Application.Interfaces
{
    public interface IScreenService
    {
        Rectangle PrimaryScreenWorkingArea { get; }

        Rectangle GetWorkingArea(Point pt);

        Rectangle GetWorkingArea(Rectangle rect);

        Point CursorPosition { get; }
    }
}