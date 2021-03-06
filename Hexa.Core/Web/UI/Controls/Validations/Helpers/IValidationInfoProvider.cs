#region Header

// ===================================================================================
// Copyright 2010 HexaSystems Corporation
// ===================================================================================
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// ===================================================================================
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// See the License for the specific language governing permissions and
// ===================================================================================

#endregion Header

namespace Hexa.Core.Web.UI.Controls.Validation
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Validation Provider Interface.
    /// </summary>
    public interface IValidationInfoProvider
    {
        #region Methods

        /// <summary>
        /// Gets the validation info.
        /// </summary>
        /// <returns></returns>
        IList<IValidationInfo> GetValidationInfo();

        /// <summary>
        /// Gets the validation info.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        IList<IValidationInfo> GetValidationInfo(string propertyName);

        #endregion Methods
    }

    /// <summary>
    /// Validation Provider Interface for TEntity.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    [SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces")]
    public interface IValidationInfoProvider<TEntity> : IValidationInfoProvider
    {
    }
}