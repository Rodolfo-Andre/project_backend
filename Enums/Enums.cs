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
        [EnumMember(Value = "Ocupado")]
        Occupied,
        [EnumMember(Value = "Libre")]
        Free,
    }
}
