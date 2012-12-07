﻿#region BSD License
/* 
Copyright (c) 2012, Clarius Consulting
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

* Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

* Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

* Neither the name of Clarius Consulting nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
#endregion

namespace Clide
{
    using System.ComponentModel.Composition;
    using System.Diagnostics;
    using System.Windows;
    using Clide.Composition;
    using Microsoft.VisualStudio.Shell.Interop;

    /// <summary>
    /// Default implementation of the <see cref="IMessageBoxService"/>.
    /// </summary>
    [Export(typeof(IMessageBoxService))]
    internal class MessageBoxService : IMessageBoxService
    {
        private static readonly ITracer tracer = Tracer.Get<MessageBoxService>();
        private IVsUIShell uiShell;
        private IUIThread uiThread;

        /// <summary>
        /// Default constructor for runtime behavior that can't be mocked.
        /// </summary>
        [ImportingConstructor]
        public MessageBoxService(
            [Import(VsContractNames.IVsUIShell)] IVsUIShell uiShell,
            IUIThread uiThread)
        {
            Guard.NotNull(() => uiShell, uiShell);
            Guard.NotNull(() => uiThread, uiThread);

            this.uiShell = uiShell;
            this.uiThread = uiThread;
        }

        public void Show(string message,
            string title = "Visual Studio",
            MessageBoxButton button = MessageBoxButton.OK,
            MessageBoxImage icon = MessageBoxImage.None,
            MessageBoxResult defaultResult = MessageBoxResult.OK)
        {
            this.uiShell.EnableModeless(0);
            try
            {
                this.uiThread.Invoke(() =>
                    MessageBox.Show(this.uiShell.GetMainWindow(), message, title, button, icon, defaultResult));
            }
            finally
            {
                this.uiShell.EnableModeless(1);
            }
        }

        public MessageBoxResult Prompt(string message,
            string title = "Visual Studio",
            MessageBoxButton button = MessageBoxButton.OKCancel,
            MessageBoxImage icon = MessageBoxImage.Question,
            MessageBoxResult defaultResult = MessageBoxResult.OK)
        {
            this.uiShell.EnableModeless(0);
            try
            {
                return this.uiThread.Invoke(() =>
                    MessageBox.Show(this.uiShell.GetMainWindow(), message, title, button, icon, defaultResult));
            }
            finally
            {
                this.uiShell.EnableModeless(1);
            }
        }

        public string InputBox(string message, string title = "Visual Studio")
        {
            this.uiShell.EnableModeless(0);
            try
            {
                return this.uiThread.Invoke(() =>
                {
                    var dialog = new InputBox();
                    dialog.Message = message;
                    dialog.Title = title;
                    dialog.ShowInTaskbar = false;
                    if (dialog.ShowDialog() == true)
                        return dialog.ResponseText;

                    return null;
                });
            }
            finally
            {
                this.uiShell.EnableModeless(1);
            }
        }
    }
}
