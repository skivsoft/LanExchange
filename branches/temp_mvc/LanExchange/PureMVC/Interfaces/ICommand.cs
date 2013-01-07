/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@pureorg>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/
#region Using

using System;

#endregion

namespace PureMVC.PureInterfaces
{
    /// <summary>
    /// The interface definition for a PureMVC Command
    /// </summary>
	/// <see cref="PureInterfaces.INotification"/>
    public interface ICommand
    {
        /// <summary>
        /// Execute the <c>ICommand</c>'s logic to handle a given <c>INotification</c>
        /// </summary>
        /// <param name="notification">An <c>INotification</c> to handle</param>
		void Execute(INotification notification);
    }
}
