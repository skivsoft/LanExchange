Hello there!

It's a short description of project folders structure.

Plugins\			Plugins folder.
SkivSoft.LanExchange.SDK	Declaration of interfaces using by plugins.
SkivSoft.LanExchange.Core	Implementation of SDK interfaces neighter platform dependencies (Windows/Linux)
				nor user interface dependecies (WinForms/WPF).
SkivSoft.LanExchange		WinForms user interface uses GDI/GDI+.
SkivSoft.LanExchangeDX		WPF user interface uses DirectX.

* Platform dependencies (Windows/Linux) must be inside plugins only.
* All strings inside project and inside plugins must be in English (en-US) language.
  It's will be translated to other languages later.
