using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HIMS.Core.Infrastructure
{
    public enum EnumSalesOrderStatus
    {
        [Description("Draft")]
        Draft = 1,

        [Description("Pending Approval")]
        Pending = 2,

        [Description("Pending Operations")]
        Pending_Operations = 3,

        [Description("Pending Technical")]
        Pending_Technical = 4,

        [Description("Pending Machine Verification")]
        Pending_Machine_Verification = 5,

        [Description("Pending Delivery")]
        Pending_Delivery = 6,

        [Description("Delivered")]
        Delivered = 7,

        [Description("Completed")]
        Completed = 8,

        [Description("Void")]
        Void = 9,

        [Description("Exchanged")]
        Exchanged = 10,

        [Description("Changed Company Name")]
        Changed_Company_Name = 11,

        [Description("Relocated")]
        Relocated = 12,

        [Description("Pending Withdraw Approval")]
        Pending_Withdraw_Approval = 13,

        [Description("Pending Removal")]
        Pending_Removal = 14,

        [Description("Removed Machine")]
        Removed = 15,

        [Description("Withdrawn")]
        Withdrawn = 16

    } // note : when add new status type, add record in Sales_OrderStatus db. enum id and  Sales_OrderStatus.Statusid MUST BE same
    public enum EnumSalesMovementType
    {
        New_Sales = 1, Exchange = 2, Change_Company_Name = 3, Relocation = 4
    } // note : when add new status type, add record in Sales_OrderMovement db. enum id and  Sales_OrderMovement.Movementid MUST BE same
    public enum EnumActionType
    {
        WITHDRAWAL = 1, SHIFTING = 2, OTHERS = 3
    }
    public enum EnumSalesCommSubsidy
    {
        With_CommSubsidy = 1, Without_CommSubsidy = 2
    }
    public enum EnumSalesRental
    {
        With_Rental = 1, Without_Rental = 2
    }
    public enum EnumSalesDeposit
    {
        With_Deposit = 1, Without_Deposit = 2
    }
    public enum EnumSalesUtilities
    {
        With_Utilities = 1, Without_Utilities = 2
    }
    public enum EnumSalesCBD
    {
        [Description("In CBD")]
        In_CBD = 1,
        [Description("Out of CBD")]
        Out_of_CBD = 2
    }
    public enum EnumSalesHHTMeterType
    {
        [Display(Name = "C")] C = 1, [Display(Name = "Q")] Q = 2, [Display(Name = "-")] Dash = 3
    }
    public enum EnumCommissionType
    {
        [Display(Name = "$")]
        Amount = 1,
        [Display(Name = "%")]
        Percentage = 2
    }
    public enum EnumEventType
    {
        SM_Approval = 1, OM_Approval = 2, TM_Approval = 3, TA_Assign_Tech = 4, TA_Approval = 5, SE_Verified = 6, Logistics_Approval = 7
    }
    public enum EnumSaleActionType
    {
        Approved = 1, Rejected = 2, Other = 3
    }
    public enum EnumSalesStatus
    {
        Draft = 1, Pending = 2, Approved = 3
    }
    public enum EnumOperationStatus
    {
        Pending = 1, Approved = 2
    }
    public enum EnumTechnicalStatus
    {
        Pending = 1, Approved = 2, TA_Assigned = 3, TA_Assigned_To_Tech = 4
    }
    public enum EnumLogisticStatus
    {
        Pending = 1, Approved = 2
    }
    public enum EnumWorkFlowRole
    {
        Admin = 1,
        SA = 2,
        SM = 3,
        SE = 4,
        OA = 5,
        OE = 6,
        TA = 7,
        TM = 8,
        WT = 9,
        FT = 10,
        FA = 11,
        CRA = 12,
        OM = 13,
        LM = 14,
        Technician = 15,
        OutTechnician = 16,
        LogisticExecutive = 17
    }
    public enum EnumReasonType
    {
        Amend = 1, Void = 2, Exchange = 3, Change_Company_Name = 4, Relocation = 5, Withdrawal = 6
    }// note : when add new reason type, add record in Sales_ReasonType db. enum id and  Sales_ReasonType.Typeid MUST BE same
    public enum EnumShiftType
    {
        AM = 1, PM = 2
    }
    public enum EnumRoles
    {
        Admin = 1,
        SA = 2,
        SM = 3,
        SE = 4,
        OA = 5,
        AOM = 6,
        TA = 7,
        TM = 8,
        WT = 9,
        FT = 10,
        FA = 11,
        CRA = 12,
        OM = 13,
        LM = 14,
        Technician = 15,
        Serviceman = 16,
        Supervisor = 17,
        OutTechnician = 18,
        LogisticAssistant = 19,
        Ops_Admin = 20
    }
    public enum EnumNotificationStatus
    {
        Pending = 1, Send = 2
    }
    public enum enmSalesApprovalDepartmentType
    {
        [Description("Sales")]
        Sales = 1,

        [Description("Operations")]
        Operations = 2,

        [Description("Technical")]
        Technical = 3,

        [Description("Logistics")]
        Logistics = 4
    }
    public enum enmSalesApprovalOperationType
    {
        [Description("N.A")]
        NA = 0,

        [Description("SM - Review Sales Order")]
        SM_Review_Sales_Order = 1,

        [Description("SE - Resubmit Sales Order")]
        SE_Resubmit_Sales_Order = 2,

        [Description("OM - Accept Schedule")]
        OM_Accept_Schedule = 3,

        [Description("TM - Accept Schedule")]
        TM_Accept_Schedule = 4,

        [Description("LM - Accept Schedule")]
        LM_Accept_Schedule = 5,

        [Description("AOM - Set Route Number")]
        AOM_Set_Route_Number = 6,

        [Description("TA - Assign Technician")]
        TA_Assign_Technician = 7,

        [Description("TI - Setup Machine")]
        TI_Setup_Machine = 8,

        [Description("TA - Review Machine")]
        TA_Review_Machine = 9,

        [Description("AOM - Set Route Schedule")]
        AOM_Set_Route_Schedule = 10,

        [Description("SE - Sales Verification")]
        SE_Sales_Verification = 11,

        [Description("LA - Delivery Of Machine")]
        LM_Delivery_Of_Machine = 12,

        [Description("TA - Set Machine In Use")]
        TA_Set_Machine_In_Use = 13,

        [Description("SA - Record Start Meter")]
        SA_Record_Start_Meter = 14,

        [Description("LA - Remove Older Machine")]
        LM_RemoveOlder_Machine = 15,

        [Description("TA - Set Older Asset Status")]
        TA_Set_OlderAsset_Status = 16,

        [Description("SA - Record End Meter")]
        SA_Record_End_Meter = 17,

        [Description("LA - Relocate Machine")]
        LM_Relocate_Machine = 18,

        [Description("SM - Approve Withdrawal")]
        Withdraw_SM_Approval = 19,

        [Description("OM - Accept Schedule")]
        Withdraw_OM_Accept_Schedule = 20,

        [Description("TM - Accept Schedule")]
        Withdraw_TM_Accept_Schedule = 21,

        [Description("LM - Accept Schedule")]
        Withdraw_LM_Accept_Schedule = 22,

        [Description("LA - Remove Older Machine")]
        Withdraw_LM_RemoveOlder_Machine = 23,

        [Description("TA - Set Machine In Warehouse")]
        Withdraw_TA_Set_Machine_In_Warehouse = 24,

        [Description("SA - Record End Meter")]
        Withdraw_SA_Record_End_Meter = 25,

        [Description("Void - SM Approval")]
        Void_SM_Approval = 26
    }
    public enum enmSalesApprovalActionType
    {
        [Description("Pending")]
        Pending = 1,

        [Description("Completed")]
        Submitted = 2,

        [Description("Completed")]
        Approved = 3,

        [Description("Rejected")]
        Rejected = 4,

        [Description("Not Applicable")]
        NA = 5
    }
    public enum enmSalesApprovalStartMeterType
    {
        [Description("Sales")]
        Sales = 1,

        [Description("Operations")]
        Operations = 2,

        [Description("Technical")]
        Technical = 3,

        [Description("Logistics")]
        Logistics = 4,

        [Description("Technical Hot Cup")]
        TA_HotCup = 5,

        [Description("LM Remove")]
        LM_Remove = 6,

        [Description("TA Hotcup Remove")]
        TA_HotCup_Remove = 7,

        [Description("SA Record Meter End")]
        SA_End_Meter = 8,

        [Description("LM Relocate")]
        LM_Relocate = 9,

        [Description("Withdraw - LM Remove")]
        Withdraw_LM_Remove = 10,

        [Description("Withdraw - TA Hotcup Remove")]
        Withdraw_TA_HotCup_Remove = 11,

        [Description("Withdraw - SA Record Meter End")]
        Withdraw_SA_End_Meter = 12
    }
    public enum enmAssetMovementActionType
    {
        Installed = 1,
        Removed = 2,
        Changed = 3,
        Relocated = 4
    }
    public enum enmServiceScheduleWeekType
    {
        Week1 = 1,
        Week2 = 2,
        Week3 = 3,
        Week4 = 4
    }
    public enum enmServiceScheduleWeekDays
    {
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6,
        Sunday = 7
    }
    public enum enumAssetStatusType
    {
        Available = 1, In_Use = 2, Scrap = 3, Ready = 4, Van = 5 //, Return = 5
    } // note : when add new asset type, add record in Tech_AssetStatusType db. enum id and  Tech_AssetStatusType.StatusTypeid MUST BE same
    public enum enumSalesItemAcitivity
    {
        [Description("Edit Item List Activity")]
        EditItemActity = 1,
    }
    public enum enumMachineType
    {
        None = 1, IMO = 2, RVM = 3
    }
    public enum enumTechQATestStatus
    {
        Cold_Drinks_Brix = 1,
        Hot_Drinks = 2,
        Carbonation = 3,
        Taste = 4,
        Appearance = 5,
        Temperature = 6,
        Syrup_Age = 7,
        Condition = 8
    }
    public enum EnumDashboardMovement
    {
        New_Sales = 1, Exchange = 2, Change_Company_Name = 3, Relocation = 4, Withdrawal = 5
    }
    public enum enumStatus
    {
        Completed = 1, Fail = 2, Progressing = 3
    }

    public enum enmSalesMeterReadingType
    {
        NEW = 1,
        MR = 2
    }

    public enum enmMetereReadingAuditStatus
    {
        [Description("Pending Audit")]
        Pending_Audit = 1,
        [Description("Audit Done")]
        Audit_Done = 2
    }
}
