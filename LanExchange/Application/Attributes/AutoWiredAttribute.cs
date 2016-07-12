using System;

namespace LanExchange.Application.Attributes
{
    /// <summary>
    /// The attribute indicates that a class should be auto-wired by IoC container.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    internal sealed class AutoWiredAttribute : Attribute
    {
    }
}