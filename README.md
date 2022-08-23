# TrustMark.DataWarehouse.Api

**This API currently supports lodgement for work delivered through ECO only. Other initiatives such as GHGVS, LADS, WHD are not supported.** 

The TrustMark .NET library provides integration access to the TrustMark DataWarehouse API.

The Lodgement Data Dictionary provides further guindance on data requirements: https://www.trustmark.org.uk/ourservices/data-warehouse/data-dictionary

## Prerequisites

You must have a TrustMark account and API key in order to run this project - if you need to obtain an API key please email data@trustmark.org.uk and we will arrange a discussion to understand your needs and plan next steps.

TrustMark will provide access to a ‘Sandbox’ environment for development and integration testing. Before API keys for the live environment can be provided you must successfully complete lodgements into the Sandbox environment that represent your use cases.

Once TrustMark has validated the test lodgements a Data Sharing Agreement will be issued and must be signed before the keys are issued.

Several organisations have successfully integrated with the Data Warehouse utilising the sample code and postman examples that have been available since 20 August 2019. This has permitted sufficient time to adopt the API. From 22 June 2020 free technical support for integration will only be available to parties that have engaged and either signed a Data Sharing Agreement or have API keys for the Sandbox environment.


## Postman

If you are uncertain about an integration or just want to test the API endpoints - there is a Postman link available https://www.getpostman.com/collections/64b2a1e75e1fcd7b65a3. Just update the variables in the API Collection with your own `trustmarkid` and `tmapikey` values which you can get from the API section under My Account on your Lodgement Portal login.

## Key Concepts

### Lodgement

A lodgement record can be created with a POST to the API. The root element contains some key properties:

Version: This value must be 2019-08-01
Type: This must be either PAS2030 or PAS2035
Complete: If true the lodgement will attempt to automatically complete; otherwise false. Optional.

For PAS2035 lodgements the pas2035Roles section must be present in the lodgement create request.

Other sections of the lodgement include:
- address
- tenure
- propertyInformation
- documents (0 .. 50)
- measures (0 .. 6)

A lodgement can be complete in a single POST but can also be complete in several steps as the life-cycle needs - either will a separate Complete call or by use or multiple AttachResponse calls before the final Complete call.

### Measure

Each measure is provided with the following sections:
- general
- guarantee
- additional
- product
- obligation

You can attach up to 6 measures per lodgement in the Measures section of the lodgement.

### Document

You must first complete the UploadFile call to receive an UploadToken, you can then attach each document to the lodgement using the Documents section of the lodgement.

You can attach up to 50 documents to a lodgement.

