﻿using System.Collections.Generic;
using System.IO;

namespace SolrNet {
    /// <summary>
    /// Contains parameters that can be specified when extracting a rich document to the index.
    /// </summary>
    /// <remarks>
    /// See http://wiki.apache.org/solr/ExtractingRequestHandler#Input_Parameters
    /// </remarks>
    public class ExtractParameters {
        /// <summary>
        /// Constructs a new ExtractParameters with required values
        /// </summary>
        /// <param name="content"></param>
        /// <param name="id"></param>
        /// <param name="resourceName"></param>
        public ExtractParameters(Stream content, string id, string resourceName) {
            Id = id;
            ResourceName = resourceName;
            Content = content;
        }

        /// <summary>
        /// Constructs a new ExtractParameters with required values
        /// </summary>
        /// <param name="content"></param>
        /// <param name="id"></param>
        public ExtractParameters(FileStream content, string id) {
            Id = id;
            ResourceName = content.Name;
            Content = content;
        }

        ///<summary>
        /// Provides the necessary unique id for the document being indexed 
        ///</summary>
        public string Id { get; private set; }

        ///<summary>
        /// Name of the file Tika can use it as a hint for detecting mime type.
        ///</summary>
        public string ResourceName { get; private set; }

        /// <summary>
        /// Causes Solr to do a commit after indexing the document, making it immediately searchable. 
        /// </summary>
        /// <remarks>
        /// For good performance when loading many documents, don't call commit until you are done. 
        /// </remarks>
        public bool AutoCommit { get; set; }

        /// <summary>
        /// If true, return the extracted content from Tika without indexing the document. 
        /// This literally includes the extracted XHTML as a string in the response. 
        /// </summary>
        public bool ExtractOnly { get; set; }

        /// <summary>
        /// The format to specify for extraction
        /// </summary>
        public ExtractFormat ExtractFormat { get; set; }

        /// <summary>
        /// Index attributes of the Tika XHTML elements into separate fields, named after the element. 
        /// For example, when extracting from HTML, Tika can return the href attributes in &lt;a&gt; tags as fields named "a".
        /// </summary>
        public bool CaptureAttributes { get; set; }

        /// <summary>
        /// Tika XHTML NAME: Capture XHTML elements with the name separately for adding to the Solr document. 
        /// This can be useful for grabbing chunks of the XHTML into a separate field. For instance, it could be used to grab paragraphs (&lt;p&gt;) 
        /// and index them into a separate field.
        /// </summary>
        /// <remarks>
        /// Content is also still captured into the overall "content" field. 
        /// </remarks>
        public string Capture { get; set; }

        /// <summary>
        /// Prefix all fields that are not defined in the schema with the given prefix. 
        /// This is very useful when combined with dynamic field definitions. 
        /// </summary>
        /// <example>
        /// Setting Prefix to false would effectively ignore all unknown fields generated by Tika given the example schema contains 
        /// <dynamicField name="ignored_*" type="ignored"/>
        /// </example>
        public string Prefix { get; set; }

        /// <summary>
        /// If uprefix is not specified and a Field cannot be determined, the default field will be used.
        /// </summary>
        public string DefaultField { get; set; }

        /// <summary>
        /// Collection of fields and thier specified value.
        /// </summary>
        public IEnumerable<ExtractField> Fields { get; set; }

        /// <summary>
        /// When extracting, only return Tika XHTML content that satisfies the XPath expression. 
        /// See http://lucene.apache.org/tika/documentation.html for details on the format of Tika XHTML.
        /// </summary>
        public string XPath { get; set; }

        /// <summary>
        /// Map all field names to lowercase with underscores. For example, Content-Type would be mapped to content_type.
        /// </summary>
        public bool LowerNames { get; set; }


        /// <summary>
        /// Mime type of the file - if provided, Tika won't have to try to infer it from the ResourceName and content
        /// </summary>
        public string StreamType { get; set; }

        /// <summary>
        /// The rich document to index
        /// </summary>
        public Stream Content { get; private set; }
    }
}