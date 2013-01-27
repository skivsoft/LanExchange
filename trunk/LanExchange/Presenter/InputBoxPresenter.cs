using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.Sdk.View;

namespace LanExchange.Presenter
{
    public class InputBoxPresenter : IPresenter
    {
        public static string Ask(string caption, string prompt, string defText, bool allowEmpty)
        {
            string result = null;
            // TODO UNCOMMENT THIS!!!
            //using (var inputBox = new InputBoxForm())
            //{
            //    if (!String.IsNullOrEmpty(caption))
            //        inputBox.Text = caption;
            //    else
            //        inputBox.Text = Application.ProductName;
            //    inputBox.Prepare(prompt, defText, allowEmpty);
            //    DialogResult res = inputBox.ShowDialog();
            //    if (res == DialogResult.OK)
            //        result = inputBox.txtInputText.Text.Trim();
            //}
            return result;
        }
    }
}
