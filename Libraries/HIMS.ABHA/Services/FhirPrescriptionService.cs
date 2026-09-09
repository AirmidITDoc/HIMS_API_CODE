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
        private static Binary CreatePatient(Registration data)
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
                            Identifier=new Identifier[]{new() {
                            System="https://YARAGOHEALTHTECH/bundle",
                            Value="0cbad398-34ef-4ae2-ab17-d54302978863"
                            } },
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
                                },
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
                                        },Text="Prescription record"
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
                            Identifier=new Identifier[] {new(){ Type=new IdentifierType(){
                                Coding= new Class1[]{ new() {
                                System="http://terminology.hl7.org/CodeSystem/v2-0203",
                                Code="MR",
                                Display="Medical record number"
                                } } }  , System="https://healthid.abdm.gov.in", Value="sayali.16@sbx" } },
                            Name= new object[]{new { text="Mrs.********** "} },
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
                            Identifier=new Identifier[]{new() { Type=new IdentifierType(){
                                Coding= new Class1[]{ new() {
                                System="http://terminology.hl7.org/CodeSystem/v2-0203",
                                Code="MD",
                                Display="Medical License number"
                                } } } , System="https://doctor.abdm.gov.in", Value="4" } },
                            Name=new object[]{ new { text = "YARAGO_DOCTOR" } }
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
                            Identifier=new Identifier[]{ new() { Type=new IdentifierType(){
                                Coding= new Class1[]{ new() {
                                System="http://terminology.hl7.org/CodeSystem/v2-0203",
                                Code="PRN",
                                Display="Provider number"
                                } } } , System="https://facility.abdm.gov.in", Value="SBXID_010428" } },
                            Name=new object[]{new{ text="YARAGO HEALTHTECH PRIVATE LIMITED"} }
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
                             LastUpdated=(DateTime)DateTime.UtcNow, Profile=new string[]{ "https://nrces.in/ndhm/fhir/r4/StructureDefinition/MedicationRequest" }
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
                    },
                    new() {
                        FullUrl = "MedicationRequest/188cf1c7-8a40-43a6-8f76-8f0d5016ccba",
                        Resource = new Resource
                        {
                            ResourceType = "MedicationRequest",
                            Id = Guid.NewGuid().ToString(),
                            Meta=new Meta1(){
                            LastUpdated=(DateTime)DateTime.UtcNow, Profile=new string[]{ "https://nrces.in/ndhm/fhir/r4/StructureDefinition/MedicationRequest" }
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
                                        Display = "Calcium Carbonate 500mg(Calcium Tablet)"
                                    }
                                },
                                Text = "Calcium Carbonate 500mg(Calcium Tablet)"
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
                             new Subject(){ Display="MedicalCondition", Reference="Condition/cea26f38-3261-4481-addd-b16d15bcaad8" }
                            },
                            DosageInstruction = new[]
                            {
                                new Dosageinstruction
                                {
                                    Text = "Take 1 tablet once daily",
                                    AdditionalInstruction = new[]
                                    {
                                        new Route {
                                            Coding=new Class1[]{
                                            new(){ System="http://snomed.info/sct",Code="261665006",Display="Take after food with water"}
                                            },                                            Text = "Take after food with water" }
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
                                        Coding=new Class1[]{ new() { System= "http://snomed.info/sct",Code= "26643006", Display= "Oral route" } },
                                        Text = "Oral"
                                    },
                                    Method = new Route
                                    {
                                         Coding=new Class1[]{ new() { System= "http://snomed.info/sct",Code= "261665006", Display= "Swallow" } },
                                       Text = "Swallow"
                                    }
                                }
                            }
                        }
                    },
                    new() {
                        FullUrl = "Condition/ca55e363-422f-4167-9bad-4ab77a6b026e",
                        Resource = new Resource
                        {
                            ResourceType = "Condition",
                            Id = Guid.NewGuid().ToString(),
                            Meta=new Meta1(){
                            LastUpdated=(DateTime)DateTime.UtcNow, Profile=new string[]{ "https://nrces.in/ndhm/fhir/r4/StructureDefinition/Condition" }
                            },
                            Code=new Code(){
                             Coding = new[]
                                {
                                    new Coding2
                                    {
                                        System = "http://snomed.info/sct",
                                        Display = "Nil"
                                    }
                                },Text="Nil"
                            },
                            Subject = new Subject
                            {
                                Reference = "Patient/a9f09417-eb99-49a1-a477-1fd6e6efb1c5",
                                Display = "Mrs.********** "
                            },
                            RecordedDate=DateTime.UtcNow,
                        }
                    },
                    new() {
                        FullUrl = "Condition/7edd5958-6419-4689-ac2c-305bbf4cb08b",
                        Resource = new Resource
                        {
                            ResourceType = "Condition",
                            Id = Guid.NewGuid().ToString(),
                            Meta=new Meta1(){
                            LastUpdated=(DateTime)DateTime.UtcNow, Profile=new string[]{ "https://nrces.in/ndhm/fhir/r4/StructureDefinition/Condition" }
                            },
                            Code=new Code(){
                             Coding = new[]
                                {
                                    new Coding2
                                    {
                                        System = "http://snomed.info/sct",
                                        Display = "Nil"
                                    }
                                },Text="Nil"
                            },
                            Subject = new Subject
                            {
                                Reference = "Patient/a9f09417-eb99-49a1-a477-1fd6e6efb1c5",
                                Display = "Mrs.********** "
                            },
                            RecordedDate=DateTime.UtcNow,
                        }
                    },
                    new() {
                        FullUrl = "Condition/cea26f38-3261-4481-addd-b16d15bcaad8",
                        Resource = new Resource
                        {
                            ResourceType = "Condition",
                            Id = Guid.NewGuid().ToString(),
                            Meta=new Meta1(){
                            LastUpdated=(DateTime)DateTime.UtcNow, Profile=new string[]{ "https://nrces.in/ndhm/fhir/r4/StructureDefinition/Condition" }
                            },
                            Code=new Code(){
                             Coding = new[]
                                {
                                    new Coding2
                                    {
                                        System = "http://snomed.info/sct",
                                        Display = "Nil"
                                    }
                                },Text="Nil"
                            },
                            Subject = new Subject
                            {
                                Reference = "Patient/a9f09417-eb99-49a1-a477-1fd6e6efb1c5",
                                Display = "Mrs.********** "
                            },
                            RecordedDate=DateTime.UtcNow,
                        }
                    },
                    new() {
                        FullUrl = "Binary/4319512a-bd37-4d0f-82a9-6a2463692423",
                        Resource = new Resource
                        {
                            ResourceType = "Binary",
                            Id = Guid.NewGuid().ToString(),
                            Meta=new Meta1(){
                            VersionId="1", LastUpdated=(DateTime)DateTime.UtcNow, Profile=new string[]{ "https://nrces.in/ndhm/fhir/r4/StructureDefinition/Binary" }
                            },
                            ContentType="application/pdf",
                            Data="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b250L0Jhc2VGb250L0hlbHZldGljYS1Cb2xkL0VuY29kaW5nL1dpbkFuc2lFbmNvZGluZz4+CmVuZG9iago2IDAgb2JqCjw8L0tpZHNbMSAwIFJdL1R5cGUvUGFnZXMvQ291bnQgMS9JVFhUKDIuMS43KT4+CmVuZG9iago4IDAgb2JqCjw8L05hbWVzWyhKUl9QQUdFX0FOQ0hPUl8wXzEpIDcgMCBSXT4+CmVuZG9iago5IDAgb2JqCjw8L0Rlc3RzIDggMCBSPj4KZW5kb2JqCjEwIDAgb2JqCjw8L05hbWVzIDkgMCBSL1R5cGUvQ2F0YWxvZy9QYWdlcyA2IDAgUi9WaWV3ZXJQcmVmZXJlbmNlczw8L1ByaW50U2NhbGluZy9BcHBEZWZhdWx0Pj4+PgplbmRvYmoKMTEgMCBvYmoKPDwvTW9kRGF0ZShEOjIwMjYwOTAxMTE1OTIzKzA1JzMwJykvQ3JlYXRvcihKYXNwZXJSZXBvcnRzIExpYnJhcnkgdmVyc2lvbiA2LjIwLjAtMmJjN2FiNjFjNTZmNDU5ZTgxNzZlYjA1Yzc3MDVlMTQ1Y2Q0MDBhZCkvQ3JlYXRpb25EYXRlKEQ6MjAyNjA5MDExMTU5MjMrMDUnMzAnKS9Qcm9kdWNlcihpVGV4dCAyLjEuNyBieSAxVDNYVCk+PgplbmRvYmoKeHJlZgowIDEyCjAwMDAwMDAwMDAgNjU1MzUgZiAKMDAwMDAwNzY3MyAwMDAwMCBuIAowMDAwMDA3OTgxIDAwMDAwIG4gCjAwMDAwMDAwMTUgMDAwMDAgbiAKMDAwMDAwODA2OSAwMDAwMCBuIAowMDAwMDA2NzQ4IDAwMDAwIG4gCjAwMDAwMDgxNjIgMDAwMDAgbiAKMDAwMDAwNzk0NiAwMDAwMCBuIAowMDAwMDA4MjI1IDAwMDAwIG4gCjAwMDAwMDgyNzkgMDAwMDAgbiAKMDAwMDAwODMxMSAwMDAwMCBuIAowMDAwMDA4NDE1IDAwMDAwIG4gCnRyYWlsZXIKPDwvSW5mbyAxMSAwIFIvSUQgWzw1Y2E4OTJiZWQyNWI2MzI5NGIyNTEyMTkxNDhlODcxYz48NTI3NzJhYjdlNWNkMmZjYWFmNTZkOGUxNGQwMDZkZTM+XS9Sb290IDEwIDAgUi9TaXplIDEyPj4Kc3RhcnR4cmVmCjg2MjUKJSVFT0YK"
                        }
                    }
               }
            };
        }
    }
}
