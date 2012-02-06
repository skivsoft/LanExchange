using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Tools
{
    public class PathUtils
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
    }
}
