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

namespace Hexa.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class PagedElements<TEntity>
        where TEntity : class
    {
        #region Constructors

        public PagedElements(IEnumerable<TEntity> elements, int totalElements)
        {
            this.Elements = elements;
            this.TotalElements = totalElements;
        }

        #endregion Constructors

        #region Properties

        [DataMember]
        public IEnumerable<TEntity> Elements
        {
            get;
            private set;
        }

        [DataMember]
        public int TotalElements
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods

        public int TotalPages(int pageSize)
        {
            return (int)Math.Ceiling(Convert.ToDouble(this.TotalElements) / pageSize);
        }

        #endregion Methods
    }
}