The file size is limited to 5MB, and to pdf, word, excel and common image formats. If you have any specific requirements, please get in touch to discuss. If you require large file uploads use the `/uploadlargefile` endpoint to generate an upload url and PUT your file there [code examples](https://docs.aws.amazon.com/AmazonS3/latest/userguide/PresignedUrlUploadObject.html)

Note that TrustMark does not require all files that are needed as part of the supplier notification to Ofgem.

For PAS 2030:2017 we require the Declaration of Conformity and would like the guarantee.

For PAS 2035 there are additional documents that the Retrofit Coordinator must provide.
https://www.trustmark.org.uk/docs/default-source/pas-2035/pas-2035---trustmark-overview.pdf


### Status

The status refers to each lodgement. The initial status of a lodgement is New and will process to Complete when the Complete call is sent.

### Transitioning to Complete

Once everything has been attached to a lodgement a Complete call can be made. This will ensure that the lodgement is submitted to the data warehouse and a certificate is generated and sent. The lodgement will be assigned a certificate number and each measure will be assigned a Unique Measure Reference number.

Your account will also be debited for the lodgement, inline with the current pricing tier.

### ValidationFail

When attempting to post a lodgement the validator will check the values supplied and provide a Reasons array back containing each field that has failed and a reason why.

## Getting Started

To get started with this project you should clone locally and compile with .NET core 2.2.

Follow the Readme in the SampleApp to continue.

## API Endpoints

All API endpoints require an x-api-key Header value.

For the latest request, response structures and sample data please see the Postman script link above.

### POST /members/lodgement

Creates a lodgement record returns a Status and Receipt.

You will also need to supply an tm-api-key Header value.

https://api.sandbox.data-hub.org.uk/lodgement/members/lodgement

Example autocomplete success response:

```
{
    "status": "Complete",
    "receipt": {
        "lodgementId": "5f0c2fe2-f467-450a-8e6e-941eb6491f11",
        "certificateNumber": "5010",
        "licenceNumber": "1624715",
        "measures": [
            {
                "measureCategory": "Solid Wall Insulation",
                "measureType": "Solid wall - External Insulation",
                "umr": "f371231c3aed4260b27df1aaabb6ff01",
                "installerReferenceNumber": "NBH002"
            }
        ]
    }
}
```

Example success response without complete

```
{
    "status": "New",
    "receipt": {
        "lodgementId": "e7f9d0ec-fb73-4264-8d7a-4bb52d590cb9",
        "certificateNumber": null,
        "licenceNumber": "1624715",
        "measures": [
            {
                "measureCategory": "Solid Wall Insulation",
                "measureType": "Solid wall - External Insulation",
                "umr": "2cf978bf9c1f4ee5a9aa3e56cbe7eff0",
                "installerReferenceNumber": "NBH003"
            }
        ]
    }
}
```

### POST /members/uploadfile

Upload a file returns a UploadToken to attach to a Lodgement.

You will also need to supply an tm-api-key Header value.

https://api.sandbox.data-hub.org.uk/lodgement/members/uploadfile

Example success response

```

{
    "status": "Created",
    "receipt": {
        "uploadToken": "e131dee09c5045eb8cab93fd26ce55d0taa1969eba9e740a6bd18ebbf41088ff2"
    }
}

```

### PUT /members/lodgement/addmeasure

Adds a new measure to an open lodgement.

You will also need to supply an tm-api-key Header value.

https://api.sandbox.data-hub.org.uk/lodgement/members/lodgement/addmeasure

```
{
    "status": "Created",
    "measureId": "941948e3-7117-44ab-a64f-d6d1b485e0cd",
    "umr": "13nYp75xZa0",
    "measure": {
        "general": {
            "installedDate": "2020-03-18T00:00:00",
            "workTypeId": "DW-102",
            "handoverDate": "2020-04-01T00:00:00",
            "installerReferenceNumber": "TM009M008",
            "supplierReferenceNumber": "",
            "subInstallerName": "",
            "subInstallerTrustMarkLicenceNumber": "",
            "installerPasCertificateNumber": "",
            "certificateFreeTextDetails": "This will appear on your certificate"
        },
        "guarantee": {
            ...
        },
        "additional": {
            ...
        },
        "product": {
            ...
        },
        "obligation": {
            ...
        }
    }
}
```

### PUT /members/lodgement/complete

Completes a Lodgement is the status allows returns a Status and Receipt.

You will also need to supply an tm-api-key Header value.

https://api.sandbox.data-hub.org.uk/lodgement/members/lodgement/complete

```
{
    "status": "Complete",
    "receipt": {
        "lodgementId": "a14c66c9-0856-4fec-9bb4-64867d051c92",
        "status": "Complete",
        "creditStatus": "OK",
        "isPaid": true,
        "certificateNumber": "5210",
        "licenceNumber": "1624715",
        "measures": [
            {
                "measureCategory": "Other Insulation",
                "measureType": "Park home insulation - roof",
                "pasStandard": "PAS 2030:2019",
                "umr": "a1BK81oL4a4",
                "installerReferenceNumber": "TM009M008"
            }
        ],
        "documents": [
            {
                "type": "Intended outcomes",
                "id": "3973c522-9b48-418f-b5b7-bf890b6ad025"
            },
            {
                "type": "Retrofit design",
                "id": "43eb3065-2e77-492c-9022-c54eb5751b02"
            },
            {
                "type": "Assessment report",
                "id": "541a9f0c-b8f1-418d-a768-b70c51dd74ee"
            },
            {
                "type": "Handover documents for client",
                "id": "706dd4c5-940c-4165-8210-60780f120ae3"
            },
            {
                "type": "Claim of compliance PAS2030",
                "id": "a6dadc68-ead1-4fb2-a475-e13883f4c1a9"
            },
            {
                "type": "Claim of compliance PAS2035",
                "id": "d9ebb771-fd05-4862-a00e-c9c4959f01bd"
            },
            {
                "type": "Insurance guarantee",
                "id": "e1bb8e62-cf96-479a-8cd5-f263cecb83ac"
            }
        ],
        "cost": 9.6,
        "tmln": "1624715",
        "trustmarkBusinessId": "39d6924e-e6f0-4a8b-96f8-591a9dff3204",
        "lodgedByTrustmarkBusinessId": "39d6924e-e6f0-4a8b-96f8-591a9dff3204",
        "isThirdParty": false,
        "thirdPartyName": null
    },
    "message": null
}
```

### PUT /members/lodgement/attachresponse

This will be used where a retrofit participant has been invited to contribute to the PAS2035 lodgement and you are uploading on behalf of that participant. If you are lodging on behalf of a Retrofit Coordinator who is collating and uploading the documents themself, use POST /members/uploadfile

Attach a response to a Lodgement if the status allows returns a Status and Receipt.

You will also need to supply an tm-api-key Header value.

https://api.sandbox.data-hub.org.uk/lodgement/members/lodgement/attachresponse

TrustMark attach PAS 2035 response:
```
{
  "Status": "Created",
  "Message": null,
  "LodgementDocument": {
    "Id": "cbfa91cf-fbf1-445a-b4cc-d2bab424bdd1",
    "LodgementId": "b888a55a-80d5-4dc8-9b61-14478c4bdebb",
    "DocumentType": "Document",
    "Role": "Advisor",
    "RoleTMLN": "1454432"
  }
}
```

### DELETE /members/lodgement

Deletes a Lodgement if the status allows returns a Status.

You will also need to supply an tm-api-key Header value.

https://api.sandbox.data-hub.org.uk/lodgement/members/lodgement

### GET /taxonomies/countries

Returns a list of Countries that are valid for the Lodgement.Address.Country field.

https://api.sandbox.data-hub.org.uk/lodgement/taxonomies/countries

### GET /taxonomies/ecosuppliers

Returns a list of ECO Suppliers that are valid for the Measure.Obligation.EcoSupplierReference field.

https://api.sandbox.data-hub.org.uk/lodgement/taxonomies/ecosuppliers

### GET /taxonomies/energyefficiencycategories

Returns a list of Energy Efficiency Categories that are valid for the Measure.General.MeasureCategory field when the Work Type is 'Energy Efficiency'.

If you are supplying a value for Measure.General.WorkTypeId you do not need to supply this value.

https://api.sandbox.data-hub.org.uk/lodgement/taxonomies/energyefficiencycategories

### GET /taxonomies/epcratings

Returns a list of EPC Ratings that are valid for the Lodgement.PreEPC and Lodgement.PostEPC fields.

https://api.sandbox.data-hub.org.uk/lodgement/taxonomies/epcratings

### GET /taxonomies/guaranteetypes

Returns a list of Guarantee Types that are valid for the Measure.Guarantee.GuaranteeSelect field.

https://api.sandbox.data-hub.org.uk/lodgement/taxonomies/guaranteetypes

### GET /taxonomies/innovationmeasures

Returns a list of Innovation Measures and Products that are valid to describe a measure's product detail.

If you are supplying a value for Measure.Product.IsInnovationMeasure you will find the expected values here.

https://api.sandbox.data-hub.org.uk/lodgement/taxonomies/innovationmeasures

### GET /taxonomies/measuretypes

Returns a list of Measure Types that are valid to complete what work has been completed.

If you are supplying a value for Measure.General.WorkTypeId you will find the expected values here.

https://api.sandbox.data-hub.org.uk/lodgement/taxonomies/measuretypes

### GET /taxonomies/propertyages

Returns a list of Property Ages that are valid for the Lodgement.PropertyInformation.PropertyAge field - use the Band value of the property age.

https://api.sandbox.data-hub.org.uk/lodgement/taxonomies/propertyages

### GET /taxonomies/propertyconstructions

Returns a list of Property Ages that are valid for the Lodgement.PropertyInformation.PropertyConstruction field.

https://api.sandbox.data-hub.org.uk/lodgement/taxonomies/propertyconstructions

### GET /taxonomies/propertydetachments

Returns a list of Property Ages that are valid for the Lodgement.PropertyInformation.PropertyDetachment field.

https://api.sandbox.data-hub.org.uk/lodgement/taxonomies/propertydetachments

### GET /taxonomies/propertytypes

Returns a list of Property Ages that are valid for the Lodgement.PropertyInformation.PropertyType field.

https://api.sandbox.data-hub.org.uk/lodgement/taxonomies/propertytypes

### GET /taxonomies/servicecategories

Returns a list of Service Categories that are valid for the Measure.General.MeasureCategory field when the Work Type is 'Repair, Maintenance & Improvement'

If you are supplying a value for Measure.General.WorkTypeId you do not need to supply this value.

https://api.sandbox.data-hub.org.uk/lodgement/taxonomies/servicecategories

### GET /taxonomies/tenuretypes

Returns a list of Tenure Types that are valid for the Lodgement.Tenure.PremisesTenure field.

https://api.sandbox.data-hub.org.uk/lodgement/taxonomies/tenuretypes

### GET /taxonomies/worktypes

Returns a list of Work Types that are valid for the Measure.General.WorkType field.

If you are supplying a value for Measure.General.WorkTypeId you do not need to supply this value.

https://api.sandbox.data-hub.org.uk/lodgement/taxonomies/worktypes

## Third Party Lodgement

If you are a software company and wish to lodge on behalf of your users you do not need their API keys. You need the correct 'third party' permission applied to your account and you will then be able to lodge on behalf of the business by providing their TrustMark Licence Number (tmln).

To lodge ECO PAS2035 work overseen by a Retrofit Coordinator the “tmln” value must be that of the Retrofit Coordinator who has granted consent for 3rd party lodgement. 
Populate the ”pas2035Roles” and “pas2035RiskRating” and for each measure provide the tmln of the installer in the “subInstallerTrustMarkLicenceNumber”.

There are examples in the Postman script. Core principles, the lodgement will be made by the Retrofit Coordinator using their tmln. The measure will contain the tmln of the installer in the sub Installer field.

Note that some Retrofit Scheme Providers have determined that their registered Retrofit Coordinators should lodge through their platform – this can be identified by looking up the Retrofit coordinator on www.trustmark.org.uk and checking if their trade is listed as ‘Retrofit Coordinator PAS2035’.
 
## Membercheck API
 
This service provides the ability for an authorised party to validate that a TrustMark Registered Business is currently registered and for what trades.

### POST /membercheck
```
{
  "tmln": 1624311
}
```

Example successful response details business and trades they are registered for.
```
{
    "success": true,
    "trustMarkLicence": 1624311,
    "organisationRegisteredName": "TrustMark TM_005 Limited (Not For Public Use)",
    "branchName": "TrustMark TM_005 Limited (Not For Public Use)",
    "address1": "TrustMark (2005) Limited",
    "address2": "Basing View",
    "town": "Basingstoke",
    "postCode": "RG21 4EB",
    "trades": [
        {
            "description": "Heating Engineers",
            "registered": [
                {
                    "registeredFrom": "2019-01-01T00:00:00",
                    "registeredTo": "2022-08-24T00:00:00+00:00"
                }
            ],
            "trustMarkTradeCode": 29
        },
        {
            "description": "External Wall Insulation [B4 - 2017]",
            "registered": [
                {
                    "registeredFrom": "2019-01-01T00:00:00",
                    "registeredTo": "2022-08-24T00:00:00+00:00"
                }
            ],
            "trustMarkTradeCode": 82
        }
    ],
    "checkDate": null,
    "checkTradesForDate": []
}
```

Example response where business cannot be found or is not currently TrustMark registered.
```
{
    "success": false
}
```



