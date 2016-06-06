using System.ComponentModel;

namespace LanExchange.Presentation.Interfaces.Config
{
    /// <summary>
    /// The base class for any an application config.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public abstract class ConfigBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when any property has changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected void OnChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
