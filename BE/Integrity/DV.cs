namespace CarAgency.BE.Integrity
{
    public sealed class DV
    {
        [TableColumn] public string SchemaName { get; set; }
        [TableColumn] public string TableName { get; set; }
        [TableColumn] public string DVV { get; set; }
        [TableColumn] public byte AlgorithmVersion { get; set; }
        [TableColumn] public string KeyId { get; set; }
    }
}
