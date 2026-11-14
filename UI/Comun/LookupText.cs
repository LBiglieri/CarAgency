using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Security.Session;

namespace UI.Comun
{
    // Color y TipoPago guardan la descripcion en ingles y la logica compara contra ella
    // (PagoBLL.DefaultPaymentType, las tarjetas de AddPagoForm, CompraBLL.BankTransferPaymentType):
    // la base no se traduce. Se traduce al mostrar, con los tags Colour_<Descripcion> y
    // PaymentType_<Descripcion> sin espacios. Un valor nuevo sin tag se muestra tal cual.
    public static class LookupText
    {
        public static string Colour(string description)
        {
            return Translate("Colour_", description);
        }

        public static string PaymentType(string description)
        {
            return Translate("PaymentType_", description);
        }

        private static string Translate(string prefix, string description)
        {
            if (string.IsNullOrEmpty(description)) return description;
            return LanguageService.GetTagText(prefix + description.Replace(" ", ""), description);
        }

        // Carga el combo con la entidad como Key y el texto traducido como Value: SelectedItem deja
        // de ser la entidad, se lee con Selected<T>. Sin seleccion, selected devuelve false para todos.
        public static void Fill<T>(ComboBox combo, IEnumerable<T> items, Func<T, string> text, Func<T, bool> selected)
        {
            List<KeyValuePair<T, string>> pairs = items.Select(i => new KeyValuePair<T, string>(i, text(i))).ToList();
            combo.DataSource = pairs;
            combo.DisplayMember = "Value";
            combo.ValueMember = "Key";
            combo.SelectedIndex = pairs.FindIndex(p => selected(p.Key));
        }

        public static T Selected<T>(ComboBox combo) where T : class
        {
            return combo.SelectedItem is KeyValuePair<T, string> pair ? pair.Key : null;
        }
    }
}
