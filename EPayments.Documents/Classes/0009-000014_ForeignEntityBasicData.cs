//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_0009_000014
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/0009-000014";
	}




	[XmlType(TypeName="ForeignEntityBasicData",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class ForeignEntityBasicData
	{

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(ElementName="ForeignEntityName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __ForeignEntityName;
		
		[XmlIgnore]
		public string ForeignEntityName
		{ 
			get { return __ForeignEntityName; }
			set { __ForeignEntityName = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(ElementName="CountryISO3166TwoLetterCode",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __CountryISO3166TwoLetterCode;
		
		[XmlIgnore]
		public string CountryISO3166TwoLetterCode
		{ 
			get { return __CountryISO3166TwoLetterCode; }
			set { __CountryISO3166TwoLetterCode = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(ElementName="CountryNameCyrillic",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __CountryNameCyrillic;
		
		[XmlIgnore]
		public string CountryNameCyrillic
		{ 
			get { return __CountryNameCyrillic; }
			set { __CountryNameCyrillic = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(ElementName="ForeignEntityRegister",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __ForeignEntityRegister;
		
		[XmlIgnore]
		public string ForeignEntityRegister
		{ 
			get { return __ForeignEntityRegister; }
			set { __ForeignEntityRegister = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(ElementName="ForeignEntityIdentifier",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __ForeignEntityIdentifier;
		
		[XmlIgnore]
		public string ForeignEntityIdentifier
		{ 
			get { return __ForeignEntityIdentifier; }
			set { __ForeignEntityIdentifier = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(ElementName="ForeignEntityOtherData",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __ForeignEntityOtherData;
		
		[XmlIgnore]
		public string ForeignEntityOtherData
		{ 
			get { return __ForeignEntityOtherData; }
			set { __ForeignEntityOtherData = value; }
		}

		public ForeignEntityBasicData()
		{
		}
	}
}
