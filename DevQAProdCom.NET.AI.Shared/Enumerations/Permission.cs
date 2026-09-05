using System.ComponentModel;

namespace DevQAProdCom.NET.AI.Shared.Enumerations
{
    public enum Permission
    {
        [Description("approve-write-create-all")]
        ApproveWriteCreateAll,

        [Description("deny-write-create-all")]
        DenyWriteCreateAll,

        [Description("approve-write-edit-all")]
        ApproveWriteEditAll,

        [Description("deny-write-edit-all")]
        DenyWriteEditAll,

        [Description("approve-read-view-all")]
        ApproveReadViewAll,

        [Description("deny-read-view-all")]
        DenyReadViewAll,
    }
}
