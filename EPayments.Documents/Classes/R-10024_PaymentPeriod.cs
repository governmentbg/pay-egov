//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_10024
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-10024";
	}




	[XmlType(TypeName="PaymentPeriod",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class PaymentPeriod
	{

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(ElementName="PaymentPeriodFromDate",Form=XmlSchemaForm.Qualified,DataType="date",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? __PaymentPeriodFromDate;
		
		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __PaymentPeriodFromDateSpecified { get { return __PaymentPeriodFromDate.HasValue; } }
		
		[XmlIgnore]
		public DateTime? PaymentPeriodFromDate
		{ 
			get { return __PaymentPeriodFromDate; }
			set { __PaymentPeriodFromDate = value; }
		}
		


		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(ElementName="PaymentPeriodToDate",Form=XmlSchemaForm.Qualified,DataType="date",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? __PaymentPeriodToDate;
		
		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __PaymentPeriodToDateSpecified { get { return __PaymentPeriodToDate.HasValue; } }
		
		[XmlIgnore]
		public DateTime? PaymentPeriodToDate
		{ 
			get { return __PaymentPeriodToDate; }
			set { __PaymentPeriodToDate = value; }
		}
		


		public PaymentPeriod()
		{
		}
	}
}
