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

using System;
namespace Clide.Composition
{
    /// <summary>
    /// Exposes the contract names for exports of general purpose services which 
    /// are not VS-specific types.
    /// </summary>
    public static class ContractNames
    {
        private const string Prefix = "Clide.";

        /// <summary>
        /// Contract name for accessing ICompositionService.
        /// </summary>
        public const string ICompositionService = Prefix + "ICompositionService";

        /// <summary>
        /// Contract name for accessing the ExportProvider.
        /// </summary>
        public const string ExportProvider = Prefix + "ExportProvider";

        /// <summary>
        /// Gets the local contract name for accessing ICompositionService.
        /// </summary>
        internal static string LocalCompositionService(Guid hostId)
        {
            return AsLocal(hostId, ContractNames.ICompositionService);
        }

        /// <summary>
        /// Gets the local contract name for accessing the ExportProvider.
        /// </summary>
        internal static string LocalExportProvider(Guid hostId)
        {
            return AsLocal(hostId, ContractNames.ExportProvider);
        }

        /// <summary>
        /// Gets the local contract name for the given host and bare contract.
        /// </summary>
        internal static string AsLocal(Guid hostId, string contractName)
        {
            return contractName + "-" + hostId;
        }
    }
}
