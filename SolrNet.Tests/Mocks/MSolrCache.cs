﻿using System;
using SolrNet.Impl;
using SolrNet.Tests.Utils;

namespace SolrNet.Tests.Mocks {
    public class MSolrCache : ISolrCache {
        public MFunc<string, SolrCacheEntity> get;

        public SolrCacheEntity this[string url] {
            get { return get.Invoke(url); }
        }

        public MFunc<SolrCacheEntity, Unit> add;

        public void Add(SolrCacheEntity e) {
            add.Invoke(e);
        }
    }
}