﻿using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.Sdk;
using LanExchange.UI;
using System.Windows.Forms;

namespace LanExchange.Presenter
{
    public static class InputBoxPresenter
    {
        public static string Ask(string caption, string prompt, string defText, bool allowEmpty)
        {
            string result = null;
            using (var inputBox = new InputBoxForm())
            {
                if (!String.IsNullOrEmpty(caption))
                    inputBox.Text = caption;
                else
                    inputBox.Text = Application.ProductName;
                inputBox.Prepare(prompt, defText, allowEmpty);
                DialogResult res = inputBox.ShowDialog();
                if (res == DialogResult.OK)
                    result = inputBox.txtInputText.Text.Trim();
            }
            return result;
        }
    }
}