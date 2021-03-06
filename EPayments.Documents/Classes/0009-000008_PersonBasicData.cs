//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_0009_000008
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/0009-000008";
	}




	[XmlType(TypeName="PersonBasicData",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class PersonBasicData
	{

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(R_0009_000005.PersonNames),ElementName="Names",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000005.PersonNames __Names;
		
		[XmlIgnore]
		public R_0009_000005.PersonNames Names
		{
			get {return __Names;}
			set {__Names = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(R_0009_000006.PersonIdentifier),ElementName="Identifier",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000006.PersonIdentifier __Identifier;
		
		[XmlIgnore]
		public R_0009_000006.PersonIdentifier Identifier
		{
			get {return __Identifier;}
			set {__Identifier = value;}
		}

		public PersonBasicData()
		{
		}
	}
}
