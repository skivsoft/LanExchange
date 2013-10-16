﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;

namespace GenerateEnglish
{
    class Program
    {
        private static Dictionary<string, Phrase> s_Dict;

        private static readonly string[] Languages = new []
        {
            "English",
            "Esperanto",
            "French",
            "German",
            "Hungarian",
            "Kazakh",
            "Russian",
            "Spanish"
        };
        
        static void Main(string[] args)
        {
            var files = GetFiles();
            s_Dict = new Dictionary<string, Phrase>();
            foreach (var file in files)
                try
                {
                    ProcessFile(file);
                }
                catch
                {
                }
            GeneratePO();
        }

        static string[] GetFiles()
        {
            var args = Environment.GetCommandLineArgs();
            var currentDir = Path.GetDirectoryName(args[0]);
            if (currentDir == null)
                return new string[]{};
            var files = Directory.GetFiles(currentDir, "*.exe");
            var result = new List<string>();
            var origFile = args[0].EndsWith(".vshost.exe") ? args[0].Replace(".vshost.", ".") : "";
            foreach(var file in files)
                if (!file.Equals(args[0]) && !file.Equals(origFile))
                    result.Add(file);
            files = Directory.GetFiles(currentDir, "*.dll");
            foreach (var file in files)
                result.Add(file);
            return result.ToArray();
        }

        static void ProcessFile(string fileName)
        {
            var assembly = Assembly.LoadFile(fileName);
            var resMan = new ResourceManager(string.Format("{0}.Properties.Resources", Path.GetFileNameWithoutExtension(fileName)), assembly);
            var resourceSet = resMan.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            var context = Path.GetFileName(fileName);
            foreach(DictionaryEntry entry in resourceSet)
            {
                if (entry.Value != null && entry.Value is string)
                {
                    var value = (string) entry.Value;
                    Phrase found;
                    if (!s_Dict.TryGetValue(value, out found))
                        s_Dict.Add(value, new Phrase(context, value));
                }
            }
        }

        static string FormatGetText(string value)
        {
            //var sb = new StringBuilder(value);
            return "\"" + value.Replace("\n", "\\n").Replace("\r", "") + "\"";
        }

        static void GeneratePO()
        {
            var sb = new StringBuilder();
            sb.AppendLine("# This is autogenerated file made by GenerateEnglish.exe");
            sb.AppendLine();
            // add languages
            foreach(var lang in Languages)
            {
                sb.AppendLine("msgctxt \"Language\"");
                sb.AppendLine(string.Format("msgid \"{0}\"", lang));
                sb.AppendLine("msgstr \"\"");
                sb.AppendLine();
            }
            // add strings
            foreach(var pair in s_Dict)
            {
                sb.AppendLine(string.Format("msgctxt \"{0}\"", pair.Value.Context));
                sb.AppendLine("msgid " + FormatGetText(pair.Value.ID));
                sb.AppendLine("msgstr \"\"");
                sb.AppendLine();
            }
            using (var writer = new StreamWriter(File.Create("English.po")))
            {
                writer.Write(sb.ToString());
            }
        }
    }
}