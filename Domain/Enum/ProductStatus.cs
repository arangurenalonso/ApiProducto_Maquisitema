namespace Domain.Enum
{
    using System.Runtime.Serialization;
    public enum ProductStatus
    {
        [EnumMember(Value = "Producto Inactivo")]
        Inactive,
        [EnumMember(Value = "Producto Activo")]
        Active
    }
}
