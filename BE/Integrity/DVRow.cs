using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CarAgency.BE.Integrity
{
    public sealed class DVRow
    {
        public IReadOnlyDictionary<string, object> Values { get; private set; }
        public string DVH { get; private set; }

        public DVRow(IDictionary<string, object> values, string dvh = null)
        {
            Values = new ReadOnlyDictionary<string, object>(new Dictionary<string, object>(values, StringComparer.OrdinalIgnoreCase));
            DVH = dvh;
        }

        public object GetValue(string name)
        {
            object value;
            if (!Values.TryGetValue(name, out value))
                throw new InvalidOperationException("Falta el valor de la columna " + name + ".");
            return value;
        }
    }

    public sealed class DVHorizontal
    {
        public string DVH { get; set; }
    }

    public sealed class DVPreparedRow
    {
        public IReadOnlyList<DVColumn> Columns { get; private set; }
        public DVRow Row { get; private set; }

        public DVPreparedRow(IReadOnlyList<DVColumn> columns, DVRow row)
        {
            Columns = columns;
            Row = row;
        }
    }

    public sealed class DVUserDigest
    {
        public Guid Id { get; set; }
        public string DVH { get; set; }
    }

    public sealed class DVFamilyDeletion
    {
        public Guid? BaseRoleId { get; private set; }
        public IReadOnlyList<DVUserDigest> UserDigests { get; private set; }

        public DVFamilyDeletion(Guid? baseRoleId, IList<DVUserDigest> digests)
        {
            BaseRoleId = baseRoleId;
            UserDigests = new ReadOnlyCollection<DVUserDigest>(digests);
        }
    }
}
