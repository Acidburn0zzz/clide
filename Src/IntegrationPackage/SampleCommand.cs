﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel.Composition;
using Clide.Commands;
using Clide;
using Microsoft.VisualStudio.Shell;

namespace IntegrationPackage
{
	[PartCreationPolicy(CreationPolicy.Shared)]
	[Command(Constants.PackageGuid, Constants.CommandSet, Constants.cmdHelloClide)]
	public class SampleCommand : ICommandExtension
	{
        private IServiceProvider serviceProvider;

        [ImportingConstructor]
        public SampleCommand([Import(Constants.ShellContractName)] IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

		public string Text
		{
			get { return "Sample"; }
		}

		public void Execute(IMenuCommand command)
		{
            DevEnv
                .Get(this.serviceProvider)
                .MessageBoxService
                .ShowInformation(string.Format(
                    "Clide Version: {0}", typeof(IDevEnv).Assembly.GetName().Version));
        }

		public void QueryStatus(IMenuCommand command)
		{
			command.Enabled = command.Visible = true;
		}
	}
}
