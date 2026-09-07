namespace CarAgency.BE.Integrity
{
    public sealed class DVColumn
    {
        [TableColumn] public string Name { get; set; }
        [TableColumn] public string SqlType { get; set; }
        [TableColumn] public int MaxLength { get; set; }
        [TableColumn] public bool IsNullable { get; set; }
        [TableColumn] public int CodePage { get; set; }
        [TableColumn] public bool IsKey { get; set; }
    }
}
