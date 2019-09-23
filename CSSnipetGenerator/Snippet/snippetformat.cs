using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

// 
// このソース コードは xsd によって自動生成されました。Version=4.7.3081.0 です。
// 

/// <remarks/>
[Serializable]
[DebuggerStepThrough]
[DesignerCategory("code")]
[GeneratedCode("xsd", "4.7.3081.0")]
[XmlRoot(Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet", IsNullable = false)]
[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet")]
public partial class CodeSnippets
{
    /// <remarks/>
    [XmlElement("CodeSnippet")]
    public CodeSnippet[] CodeSnippet { get; set; }
}

/// <remarks/>
[Serializable]
[DebuggerStepThrough]
[DesignerCategory("code")]
[GeneratedCode("xsd", "4.7.3081.0")]
[XmlRoot(Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet", IsNullable = false)]
[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet")]
public partial class CodeSnippet
{

    /// <remarks/>
    public CodeSnippetHeader Header { get; set; }

    /// <remarks/>
    [XmlArrayItem("Code", typeof(CodeSnippetCode), IsNullable = false)]
    [XmlArrayItem("Declarations", typeof(CodeSnippetDeclarations), IsNullable = false)]
    [XmlArrayItem("Imports", typeof(CodeSnippetImports), IsNullable = false)]
    [XmlArrayItem("References", typeof(CodeSnippetReferences), IsNullable = false)]
    public List<SnippetElement> Snippet { get; set; }

    /// <remarks/>
    [XmlAttribute]
    public string Format { get; set; }
}

public abstract class SnippetElement { }

/// <remarks/>
[Serializable]
[DebuggerStepThrough]
[DesignerCategory("code")]
[GeneratedCode("xsd", "4.7.3081.0")]
[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet")]
public partial class CodeSnippetHeader
{
    /// <remarks/>
    [XmlElement("Author", typeof(string))]
    [XmlElement("Description", typeof(string))]
    [XmlElement("HelpUrl", typeof(string))]
    [XmlElement("Keywords", typeof(CodeSnippetKeywords))]
    [XmlElement("Shortcut", typeof(string))]
    [XmlElement("SnippetTypes", typeof(CodeSnippetSnippetTypes))]
    [XmlElement("Title", typeof(string))]
    [XmlChoiceIdentifier("ItemsElementName")]
    public List<object> Items { get; set; }

    /// <remarks/>
    [XmlIgnore]
    [XmlElement("ItemsElementName")]
    public List<ItemsChoiceType> ItemsElementName { get; set; }

    /// <remarks/>
    [Serializable]
    [GeneratedCode("xsd", "4.7.3081.0")]
    [XmlType(Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet", IncludeInSchema = false)]
    public enum ItemsChoiceType
    {
        /// <remarks/>
        Author,
        /// <remarks/>
        Description,
        /// <remarks/>
        HelpUrl,
        /// <remarks/>
        Keywords,
        /// <remarks/>
        Shortcut,
        /// <remarks/>
        SnippetTypes,
        /// <remarks/>
        Title,
    }
}

/// <remarks/>
[Serializable]
[DebuggerStepThrough]
[DesignerCategory("code")]
[GeneratedCode("xsd", "4.7.3081.0")]
[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet")]
public partial class CodeSnippetKeywords
{
    /// <remarks/>
    [XmlElement("Keyword")]
    public string[] Keyword { get; set; }
}

/// <remarks/>
[Serializable]
[DebuggerStepThrough]
[DesignerCategory("code")]
[GeneratedCode("xsd", "4.7.3081.0")]
[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet")]
public partial class CodeSnippetSnippetTypes
{
    /// <remarks/>
    [XmlElement("SnippetType")]
    public string[] SnippetType { get; set; }
}

/// <remarks/>
[Serializable]
[DebuggerStepThrough]
[DesignerCategory("code")]
[GeneratedCode("xsd", "4.7.3081.0")]
[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet")]
public partial class CodeSnippetCode : SnippetElement
{
    /// <remarks/>
    [XmlAttribute]
    public string Language { get; set; }
    /// <remarks/>
    [XmlAttribute]
    public string Kind { get; set; }
    /// <remarks/>
    [XmlAttribute]
    public string Delimiter { get; set; }
    /// <remarks/>
    [XmlText]
    public string[] Text { get; set; }
}

/// <remarks/>
[Serializable]
[DebuggerStepThrough]
[DesignerCategory("code")]
[GeneratedCode("xsd", "4.7.3081.0")]
[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet")]
public partial class CodeSnippetDeclarations : SnippetElement
{
    /// <remarks/>
    [XmlElement("Literal", typeof(CodeSnippetDeclarationsLiteral))]
    [XmlElement("Object", typeof(CodeSnippetDeclarationsObject))]
    public object[] Items { get; set; }
}

/// <remarks/>
[Serializable]
[DebuggerStepThrough]
[DesignerCategory("code")]
[GeneratedCode("xsd", "4.7.3081.0")]
[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet")]
public partial class CodeSnippetDeclarationsLiteral
{
    /// <remarks/>
    [XmlElement("Default", typeof(string))]
    [XmlElement("Function", typeof(string))]
    [XmlElement("ID", typeof(string))]
    [XmlElement("ToolTip", typeof(string))]
    [XmlElement("Type", typeof(string))]
    [XmlChoiceIdentifier("ItemsElementName")]
    public List<string> Items { get; set; }

    /// <remarks/>
    [XmlIgnore]
    [XmlElement("ItemsElementName")]
    public List<ItemsChoiceType> ItemsElementName { get; set; }

    /// <remarks/>
    [XmlAttribute]
    public bool Editable { get; set; }

    /// <remarks/>
    [XmlIgnore]
    public bool EditableSpecified { get; set; }

    /// <remarks/>
    [Serializable]
    [GeneratedCode("xsd", "4.7.3081.0")]
    [XmlType(Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet", IncludeInSchema = false)]
    public enum ItemsChoiceType
    {
        /// <remarks/>
        Default,
        /// <remarks/>
        Function,
        /// <remarks/>
        ID,
        /// <remarks/>
        ToolTip,
        /// <remarks/>
        Type,
    }
}

/// <remarks/>
[Serializable]
[DebuggerStepThrough]
[DesignerCategory("code")]
[GeneratedCode("xsd", "4.7.3081.0")]
[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet")]
public partial class CodeSnippetDeclarationsObject
{

    /// <remarks/>
    [XmlElement("Default", typeof(string))]
    [XmlElement("Function", typeof(string))]
    [XmlElement("ID", typeof(string))]
    [XmlElement("ToolTip", typeof(string))]
    [XmlElement("Type", typeof(string))]
    [XmlChoiceIdentifier("ItemsElementName")]
    public string[] Items { get; set; }

    /// <remarks/>
    [XmlIgnore]
    [XmlElement("ItemsElementName")]
    public ItemsChoiceType[] ItemsElementName { get; set; }

    /// <remarks/>
    [XmlAttribute]
    public bool Editable { get; set; }

    /// <remarks/>
    [XmlIgnore]
    public bool EditableSpecified { get; set; }
    /// <remarks/>
    [Serializable]
    [GeneratedCode("xsd", "4.7.3081.0")]
    [XmlType(Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet", IncludeInSchema = false)]
    public enum ItemsChoiceType
    {
        /// <remarks/>
        Default,
        /// <remarks/>
        Function,
        /// <remarks/>
        ID,
        /// <remarks/>
        ToolTip,
        /// <remarks/>
        Type,
    }
}

/// <remarks/>
[Serializable]
[DebuggerStepThrough]
[DesignerCategory("code")]
[GeneratedCode("xsd", "4.7.3081.0")]
[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet")]
public partial class CodeSnippetImports : SnippetElement
{
    /// <remarks/>
    [XmlElement("Import")]
    public CodeSnippetImportsImport[] Import { get; set; }
}

/// <remarks/>
[Serializable]
[DebuggerStepThrough]
[DesignerCategory("code")]
[GeneratedCode("xsd", "4.7.3081.0")]
[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet")]
public partial class CodeSnippetImportsImport
{
    /// <remarks/>
    public string Namespace { get; set; }
}

/// <remarks/>
[Serializable]
[DebuggerStepThrough]
[DesignerCategory("code")]
[GeneratedCode("xsd", "4.7.3081.0")]
[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet")]
public partial class CodeSnippetReferences : SnippetElement
{
    /// <remarks/>
    [XmlElement("Reference")]
    public CodeSnippetReferencesReference[] Reference { get; set; }
}

/// <remarks/>
[Serializable]
[DebuggerStepThrough]
[DesignerCategory("code")]
[GeneratedCode("xsd", "4.7.3081.0")]
[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet")]
public partial class CodeSnippetReferencesReference
{
    /// <remarks/>
    [XmlElement("Url", typeof(string))]
    [XmlElement("Assembly", typeof(string))]
    [XmlChoiceIdentifier("ItemsElementName")]
    public string[] Items { get; set; }

    /// <remarks/>
    [XmlIgnore]
    [XmlElement("ItemsElementName")]
    public ItemsChoiceType[] ItemsElementName { get; set; }

    /// <remarks/>
    [Serializable]
    [GeneratedCode("xsd", "4.7.3081.0")]
    [XmlType(Namespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet", IncludeInSchema = false)]
    public enum ItemsChoiceType
    {
        /// <remarks/>
        Assembly,
        /// <remarks/>
        Url,
    }
}
