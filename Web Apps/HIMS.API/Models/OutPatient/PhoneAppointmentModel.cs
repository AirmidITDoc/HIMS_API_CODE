using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HIMS.API.Models.OutPatient
{
    public class PhoneAppointmentModel
    {
        public long PhoneAppId { get; set; }
        public string? AppDate { get; set; }
        public string? AppTime { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? MobileNo { get; set; }
        public string? PhAppDate { get; set; }
        public string? PhAppTime { get; set; }
        public long? DepartmentId { get; set; }
        public long? DoctorId { get; set; }
        public string? RegNo { get; set; }


    }
        public class PhoneAppointmentModelValidator : AbstractValidator<PhoneAppointmentModel>
        {
            public PhoneAppointmentModelValidator()
            {
                RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("FirstName is required");
                RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("LastName is required");
                RuleFor(x => x.MobileNo).NotNull().NotEmpty().WithMessage("Mobile is required");
                RuleFor(x => x.DepartmentId).NotNull().NotEmpty().WithMessage("Department is required");
                RuleFor(x => x.DoctorId).NotNull().NotEmpty().WithMessage("Doctor is required");
            }
        }
    }

