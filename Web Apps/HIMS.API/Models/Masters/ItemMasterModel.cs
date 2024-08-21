using FluentValidation;

namespace HIMS.API.Models.Masters
    {
        public class ItemMasterModel
        {

            public long ItemId { get; set; }
            public string? ItemShortName { get; set; }
            public string? ItemName { get; set; }
            public bool? Isdeleted { get; set; }
            public long? Addedby { get; set; }
            public long? UpDatedBy { get; set; }

            public String? IsUpdatedBy { get; set; }
                public String? CreatedDate { get; set; }
              public String? ItemTime { get; set; }

    }

        public class ItemMasterModelValidator : AbstractValidator<ItemMasterModel>
        {
            public ItemMasterModelValidator()
            {
                RuleFor(x => x.ItemId).NotNull().NotEmpty().WithMessage("ItemId is required");
                RuleFor(x => x.ItemShortName).NotNull().NotEmpty().WithMessage("ItemShortName is required");
                RuleFor(x => x.ItemName).NotNull().NotEmpty().WithMessage("ItemName is required");

            }
            public class MAssignItemToStore
            {
                public long AssignId { get; set; }
                public long? StoreId { get; set; }
                public long? ItemId { get; set; }
            }
            public class MAssignItemToStoreValidator : AbstractValidator<MAssignItemToStore>
            {
                public MAssignItemToStoreValidator()
                {
                    RuleFor(x => x.AssignId).NotNull().NotEmpty().WithMessage("AssignId is required");
                    RuleFor(x => x.StoreId).NotNull().NotEmpty().WithMessage("StoreId is required");
                    RuleFor(x => x.ItemId).NotNull().NotEmpty().WithMessage("ItemId is required");
                }
            }
        }
    }



