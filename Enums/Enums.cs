using System.Runtime.Serialization;

namespace project_backend.Enums
{
    public enum TypeCommandState
    {
        Paid = 1,
        UnPaid = 2,

    }

    public enum TypeTableState
    {
        [EnumMember(Value = "none")]
        None,
        [EnumMember(Value = "Occupied")]
        Occupied,
        [EnumMember(Value = "Free")]
        Free,
    }
}
