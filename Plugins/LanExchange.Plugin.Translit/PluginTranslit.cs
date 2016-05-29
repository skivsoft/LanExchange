﻿using System;
using System.ComponentModel.Composition;

using LanExchange.SDK;
using LanExchange.SDK.Extensions;

namespace LanExchange.Plugin.Translit
{
    [Export(typeof(IPlugin))]
    public sealed class PluginTranslit : IPlugin
    {
        public void Initialize(IServiceProvider serviceProvider)
        {
            var service = serviceProvider.Resolve<ITranslationService>();
            if (service != null)
            {
                service.RegisterTranslit<RussianCyrillicToLatin>();
                service.RegisterTranslit<RussianCyrillicToGame>();
                service.RegisterTranslit<KazakhCyrillicToLatin>();
                service.RegisterTranslit<KazakhCyrillicToArabic>();
            }
        }
    }
}