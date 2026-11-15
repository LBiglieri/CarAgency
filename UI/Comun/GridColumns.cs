using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Security.Session;

namespace UI.Comun
{
    // Las grillas que generan las columnas desde la entidad muestran el nombre de la propiedad y los Id.
    // Show deja visibles solo las columnas pedidas, en ese orden, con el encabezado del tag
    // Col_<Propiedad>; los importes salen con dos decimales y las fechas con hora.
    public static class GridColumns
    {
        // Columnas de un Vehiculo que se muestran en las grillas de stock y de cotizacion/reserva.
        public static readonly string[] Vehiculo = { "Patente", "Marca_Descripcion", "Modelo_Descripcion", "Version_Descripcion",
            "Color_Descripcion", "Anio", "Kilometraje", "Puertas", "Precio", "Opcionales", "Observaciones" };

        // Ultimas columnas pedidas por grilla. Si el DataSource se asigna antes de mostrar el formulario,
        // la grilla vuelve a enlazar al crear su BindingContext y reordena las columnas autogeneradas:
        // el orden se reaplica en cada DataBindingComplete.
        private static readonly ConditionalWeakTable<DataGridView, string[]> requested = new ConditionalWeakTable<DataGridView, string[]>();

        public static void Show(DataGridView grid, params string[] columns)
        {
            string[] previous;
            if (requested.TryGetValue(grid, out previous))
                requested.Remove(grid);
            else
                grid.DataBindingComplete += (sender, e) =>
                {
                    string[] current;
                    if (requested.TryGetValue(grid, out current))
                        Apply(grid, current);
                };
            requested.Add(grid, columns);
            Apply(grid, columns);
        }

        private static void Apply(DataGridView grid, string[] columns)
        {
            foreach (DataGridViewColumn column in grid.Columns)
                column.Visible = false;
            // Cambiar un DisplayIndex corre a las demas columnas: se insertan de la ultima a la primera
            // en la posicion 0, asi quedan adelante y en el orden pedido.
            for (int i = columns.Length - 1; i >= 0; i--)
            {
                DataGridViewColumn column = grid.Columns[columns[i]];
                if (column == null) continue;
                column.Visible = true;
                column.DisplayIndex = 0;
                if (column.ValueType == typeof(double))
                    column.DefaultCellStyle.Format = "N2";
                else if (column.ValueType == typeof(DateTime))
                    column.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }
            Translate(grid);
        }

        // Para UpdateLanguage: vuelve a traducir los encabezados de las columnas visibles.
        public static void Translate(DataGridView grid)
        {
            foreach (DataGridViewColumn column in grid.Columns)
                if (column.Visible)
                    column.HeaderText = LanguageService.GetTagText("Col_" + column.Name, column.Name);
        }

        // Traduce al pintar los valores de una columna de texto (Color_Descripcion, TipoPago_Descripcion).
        // Se suscribe una sola vez, en el constructor del formulario.
        public static void TranslateValues(DataGridView grid, string column, Func<string, string> text)
        {
            grid.CellFormatting += (sender, e) =>
            {
                if (e.ColumnIndex >= 0 && grid.Columns[e.ColumnIndex].Name == column && e.Value is string value)
                    e.Value = text(value);
            };
        }
    }
}
