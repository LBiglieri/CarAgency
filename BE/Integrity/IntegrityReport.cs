using System.Collections.Generic;
using System.Linq;

namespace CarAgency.BE.Integrity
{
    public sealed class IntegrityReport
    {
        public IReadOnlyList<string> Issues { get; private set; }
        public bool IsConsistent { get { return Issues.Count == 0; } }
        public bool CanRecalculate { get; private set; }

        public IntegrityReport(IEnumerable<string> issues, bool canRecalculate)
        {
            Issues = issues.ToList().AsReadOnly();
            CanRecalculate = canRecalculate;
        }
    }
}
