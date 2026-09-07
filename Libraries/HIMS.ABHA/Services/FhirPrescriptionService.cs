using HIMS.ABHA.Interface;
using HIMS.ABHA.Models.M2.PrescriptionDataTransfer;
using HIMS.Data;
using HIMS.Data.Models;

namespace HIMS.ABHA.Services
{
    public class FhirPrescriptionService : IFhirPrescriptionService
    {

        private readonly HIMSDbContext _context;

        public FhirPrescriptionService(HIMSDbContext context)
        {
            _context = context;
        }

        public async Task<DocumentBundle> CreatePrescriptionBundle(string registrationId)
        {
            // Get data from database
            var patient = await GetPatient(registrationId);

            //var doctor = await GetDoctor(registrationId);

            //var registration = await GetRegistration(registrationId);

            //var prescription = await GetPrescription(registrationId);

            //var medicines = await GetMedicines(
            //    prescription.Id);

            // Create FHIR resources
            var patientResource = CreatePatient(patient);

            //var practitionerResource =CreatePractitioner(doctor);

            //var organizationResource = CreateOrganization();

            //var encounterResource =CreateEncounter(
            //        registration,
            //        patientResource);

            //var medicationResources = CreateMedications(
            //        medicines,
            //        patientResource,
            //        practitionerResource,
            //        prescription);

            //var conditionResources = CreateConditions(
            //        registrationId,
            //        patientResource);

            //var compositionResource = CreateComposition(
            //        patientResource,
            //        practitionerResource,
            //        encounterResource,
            //        medicationResources,
            //        prescription);

            // Create Bundle
            var bundle = CreateBundle(
                patientResource
                //compositionResource,
                //practitionerResource,
                //organizationResource,
                //encounterResource,
                //medicationResources,
                //conditionResources
                );

            return bundle;
        }

        private DocumentBundle CreateBundle(
    //Composition composition,
    Patient patient
    //Practitioner practitioner,
    //Organization organization,
    //Encounter encounter,
    //List<MedicationRequest> medications,
    //List<Condition> conditions
            )
        {
            var bundle = new DocumentBundle
            {
                ResourceType = "Bundle",

                Id = Guid.NewGuid().ToString(),

                Type = "document",

                Timestamp = DateTime.UtcNow,

                Entry = new List<BundleEntry>()
            };

            //// Composition
            //bundle.Entry.Add(new BundleEntry
            //{
            //    FullUrl =
            //        $"Composition/{composition.Id}",

            //    Resource = composition
            //});

            // Patient
            bundle.Entry.Add(new BundleEntry
            {
                FullUrl =
                    $"Patient/{patient.Id}",

                Resource = patient
            });

            //// Practitioner
            //bundle.Entry.Add(new BundleEntry
            //{
            //    FullUrl =
            //        $"Practitioner/{practitioner.Id}",

            //    Resource = practitioner
            //});

            //// Organization
            //bundle.Entry.Add(new BundleEntry
            //{
            //    FullUrl =
            //        $"Organization/{organization.Id}",

            //    Resource = organization
            //});

            //// Encounter
            //bundle.Entry.Add(new BundleEntry
            //{
            //    FullUrl =
            //        $"Encounter/{encounter.Id}",

            //    Resource = encounter
            //});

            //// Medicines
            //foreach (var medication in medications)
            //{
            //    bundle.Entry.Add(new BundleEntry
            //    {
            //        FullUrl =
            //            $"MedicationRequest/{medication.Id}",

            //        Resource = medication
            //    });
            //}

            //// Conditions
            //foreach (var condition in conditions)
            //{
            //    bundle.Entry.Add(new BundleEntry
            //    {
            //        FullUrl =
            //            $"Condition/{condition.Id}",

            //        Resource = condition
            //    });
            //}

            return bundle;
        }

