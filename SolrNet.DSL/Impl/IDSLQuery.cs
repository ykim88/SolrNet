﻿#region license

// Copyright (c) 2007-2010 Mauricio Scheffer
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//      http://www.apache.org/licenses/LICENSE-2.0
//  
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

namespace SolrNet.DSL.Impl {
    public interface IDSLQuery<T> : IDSLRun<T> {
        IDSLQueryRange<T> ByRange<RT>(string fieldName, RT from, RT to);
        IDSLQueryBy<T> By(string fieldName);
    }
}