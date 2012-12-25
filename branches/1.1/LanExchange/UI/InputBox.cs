using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace LanExchange.UI
{
    /// <summary>
    /// Summary description for InputBox.
    /// </summary>
    [ToolboxItem(true)]
    public class InputBox : Component
    {
        protected string m_Prompt = string.Empty;
        protected string m_Caption = string.Empty;
        private InputBoxForm inputBox;

        public InputBox(IContainer container)
        {
            container.Add(this);
        }

        public InputBox() { }

        /// <summary>
        /// Property Prompt (string)
        /// </summary>
        [DefaultValue("")]
        public string Prompt { get; set; }

        /// <summary>
        /// Property Caption (string)
        /// </summary>
        [DefaultValue("")]
        public string Caption { get; set; }

        /// <summary>
        /// Property ErrorMsgOnEmpty (string)
        /// </summary>
        [DefaultValue("")]
        public string ErrorMsgOnEmpty { get; set; }

        /// <summary>
        /// Shows input-box
        /// </summary>
        /// <param name="defText">Default text for InputBox</param>
        /// <returns>null if Cancel has been pressed or string</returns>
        public virtual string Ask(string defText, bool allow_empty)
        {
            if (inputBox == null)
                inputBox = new InputBoxForm();

            if (!String.IsNullOrEmpty(m_Caption))
                inputBox.Text = m_Caption;
            else
                inputBox.Text = Application.ProductName;

            inputBox.Prepare(m_Prompt, ErrorMsgOnEmpty, defText, allow_empty);

            DialogResult res = inputBox.ShowDialog();
            if (res != DialogResult.OK)
                return null;
            else
                return inputBox.txtInputText.Text.Trim();
        }

        internal string Ask(string caption, string prompt, string defText, bool allow_empty)
        {
            m_Caption = caption;
            m_Prompt = prompt;
            return Ask(defText, allow_empty);
        }
    }
}
