using System.Runtime.Serialization;

namespace project_backend.Enums
{
    public enum TypeCommandState
    {
        Generated = 1,
        Prepared = 2,
        Paid = 3,
    }

    public enum TypeTableState
    {
        [EnumMember(Value = "Ocupado")]
        Occupied,
        [EnumMember(Value = "Libre")]
        Free,
    }
}
