using HIMS.ABHA.Interface;
using HIMS.ABHA.Models.M2;
using HIMS.Data.Models;
using LinqToDB;

namespace HIMS.ABHA.Services
{
    public class FhirPrescriptionService : IFhirPrescriptionService
    {

        private readonly HIMSDbContext _context;

        public FhirPrescriptionService(HIMSDbContext context)
        {
            _context = context;
        }

        public async Task<Binary> CreatePrescriptionBundle(string registrationId)
        {
            // Get data from database
            var patient = await GetPatient(registrationId);

            var bundle = CreatePatient(patient);

            return bundle;
        }

        private async Task<Registration> GetPatient(string registrationId)
        {
            if (!long.TryParse(registrationId, out long regId))
                return null;

            return await _context.Registrations.FirstOrDefaultAsync(x => x.RegId == regId);
        }
        //==================
        private Binary CreatePatient(Registration data)
        {
            return new Binary
            {
                ResourceType = "Bundle",
                Id = Guid.NewGuid().ToString(),

                Meta = new Meta
                {
                    VersionId = "1",
                    LastUpdated = DateTime.UtcNow,
                    Profile = new[]
                   {
                        "https://nrces.in/ndhm/fhir/r4/StructureDefinition/DocumentBundle"
                    },
                    Security = new[]
                   {
                        new Security
                        {
                            System = "http://terminology.hl7.org/CodeSystem/v3-Confidentiality",
                            Code = "V",
                            Display = "very restricted"
                        }
                    }
                },

                Identifier = new Identifier
                {
                    System = "https://YARAGOHEALTHTECH/bundle",
                    Value = "Prescription-YGO1001114-105-153-0-722605"
                },

                Type = "document",
                Timestamp = DateTime.UtcNow,

                Entry = new Entry[]
               {
                    new() {
                        FullUrl = "Composition/3c4011d8-839e-4115-b9b4-cf2cac95571e",
                        Resource = new Resource
                        {
                            ResourceType = "Composition",
                            Id =Guid.NewGuid().ToString(),
                            Meta = new Meta1
                            {
                                VersionId = "1",
                                LastUpdated = DateTime.UtcNow,
                                Profile = new[]
                                {
                                    "https://nrces.in/ndhm/fhir/r4/StructureDefinition/PrescriptionRecord"
                                }
                            },
                            Identifier=new{
                            System="https://YARAGOHEALTHTECH/bundle",
                            Value="0cbad398-34ef-4ae2-ab17-d54302978863"
                            },
                            Status = "final",
                            Type = new Models.M2.Type
                            {
                                Coding = new[]
                                {
                                    new Security
                                    {
                                        System = "http://snomed.info/sct",
                                        Code = "440545006  snomed code",
                                        Display = "Prescription record"
                                    }
                                }
                            },
                            Subject = new Subject
                            {
                                Reference = "Patient/a9f09417-eb99-49a1-a477-1fd6e6efb1c5 Reference to the Patient resource Their FHIR Patient ID or UID",
                                Display = "Mrs.********** "
                            },
                            Encounter = new Subject
                            {
                                Reference = "Encounter/93d2a7f3-c743-45e9-be7d-8759ac3fd37a  Reference to the Encounter/visit Their FHIR Encounter ID",
                                Display="ambulatory"
                            },
                            Date = DateTime.UtcNow,
                            Author = new[]
                            {
                                new Subject
                                {
                                    Reference = "Practitioner/4  Reference to the doctor/practitioner Their FHIR Practitioner ID",
                                    Display = "YARAGO_DOCTOR"
                                }
                            },
                            Title = "Prescription record",
                            Custodian = new Custodian
                            {
                                Reference = "Organisation/e7eb8918-02d0-4d45-b2ec-259c9d217837  Reference to the hospital/facility organization Their FHIR Organization ID"
                            },
                            Section = new[]
                            {
                                new Section
                                {
                                    Title = "Medications",
                                    Code = new Route
                                    {
                                        Coding = new[]
                                        {
                                            new Class1
                                            {
                                                System = "http://snomed.info/sct",
                                                Code = "440545006 snomed code",
                                                Display = "Prescription record"
                                            }
                                        }
                                    },
                                    Entry = new[]
                                    {
                                        new SectionEntry {
                                            Reference = "MedicationRequest/96626329-eab3-482c-895b-177c2baad4fe   Reference to a MedicationRequest Their MedicationRequest ID",
                                        Type="MedicationRequest"
                                        },
                                        new SectionEntry {
                                            Reference = "MedicationRequest/253479fd-b034-4b7a-b33a-9cad31686150   Their MedicationRequest ID",
                                        Type="MedicationRequest"
                                        },
                                        new SectionEntry {
                                            Reference = "MedicationRequest/188cf1c7-8a40-43a6-8f76-8f0d5016ccba   Their MedicationRequest ID",
                                        Type="MedicationRequest"
                                        },
                                        new SectionEntry {
                                            Reference = "Binary/4319512a-bd37-4d0f-82a9-6a2463692423   Reference to attached document/PDF Their Binary ID",
                                        Type="Binary"
                                        }
                                    }
                                }
                            }
                        }
                    },
                    new() {
                        FullUrl = "Patient/a9f09417-eb99-49a1-a477-1fd6e6efb1c5",
                        Resource = new Resource
                        {
                            ResourceType = "Patient",
                            Id = Guid.NewGuid().ToString(),
                            Meta=new Meta1(){
                                 VersionId="1", LastUpdated=DateTime.UtcNow, Profile=new string[]{ "https://nrces.in/ndhm/fhir/r4/StructureDefinition/Patient" }
                            },
                            Identifier=new Identifier { Type=new IdentifierType(){
                                Coding= new Class1[]{ new() {
                                System="http://terminology.hl7.org/CodeSystem/v2-0203",
                                Code="MR",
                                Display="Medical record number"
                                } } } , System="https://healthid.abdm.gov.in", Value="sayali.16@sbx" },
                            Name=new{ text="Mrs.********** "},
                            Gender = "female",
                            BirthDate = "1993-11-30"
                        }
                    },
                     new() {
                       FullUrl = "Practitioner/4",
                        Resource = new Resource
                        {
                            ResourceType = "Practitioner",
                            Id = "4",
                            Meta=new Meta1(){
                            VersionId="1", LastUpdated=(DateTime)DateTime.UtcNow, Profile=new string[]{ "https://nrces.in/ndhm/fhir/r4/StructureDefinition/Practitioner" }
                            },
                            Identifier=new Identifier { Type=new IdentifierType(){
                                Coding= new Class1[]{ new() {
                                System="http://terminology.hl7.org/CodeSystem/v2-0203",
                                Code="MD",
                                Display="Medical License number"
                                } } } , System="https://doctor.abdm.gov.in", Value="4" },
                            Name=new{ text="YARAGO_DOCTOR"}
                        }
                    },
                      new() {
                       FullUrl = "Organisation/e7eb8918-02d0-4d45-b2ec-259c9d217837",
                        Resource = new Resource
                        {
                            ResourceType = "Organization",
                            Id = Guid.NewGuid().ToString(),
                            Meta=new Meta1(){
                            VersionId="1", LastUpdated=(DateTime)DateTime.UtcNow, Profile=new string[]{ "https://nrces.in/ndhm/fhir/r4/StructureDefinition/Organization" }
                            },
                            Identifier=new Identifier { Type=new IdentifierType(){
                                Coding= new Class1[]{ new() {
                                System="http://terminology.hl7.org/CodeSystem/v2-0203",
                                Code="PRN",
                                Display="Provider number"
                                } } } , System="https://facility.abdm.gov.in", Value="SBXID_010428" },
                            Name=new{ text="YARAGO HEALTHTECH PRIVATE LIMITED"}
                        }
                    },
                      new() {
                       FullUrl = "Encounter/93d2a7f3-c743-45e9-be7d-8759ac3fd37a",
                        Resource = new Resource
                        {
                            ResourceType = "Encounter",
                            Id = Guid.NewGuid().ToString(),
                            Status="finished",
                            _Class=new Class1(){
                             System="http://terminology.hl7.org/CodeSystem/v3-ActCode",
                             Code="AMB",
                             Display="ambulatory"
                            },
                            Subject=new Subject(){
                             Reference="Patient/a9f09417-eb99-49a1-a477-1fd6e6efb1c5"
                            },
                            Period=new Period(){
                             Start=DateTime.Now.AddDays(-2),
                             End=DateTime.Now.AddDays(2)
                            }
                        }
                    },
                      new() {
                       FullUrl = "MedicationRequest/96626329-eab3-482c-895b-177c2baad4fe",
                        Resource = new Resource
                        {
                            ResourceType = "MedicationRequest",
                            Id = Guid.NewGuid().ToString(),
                             Meta=new Meta1(){
                            LastUpdated=(DateTime)DateTime.UtcNow, Profile=new string[]{ "https://nrces.in/ndhm/fhir/r4/StructureDefinition/MedicationRequest" }
                            },
                            Status="active",
                            Intent="order",
                            MedicationCodeableConcept=new Medicationcodeableconcept(){
                             Coding=new Class1[]{ new() {
                              System="http://snomed.info/sct",
                              Code="261665006",
                              Display="Loteprednol Etabonate 0.5%(Fresh Eye Solution)"
                             } },Text="Loteprednol Etabonate 0.5%(Fresh Eye Solution)"
                            },
                            Subject=new Subject(){
                             Reference="Patient/a9f09417-eb99-49a1-a477-1fd6e6efb1c5",
                             Display="Mrs.********** "
                            },
                            AuthoredOn=DateTime.UtcNow,
                            Requester=new Subject(){
                             Reference="Practitioner/4",
                             Display="YARAGO_DOCTOR"
                            },
                            ReasonCode=new Code[]{ new() {
                             Coding=new Coding2[]{
                             new(){ System="http://snomed.info/sct",Display="Nil"  }
                             },Text="Nil"
                            } },
                            ReasonReference=new Subject[]{
                            new(){ Reference="Condition/ca55e363-422f-4167-9bad-4ab77a6b026e  Reference to a Condition/medical condition .Their Condition ID",
                            Display="MedicalCondition"}
                            },
                            DosageInstruction=new Dosageinstruction[]{
                            new(){ Text="(Right Eye)Instill 1 drop once daily",
                            AdditionalInstruction=new Route[]{
                            new(){ Coding=new Class1[]{
                            new(){  System="http://snomed.info/sct",Code="261665006",Display="Instill 1 drop in right/left eye"}
                            },Text="Instill 1 drop in right/left eye" }
                            },
                                Timing=new Timing(){
                                 Repeat=new Repeat(){
                                  Duration=0, DurationUnit="d", Frequency=1, Period=1, PeriodUnit="d"
                                 }
                                },
                                Route=new Route(){
                                Coding=new Class1[]{
                                new(){  System="http://snomed.info/sct",Code="54485002",Display="Ophthalmic route"}
                                },Text="Ophthalmic"
                                },
                                Method=new Route(){
                                Coding=new Class1[]{
                                new(){  System="http://snomed.info/sct",Code="261665006",Display="Dropper"}
                                },Text="Dropper"
                                }
                            }
                            }
                        }
                    },
                    new() {
                        FullUrl = "MedicationRequest/253479fd-b034-4b7a-b33a-9cad31686150",
                        Resource = new Resource
                        {
                            ResourceType = "MedicationRequest",
                            Id = Guid.NewGuid().ToString(),
                            Meta=new Meta1(){
                            VersionId="1", LastUpdated=(DateTime)DateTime.UtcNow, Profile=new string[]{ "https://nrces.in/ndhm/fhir/r4/StructureDefinition/MedicationRequest" }
                            },
                            Status = "active",
                            Intent = "order",
                            MedicationCodeableConcept = new Medicationcodeableconcept
                            {
                                Coding = new[]
                                {
                                    new Class1
                                    {
                                        System = "http://snomed.info/sct",
                                        Code = "261665006",
                                        Display = "Mupirocin 2%(Mupirocin Ointment)"
                                    }
                                },
                                Text = "Mupirocin 2%(Mupirocin Ointment)"
                            },
                            Subject = new Subject
                            {
                                Reference = "Patient/a9f09417-eb99-49a1-a477-1fd6e6efb1c5",
                                Display = "Mrs.********** "
                            },
                            AuthoredOn = DateTime.UtcNow,
                            Requester = new Subject
                            {
                                Reference = "Practitioner/4",
                                Display = "YARAGO_DOCTOR"
                            },
                            ReasonCode = new[]
                            {
                                new Code
                                { Coding=new Coding2[]{
                                 new(){ Display="Nil",System="http://snomed.info/sct" }
                                },
                                    Text = "Nil"
                                }
                            },
                            ReasonReference=new[]
                            {
                             new Subject(){ Display="MedicalCondition", Reference="Condition/7edd5958-6419-4689-ac2c-305bbf4cb08b" }
                            },
                            DosageInstruction = new[]
                            {
                                new Dosageinstruction
                                {
                                    Text = "Apply once daily",
                                    AdditionalInstruction = new[]
                                    {
                                        new Route {
                                            Coding=new Class1[]{
                                            new(){ System="http://snomed.info/sct",Code="261665006",Display="Apply thin layer to conjunctival sac"}
                                            },                                            Text = "Apply thin layer to conjunctival sac" }
                                    },
                                    Timing = new Timing
                                    {
                                        Repeat = new Repeat
                                        {
                                            Duration = 0,
                                            DurationUnit = "d",
                                            Frequency = 1,
                                            Period = 1,
                                            PeriodUnit = "d"
                                        }
                                    },
                                    Route = new Route
                                    {
                                        Coding=new Class1[]{ new() { System= "http://snomed.info/sct",Code= "54485002",Display= "Ophthalmic route" } },
                                        Text = "Ophthalmic"
                                    },
                                    Method = new Route
                                    {
                                         Coding=new Class1[]{ new() { System= "http://snomed.info/sct",Code= "261665006", Display= "Apply to eye / Fingertip" } },
                                       Text = "Apply to eye / Fingertip"
                                    }
                                }
                            }
                        }
                    }
               }
            };
        }
    }
}
