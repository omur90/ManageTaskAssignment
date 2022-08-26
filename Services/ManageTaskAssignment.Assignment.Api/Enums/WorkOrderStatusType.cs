using System.ComponentModel;

namespace ManageTaskAssignment.Assignment.Api.Enums
{
    public enum WorkOrderStatusType
    {
        [Description("Üstlenilmeyi Bekliyor")]
        WaitingToAssign = 1,
        [Description("Üstlenildi")]
        Assaigned = 2,
        [Description("Tamamland")]
        Completed = 3,
        [Description("Reddedildi")]
        Ignored = 4,
        [Description("İptal Edildi")]
        Cancelled = 5,
    }
}