        private async Task<Registration> GetPatient(string registrationId)
        {
            if (!long.TryParse(registrationId, out long regId))
                return null;

            return await _context.Registrations.FirstOrDefaultAsync(x => x.RegId == regId);
        }

    //    private async Task<DoctorEntity> GetDoctor(
    //string registrationId)
    //    {
    //        return await _context.T_Doctor
    //            .FirstOrDefaultAsync(x =>
    //                x.RegistrationId == registrationId);
    //    }

    //    private async Task<PrescriptionHeader> GetPrescription(
    //string registrationId)
    //    {
    //        return await _context.T_PrescriptionHeader
    //            .FirstOrDefaultAsync(x =>
    //                x.RegistrationId == registrationId);
    //    }
    //    private async Task<List<PrescriptionDetail>> GetMedicines(
    //int prescriptionId)
    //    {
    //        return await _context.T_PrescriptionDetails
    //            .Where(x => x.PrescriptionId == prescriptionId)
    //            .ToListAsync();
    //    }


        //==================
        private Patient CreatePatient(Registration data)
        {
            return new Patient
            {
                ResourceType = "Patient",

                Id = Guid.NewGuid().ToString(),

                Identifier = new List<Identifier>
        {
            new Identifier
            {
                System = "https://healthid.abdm.gov.in",
                Value = data.FirstName
            }
        },

                Name = new List<HumanName>
        {
            new HumanName
            {
                Text = data.FirstName
            }
        },

                Gender = data.FirstName, // Assuming Gender is stored in FirstName

                BirthDate = data.DateofBirth?.ToString("yyyy-MM-dd")
            };
        }

    //    private Practitioner CreatePractitioner(
    //DoctorEntity data)
    //    {
    //        return new Practitioner
    //        {
    //            ResourceType = "Practitioner",

    //            Id = data.Id.ToString(),

    //            Identifier = new List<Identifier>
    //    {
    //        new Identifier
    //        {
    //            System = "https://doctor.abdm.gov.in",
    //            Value = data.Id.ToString()
    //        }
    //    },

    //            Name = new List<HumanName>
    //    {
    //        new HumanName
    //        {
    //            Text = data.DoctorName
    //        }
    //    }
    //        };
    //    }

    //    private List<MedicationRequest> CreateMedications(List<PrescriptionDetail> medicines,
    //Patient patient,
    //Practitioner practitioner,
    //PrescriptionHeader prescription)
    //    {
    //        var result = new List<MedicationRequest>();

    //        foreach (var medicine in medicines)
    //        {
    //            var medication = new MedicationRequest
    //            {
    //                ResourceType = "MedicationRequest",

    //                Id = Guid.NewGuid().ToString(),

    //                Status = "active",

    //                Intent = "order",

    //                MedicationCodeableConcept =
    //                    new CodeableConcept
    //                    {
    //                        Coding = new List<Coding>
    //                        {
    //                    new Coding
    //                    {
    //                        System =
    //                            "http://snomed.info/sct",

    //                        Code =
    //                            medicine.SnomedCode,

    //                        Display =
    //                            medicine.MedicineName
    //                    }
    //                        },

    //                        Text = medicine.MedicineName
    //                    },

    //                Subject = new Reference
    //                {
    //                    ReferenceValue =
    //                        $"Patient/{patient.Id}",

    //                    Display =
    //                        patient.Name.FirstOrDefault()?.Text
    //                },

    //                AuthoredOn =
    //                    prescription.PrescriptionDate
    //                    .ToString("yyyy-MM-ddTHH:mm:ssZ"),

    //                Requester = new Reference
    //                {
    //                    ReferenceValue =
    //                        $"Practitioner/{practitioner.Id}"
    //                },

    //                DosageInstruction =
    //                    new List<DosageInstruction>
    //                    {
    //                new DosageInstruction
    //                {
    //                    Text =
    //                        medicine.Dosage
    //                }
    //                    }
    //            };

    //            result.Add(medication);
    //        }

    //        return result;
    //    }
    }
}
