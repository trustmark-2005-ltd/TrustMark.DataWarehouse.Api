using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SampleApp.Models
{
    public class CreateLodgementRequest
    {
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("complete")]
        public bool Complete { get; set; }

        [JsonProperty("pas2035Roles")]
        public Pas2035Roles Pas2035Roles { get; set; }

        [JsonProperty("address")]
        public Address Address { get; set; }

        [JsonProperty("tenure")]
        public Tenure Tenure { get; set; }

        [JsonProperty("propertyInformation")]
        public PropertyInformation PropertyInformation { get; set; }

        [JsonProperty("measures")]
        public List<Measure> Measures { get; set; }

        [JsonProperty("documents")]
        public List<Document> Documents { get; set; }

        [JsonProperty("annualCostSaving")]
        public int? AnnualCostSaving { get; set; }

        [JsonProperty("workInvoiceTotal")]
        public int? WorkInvoiceTotal { get; set; }

        [JsonProperty("preEPC")]
        public string PreEPC { get; set; }

        [JsonProperty("postEPC")]
        public string PostEPC { get; set; }
    }

    public class Pas2035Roles
    {
        [JsonProperty("advisorTMLN")]
        public string AdvisorTMLN { get; set; }

        [JsonProperty("assessorTMLN")]
        public string AssessorTMLN { get; set; }

        [JsonProperty("designerEmail")]
        public string DesignerEmail { get; set; }

        [JsonProperty("installerTMLN")]
        public string InstallerTMLN { get; set; }

        [JsonProperty("evaluatorTMLN")]
        public string EvaluatorTMLN { get; set; }
    }

    public class Address
    {
        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("flatNameNumber")]
        public string FlatNameNumber { get; set; }

        [JsonProperty("postcode")]
        public string Postcode { get; set; }

        [JsonProperty("uprn")]
        public string Uprn { get; set; }

        [JsonProperty("address1")]
        public string Address1 { get; set; }

        [JsonProperty("address2")]
        public string Address2 { get; set; }

        [JsonProperty("address3")]
        public string Address3 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("county")]
        public string County { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }
    }

    public class Tenure
    {
        [JsonProperty("premisesTenure")]
        public string PremisesTenure { get; set; }

        [JsonProperty("residentName")]
        public string ResidentName { get; set; }

        [JsonProperty("residentContactNumber")]
        public string ResidentContactNumber { get; set; }

        [JsonProperty("residentAlternativeContactNumber")]
        public string ResidentAlternativeContactNumber { get; set; }

        [JsonProperty("ownerName")]
        public string OwnerName { get; set; }

        [JsonProperty("ownerContactNumber")]
        public string OwnerContactNumber { get; set; }

        [JsonProperty("ownerAlternativeContactNumber")]
        public string OwnerAlternativeContactNumber { get; set; }

        [JsonProperty("ownerEmail")]
        public string OwnerEmail { get; set; }
    }

    public class PropertyInformation
    {
        [JsonProperty("propertyType")]
        public string PropertyType { get; set; }

        [JsonProperty("propertyDetachment")]
        public string PropertyDetachment { get; set; }

        [JsonProperty("propertyBedrooms")]
        public string PropertyBedrooms { get; set; }

        [JsonProperty("propertyTypeName")]
        public string PropertyTypeName { get; set; }

        [JsonProperty("propertyAge")]
        public string PropertyAge { get; set; }

        [JsonProperty("propertyConstruction")]
        public string PropertyConstruction { get; set; }

        [JsonProperty("uniquePropertyReferenceNumber")]
        public string UniquePropertyReferenceNumber { get; set; }
    }

    public class Document
    {
        [JsonProperty("uploadToken")]
        public string UploadToken { get; set; }

        [JsonProperty("documentType")]
        public string DocumentType { get; set; }

        [JsonProperty("pas2035Role")]
        public string Role { get; set; }

        [JsonProperty("pas2035RoleTMLN")]
        public string RoleTMLN { get; set; }
    }

    public class Measure
    {
        [JsonProperty("general")]
        public MeasureGeneral General { get; set; }

        [JsonProperty("guarantee")]
        public MeasureGuarantee Guarantee { get; set; }

        [JsonProperty("additional")]
        public MeasureAdditional Additional { get; set; }

        [JsonProperty("product")]
        public MeasureProduct Product { get; set; }

        [JsonProperty("obligation")]
        public MeasureObligation Obligation { get; set; }
    }

    public class MeasureGeneral
    {
        [JsonProperty("installedDate")]
        public DateTime InstalledDate { get; set; }

        [JsonProperty("workTypeId")]
        public string WorkTypeId { get; set; }

        [JsonProperty("workType")]
        public string WorkType { get; set; }

        [JsonProperty("measureCategory")]
        public string MeasureCategory { get; set; }

        [JsonProperty("measureType")]
        public string measureType { get; set; }

        [JsonProperty("pasAnnex")]
        public string PasAnnex { get; set; }

        [JsonProperty("standard")]
        public string Standard { get; set; }

        [JsonProperty("handoverDate")]
        public DateTime? HandoverDate { get; set; }

        [JsonProperty("installerReferenceNumber")]
        public string InstallerReferenceNumber { get; set; }

        [JsonProperty("supplierReferenceNumber")]
        public string SupplierReferenceNumber { get; set; }

        [JsonProperty("subInstallerName")]
        public string SubInstallerName { get; set; }

        [JsonProperty("subInstallerTrustMarkLicenceNumber")]
        public string SubInstallerTrustMarkLicenceNumber { get; set; }

        [JsonProperty("installerPasCertificateNumber")]
        public string InstallerPasCertificateNumber { get; set; }
    }

    public class MeasureGuarantee
    {
        [JsonProperty("guaranteeSelect")]
        public string GuaranteeSelect { get; set; }

        [JsonProperty("fileUpload")]
        public string FileUpload { get; set; }

        [JsonProperty("guaranteeIssuedBy")]
        public string GuaranteeIssuedBy { get; set; }

        [JsonProperty("guaranteePolicyReference")]
        public string GuaranteePolicyReference { get; set; }

        [JsonProperty("guaranteeStartDate")]
        public DateTime? GuaranteeStartDate { get; set; }

        [JsonProperty("guaranteeCoverDuration")]
        public int? GuaranteeCoverDuration { get; set; }
    }

    public class MeasureAdditional
    {
        [JsonProperty("has100PercentMeasureInstalled")]
        public string Has100PercentMeasureInstalled { get; set; }

        [JsonProperty("operativeName")]
        public string OperativeName { get; set; }

        [JsonProperty("operativeCertificationReference")]
        public string OperativeCertificationReference { get; set; }
    }

    public class MeasureProduct
    {
        [JsonProperty("productManufacturer")]
        public string ProductManufacturer { get; set; }

        [JsonProperty("productModel")]
        public string ProductModel { get; set; }

        [JsonProperty("productVersion")]
        public string ProductVersion { get; set; }

        [JsonProperty("productSerialNumber")]
        public string ProductSerialNumber { get; set; }

        [JsonProperty("productWarrantyDuration")]
        public string ProductWarrantyDuration { get; set; }

        [JsonProperty("productWarrantyUpload")]
        public string ProductWarrantyUpload { get; set; }
    }

    public class MeasureObligation
    {
        [JsonProperty("overallObligationPeriod")]
        public string OverallObligationPeriod { get; set; }

        [JsonProperty("ecoSupplierReference")]
        public string EcoSupplierReference { get; set; }
    }
}
