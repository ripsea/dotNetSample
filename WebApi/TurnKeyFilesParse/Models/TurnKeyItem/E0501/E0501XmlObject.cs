namespace Api.Model.TurnKeyItem.E0501
{

        // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
        [System.SerializableAttribute]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:GEINV:InvoiceEnvelope:3.1")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:GEINV:InvoiceEnvelope:3.1", IsNullable = false)]
        public class InvoiceEnvelope
        {

            private InvoiceEnvelopeFrom fromField;

            private InvoiceEnvelopeFromVAC fromVACField;

            private InvoiceEnvelopeTO toField;

            private InvoiceEnvelopeToVAC toVACField;

            private InvoiceEnvelopeInvoicePack invoicePackField;

            /// <remarks/>
            public InvoiceEnvelopeFrom From
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
            public InvoiceEnvelopeFromVAC FromVAC
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
            public InvoiceEnvelopeTO To
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
            public InvoiceEnvelopeToVAC ToVAC
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

            /// <remarks/>
            public InvoiceEnvelopeInvoicePack InvoicePack
            {
                get
                {
                    return this.invoicePackField;
                }
                set
                {
                    this.invoicePackField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:GEINV:InvoiceEnvelope:3.1")]
        public partial class InvoiceEnvelopeFrom
        {

            private string partyIdField;

            /// <remarks/>
            public string PartyId
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:GEINV:InvoiceEnvelope:3.1")]
        public partial class InvoiceEnvelopeFromVAC
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:GEINV:InvoiceEnvelope:3.1")]
        public partial class InvoiceEnvelopeTO
        {

            private string partyIdField;

            /// <remarks/>
            public string PartyId
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:GEINV:InvoiceEnvelope:3.1")]
        public partial class InvoiceEnvelopeToVAC
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:GEINV:InvoiceEnvelope:3.1")]
        public partial class InvoiceEnvelopeInvoicePack
        {

            private InvoiceAssignNo[] invoiceAssignNoField;

            private int countField;

            private string messageTypeField;

            private string versionField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("InvoiceAssignNo", Namespace = "urn:GEINV:E0501:3.1")]
            public InvoiceAssignNo[] InvoiceAssignNo
            {
                get
                {
                    return this.invoiceAssignNoField;
                }
                set
                {
                    this.invoiceAssignNoField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public int count
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
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string messageType
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
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string version
            {
                get
                {
                    return this.versionField;
                }
                set
                {
                    this.versionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:GEINV:E0501:3.1")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:GEINV:E0501:3.1", IsNullable = false)]
        public partial class InvoiceAssignNo
        {

            private string banField;

            private string invoiceTypeField;

            private string yearMonthField;

            private string invoiceTrackField;

            private string invoiceBeginNoField;

            private string invoiceEndNoField;

            private int invoiceBookletField;

            /// <remarks/>
            public string Ban
            {
                get
                {
                    return this.banField;
                }
                set
                {
                    this.banField = value;
                }
            }

            /// <remarks/>
            public string InvoiceType
            {
                get
                {
                    return this.invoiceTypeField;
                }
                set
                {
                    this.invoiceTypeField = value;
                }
            }

            /// <remarks/>
            public string YearMonth
            {
                get
                {
                    return this.yearMonthField;
                }
                set
                {
                    this.yearMonthField = value;
                }
            }

            /// <remarks/>
            public string InvoiceTrack
            {
                get
                {
                    return this.invoiceTrackField;
                }
                set
                {
                    this.invoiceTrackField = value;
                }
            }

            /// <remarks/>
            public string InvoiceBeginNo
            {
                get
                {
                    return this.invoiceBeginNoField;
                }
                set
                {
                    this.invoiceBeginNoField = value;
                }
            }

            /// <remarks/>
            public string InvoiceEndNo
            {
                get
                {
                    return this.invoiceEndNoField;
                }
                set
                {
                    this.invoiceEndNoField = value;
                }
            }

            /// <remarks/>
            public int InvoiceBooklet
            {
                get
                {
                    return this.invoiceBookletField;
                }
                set
                {
                    this.invoiceBookletField = value;
                }
            }
        }


    }
