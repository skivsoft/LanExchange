using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace LanExchange.OSLayer
{
    public class Tools
    {
        public static void ExplodeCmd(string CmdLine, out string FName, out string Params)
        {
            FName = "";
            Params = "";
            bool bQuote = false;
            bool bParam = false;
            for (int i = 0; i < CmdLine.Length; i++)
            {
                if (!bParam && CmdLine[i] == '"')
                {
                    bQuote = !bQuote;
                    continue;
                }
                if (!bParam && !bQuote && CmdLine[i] == ' ')
                    bParam = true;
                else
                    if (bParam)
                        Params += CmdLine[i].ToString();
                    else
                        FName += CmdLine[i].ToString();
            }
        }

        /*
        public static void ShowProperties(object obj)
        {
            Form F = new Form();
            F.Text = obj.ToString();
            PropertyGrid Grid = new PropertyGrid();
            Grid.Dock = DockStyle.Fill;
            Grid.SelectedObject = obj;
            F.Controls.Add(Grid);
            F.Show();
        }
        */
    }
}
