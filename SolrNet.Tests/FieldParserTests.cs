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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using NUnit.Framework;
using SolrNet.Impl.FieldParsers;

namespace SolrNet.Tests {
    [TestFixture]
    public class FieldParserTests {
        [TestCase(typeof(string))]
        [TestCase(typeof(Dictionary<,>))]
        [TestCase(typeof(IDictionary<,>))]
        [TestCase(typeof(IDictionary<int, int>))]
        [TestCase(typeof(IDictionary))]
        [TestCase(typeof(Hashtable))]
        public void CollectionFieldParser_cant_handle_types(Type t) {
            var p = new CollectionFieldParser(null);
            Assert.IsFalse(p.CanHandleType(t));
        }

        [TestCase(typeof(IEnumerable))]
        [TestCase(typeof(IEnumerable<>))]
        [TestCase(typeof(IEnumerable<int>))]
        [TestCase(typeof(ICollection))]
        [TestCase(typeof(ICollection<>))]
        [TestCase(typeof(ICollection<int>))]
        [TestCase(typeof(IList))]
        [TestCase(typeof(IList<>))]
        [TestCase(typeof(IList<int>))]
        [TestCase(typeof(ArrayList))]
        [TestCase(typeof(List<>))]
        [TestCase(typeof(List<int>))]
        public void CollectionFieldParser_can_handle_types(Type t) {
            var p = new CollectionFieldParser(null);
            Assert.IsTrue(p.CanHandleType(t));
        }

        private enum Numbers {
            One,
            Two
        }

        [Test]
        public void DecimalFieldParser() {
            var p = new DecimalFieldParser();
            var xml = new XDocument();
            xml.Add(new XElement("item", "6.66E13"));
            var value = (decimal) p.Parse(xml.Root, typeof(decimal));
            Assert.AreEqual(66600000000000m, value);
        }

        [Test]
        public void DecimalFieldParser_overflow() {
            Assert.Throws<OverflowException>(() => {
                var p = new DecimalFieldParser();
                var xml = new XDocument();
                xml.Add(new XElement("item", "6.66E53"));
                var value = (decimal) p.Parse(xml.Root, typeof(decimal));
            });
        }

        [Test]
        public void DefaultFieldParser_EnumAsString() {
            var p = new DefaultFieldParser();
            var xml = new XDocument();
            xml.Add(new XElement("str", "One"));
            var r = p.Parse(xml.Root, typeof(Numbers));
            Assert.IsInstanceOf<Numbers>(r);
        }

        [Test]
        public void DoubleFieldParser() {
            var p = new DoubleFieldParser();
            var xml = new XDocument();
            xml.Add(new XElement("item", "123.99"));
            p.Parse(xml.Root, typeof(float));
        }

        [Test]
        public void EnumAsString() {
            var p = new EnumFieldParser();
            var xml = new XDocument();
            xml.Add(new XElement("str", "One"));
            var r = p.Parse(xml.Root, typeof(Numbers));
            Assert.IsInstanceOf<Numbers>(r);
        }

        [Test]
        public void FloatFieldParser_cant_handle_string() {
            var p = new FloatFieldParser();
            var xml = new XDocument();
            xml.Add(new XElement("str", "pepe"));
            Assert.Throws<FormatException>(() => p.Parse(xml.Root, null));
        }

        [Test]
        public void FloatFieldParser_Parse() {
            var p = new FloatFieldParser();
            var xml = new XDocument();
            xml.Add(new XElement("int", "31"));
            var v = p.Parse(xml.Root, null);
            Assert.IsInstanceOf<float>(v);
            Assert.AreEqual(31f, v);
        }

        [Test]
        public void SupportGuid() {
            var p = new DefaultFieldParser();
            var g = Guid.NewGuid();
            var xml = new XDocument();
            xml.Add(new XElement("str", g.ToString()));
            var r = p.Parse(xml.Root, typeof(Guid));
            var pg = (Guid) r;
            Assert.AreEqual(g, pg);
        }

        [Test]
        public void SupportsNullableGuid() {
            var p = new DefaultFieldParser();
            var g = Guid.NewGuid();
            var xml = new XDocument();
            xml.Add(new XElement("str", g.ToString()));
            var r = p.Parse(xml.Root, typeof(Guid?));
            var pg = (Guid?) r;
            Assert.AreEqual(g, pg.Value);
        }
    }
}