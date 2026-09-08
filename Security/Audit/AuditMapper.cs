using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CarAgency.BE.Audit;
using CarAgency.DAL.Audit;
using CarAgency.DAL.Persistence;
using CarAgency.Security.Integrity;

namespace CarAgency.Security.Audit
{
    public sealed class AuditMapper
    {
        private const string Table = "Events";
        private readonly AuditDataAccess data;
        private readonly string connectionString;
        public AuditMapper() : this(new DatabaseConnectionProvider().GetConnectionString()) { }
        public AuditMapper(string connectionString)
        {
            this.connectionString = connectionString;
            data = new AuditDataAccess(connectionString);
        }

        public bool ActorExists(Guid userId) { return data.ActorExists(userId); }

        public void Insert(AuditEvent entry)
        {
            var digitVerifier = new DigitVerifierWriteMapper(connectionString);
            data.Insert(entry.Id, entry.UserId, entry.AttemptedLogin, entry.OccurredAt, entry.Module.ToString(),
                entry.EventType.ToString(), entry.Criticality, entry.TargetId,
                command => digitVerifier.PrepareDvh(command, Table));
            digitVerifier.UpdateDvv(Table);
        }

        public List<AuditEvent> Query(AuditFilter filter)
        {
            using (var table = data.Query(filter.Login, filter.From.Date, filter.To.Date.AddDays(1),
                filter.Module.HasValue ? filter.Module.ToString() : null,
                filter.EventType.HasValue ? filter.EventType.ToString() : null, filter.Criticality))
            {
                return table.Rows.Cast<DataRow>().Select(row => new AuditEvent {
                    Id = (Guid)row["Id"], UserId = row["UserId"] == DBNull.Value ? (Guid?)null : (Guid)row["UserId"],
                    Login = (string)row["Login"], Name = (string)row["Name"], Surname = (string)row["Surname"],
                    OccurredAt = (DateTime)row["OccurredAt"], Module = (AuditModule)Enum.Parse(typeof(AuditModule), (string)row["Module"]),
                    EventType = (AuditEventType)Enum.Parse(typeof(AuditEventType), (string)row["EventType"]),
                    Criticality = Convert.ToInt32(row["Criticality"]),
                    TargetId = row["TargetId"] == DBNull.Value ? (Guid?)null : (Guid)row["TargetId"]
                }).ToList();
            }
        }
        public List<string> GetLogins()
        { using (var table = data.GetLogins()) return table.Rows.Cast<DataRow>().Select(r => (string)r["Login"]).ToList(); }
    }
}
