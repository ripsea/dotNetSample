using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Models.TurnKeyItem.SummaryResult
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:GEINV:SummaryResult:3.0")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:GEINV:SummaryResult:3.0", IsNullable = false)]
    public partial class SummaryResult
    {

        private SummaryResultRoutingInfo routingInfoField;

        private SummaryResultDetailList detailListField;

        /// <remarks/>
        public SummaryResultRoutingInfo RoutingInfo
        {
            get
            {
                return this.routingInfoField;
            }
            set
            {
                this.routingInfoField = value;
            }
        }

        /// <remarks/>
        public SummaryResultDetailList DetailList
        {
            get
            {
                return this.detailListField;
            }
            set
            {
                this.detailListField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:GEINV:SummaryResult:3.0")]
    public partial class SummaryResultRoutingInfo
    {

        private SummaryResultRoutingInfoFrom fromField;

        private SummaryResultRoutingInfoFromVAC fromVACField;

        private SummaryResultRoutingInfoTO toField;

        private SummaryResultRoutingInfoToVAC toVACField;

        /// <remarks/>
        public SummaryResultRoutingInfoFrom From
        {
            get
            {
                return this.fromField;
            }
            set
            {
                this.fromField = value;
            }
        }

        /// <remarks/>
        public SummaryResultRoutingInfoFromVAC FromVAC
        {
            get
            {
                return this.fromVACField;
            }
            set
            {
                this.fromVACField = value;
            }
        }

        /// <remarks/>
        public SummaryResultRoutingInfoTO To
        {
            get
            {
                return this.toField;
            }
            set
            {
                this.toField = value;
            }
        }

        /// <remarks/>
        public SummaryResultRoutingInfoToVAC ToVAC
        {
            get
            {
                return this.toVACField;
            }
            set
            {
                this.toVACField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:GEINV:SummaryResult:3.0")]
    public partial class SummaryResultRoutingInfoFrom
    {

        private uint partyIdField;

        /// <remarks/>
        public uint PartyId
        {
            get
            {
                return this.partyIdField;
            }
            set
            {
                this.partyIdField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:GEINV:SummaryResult:3.0")]
    public partial class SummaryResultRoutingInfoFromVAC
    {

        private string routingIdField;

        /// <remarks/>
        public string RoutingId
        {
            get
            {
                return this.routingIdField;
            }
            set
            {
                this.routingIdField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:GEINV:SummaryResult:3.0")]
    public partial class SummaryResultRoutingInfoTO
    {

        private byte partyIdField;

        /// <remarks/>
        public byte PartyId
        {
            get
            {
                return this.partyIdField;
            }
            set
            {
                this.partyIdField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:GEINV:SummaryResult:3.0")]
    public partial class SummaryResultRoutingInfoToVAC
    {

        private byte routingIdField;

        /// <remarks/>
        public byte RoutingId
        {
            get
            {
                return this.routingIdField;
            }
            set
            {
                this.routingIdField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:GEINV:SummaryResult:3.0")]
    public partial class SummaryResultDetailList
    {

        private SummaryResultDetailListMessage messageField;

        /// <remarks/>
        public SummaryResultDetailListMessage Message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:GEINV:SummaryResult:3.0")]
    public partial class SummaryResultDetailListMessage
    {

        private SummaryResultDetailListMessageInfo infoField;

        private SummaryResultDetailListMessageResultType resultTypeField;

        /// <remarks/>
        public SummaryResultDetailListMessageInfo Info
        {
            get
            {
                return this.infoField;
            }
            set
            {
                this.infoField = value;
            }
        }

        /// <remarks/>
        public SummaryResultDetailListMessageResultType ResultType
        {
            get
            {
                return this.resultTypeField;
            }
            set
            {
                this.resultTypeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:GEINV:SummaryResult:3.0")]
    public partial class SummaryResultDetailListMessageInfo
    {

        private string idField;

        private byte sizeField;

        private string messageTypeField;

        private string serviceField;

        private string actionField;

        /// <remarks/>
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public byte Size
        {
            get
            {
                return this.sizeField;
            }
            set
            {
                this.sizeField = value;
            }
        }

        /// <remarks/>
        public string MessageType
        {
            get
            {
                return this.messageTypeField;
            }
            set
            {
                this.messageTypeField = value;
            }
        }

        /// <remarks/>
        public string Service
        {
            get
            {
                return this.serviceField;
            }
            set
            {
                this.serviceField = value;
            }
        }

        /// <remarks/>
        public string Action
        {
            get
            {
                return this.actionField;
            }
            set
            {
                this.actionField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:GEINV:SummaryResult:3.0")]
    public partial class SummaryResultDetailListMessageResultType
    {

        private SummaryResultDetailListMessageResultTypeTotal totalField;

        private SummaryResultDetailListMessageResultTypeGood goodField;

        private SummaryResultDetailListMessageResultTypeFailed failedField;

        /// <remarks/>
        public SummaryResultDetailListMessageResultTypeTotal Total
        {
            get
            {
                return this.totalField;
            }
            set
            {
                this.totalField = value;
            }
        }

        /// <remarks/>
        public SummaryResultDetailListMessageResultTypeGood Good
        {
            get
            {
                return this.goodField;
            }
            set
            {
                this.goodField = value;
            }
        }

        /// <remarks/>
        public SummaryResultDetailListMessageResultTypeFailed Failed
        {
            get
            {
                return this.failedField;
            }
            set
            {
                this.failedField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:GEINV:SummaryResult:3.0")]
    public partial class SummaryResultDetailListMessageResultTypeTotal
    {

        private SummaryResultDetailListMessageResultTypeTotalResultDetailType resultDetailTypeField;

        /// <remarks/>
        public SummaryResultDetailListMessageResultTypeTotalResultDetailType ResultDetailType
        {
            get
            {
                return this.resultDetailTypeField;
            }
            set
            {
                this.resultDetailTypeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:GEINV:SummaryResult:3.0")]
    public partial class SummaryResultDetailListMessageResultTypeTotalResultDetailType
    {

        private byte countField;

        private SummaryResultDetailListMessageResultTypeTotalResultDetailTypeInvoice[] invoicesField;

        /// <remarks/>
        public byte Count
        {
            get
            {
                return this.countField;
            }
            set
            {
                this.countField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Invoice", IsNullable = false)]
        public SummaryResultDetailListMessageResultTypeTotalResultDetailTypeInvoice[] Invoices
        {
            get
            {
                return this.invoicesField;
            }
            set
            {
                this.invoicesField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:GEINV:SummaryResult:3.0")]
    public partial class SummaryResultDetailListMessageResultTypeTotalResultDetailTypeInvoice
    {

        private string referenceNumberField;

        private uint invoiceDateField;

        /// <remarks/>
        public string ReferenceNumber
        {
            get
            {
                return this.referenceNumberField;
            }
            set
            {
                this.referenceNumberField = value;
            }
        }

        /// <remarks/>
        public uint InvoiceDate
        {
            get
            {
                return this.invoiceDateField;
            }
            set
            {
                this.invoiceDateField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:GEINV:SummaryResult:3.0")]
    public partial class SummaryResultDetailListMessageResultTypeGood
    {

        private SummaryResultDetailListMessageResultTypeGoodResultDetailType resultDetailTypeField;

        /// <remarks/>
        public SummaryResultDetailListMessageResultTypeGoodResultDetailType ResultDetailType
        {
            get
            {
                return this.resultDetailTypeField;
            }
            set
            {
                this.resultDetailTypeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:GEINV:SummaryResult:3.0")]
    public partial class SummaryResultDetailListMessageResultTypeGoodResultDetailType
    {

        private byte countField;

        private SummaryResultDetailListMessageResultTypeGoodResultDetailTypeInvoice[] invoicesField;

        /// <remarks/>
        public byte Count
        {
            get
            {
                return this.countField;
            }
            set
            {
                this.countField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Invoice", IsNullable = false)]
        public SummaryResultDetailListMessageResultTypeGoodResultDetailTypeInvoice[] Invoices
        {
            get
            {
                return this.invoicesField;
            }
            set
            {
                this.invoicesField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:GEINV:SummaryResult:3.0")]
    public partial class SummaryResultDetailListMessageResultTypeGoodResultDetailTypeInvoice
    {

        private string referenceNumberField;

        private uint invoiceDateField;

        /// <remarks/>
        public string ReferenceNumber
        {
            get
            {
                return this.referenceNumberField;
            }
            set
            {
                this.referenceNumberField = value;
            }
        }

        /// <remarks/>
        public uint InvoiceDate
        {
            get
            {
                return this.invoiceDateField;
            }
            set
            {
                this.invoiceDateField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:GEINV:SummaryResult:3.0")]
    public partial class SummaryResultDetailListMessageResultTypeFailed
    {

        private SummaryResultDetailListMessageResultTypeFailedResultDetailType resultDetailTypeField;

        /// <remarks/>
        public SummaryResultDetailListMessageResultTypeFailedResultDetailType ResultDetailType
        {
            get
            {
                return this.resultDetailTypeField;
            }
            set
            {
                this.resultDetailTypeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:GEINV:SummaryResult:3.0")]
    public partial class SummaryResultDetailListMessageResultTypeFailedResultDetailType
    {

        private byte countField;

        /// <remarks/>
        public byte Count
        {
            get
            {
                return this.countField;
            }
            set
            {
                this.countField = value;
            }
        }
    }


}