namespace CarAgency.BE.Integrity
{
    public sealed class DV
    {
        [TableColumn] public string NombreEsquema { get; set; }
        [TableColumn] public string NombreTabla { get; set; }
        [TableColumn] public string DVV { get; set; }
        [TableColumn] public byte VersionAlgoritmo { get; set; }
    }
}
