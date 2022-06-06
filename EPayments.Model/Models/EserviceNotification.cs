using EPayments.Common.Helpers;
using EPayments.Model.DataObjects.EmailTemplateContext;
using EPayments.Model.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Text;

namespace EPayments.Model.Models
{
    public partial class EserviceNotification
    {
        public int EserviceNotificationId { get; set; }
        public int PaymentRequestId { get; set; }
        public int EserviceClientId { get; set; }
        public string Url { get; set; }
        public string PostData { get; set; }
        public NotificationStatus NotificationStatusId { get; set; }
        public int FailedAttempts { get; set; }
        public string FailedAttemptsErrors { get; set; }
        public DateTime? SendNotBefore { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public byte[] Version { get; set; }

        public virtual EserviceClient EserviceClient { get; set; }

        private EserviceNotification()
        {
        }

        public EserviceNotification(PaymentRequest request, int? sendingDelayInMinutes = null)
        {
            var dataParam = new
            {
                Id = request.PaymentRequestIdentifier,
                Status = request.PaymentRequestStatusId.ToString(),
                ChangeTime = Formatter.DateTimeToIso8601Format(request.PaymentRequestStatusChangeTime)
            };

            var jsonData = JsonConvert.SerializeObject(dataParam);

            this.PaymentRequestId = request.PaymentRequestId;
            this.EserviceClientId = request.EserviceClientId;
            this.Url = request.AdministrativeServiceNotificationURL;
            this.PostData = jsonData;
            this.NotificationStatusId = NotificationStatus.Pending;
            this.FailedAttempts = 0;
            this.CreateDate = this.ModifyDate = DateTime.Now;
            this.SendNotBefore = sendingDelayInMinutes.HasValue ? DateTime.Now.AddMinutes(sendingDelayInMinutes.Value) : (DateTime?)null;
        }

        public void SetStatus(NotificationStatus notificationStatusId)
        {
            this.NotificationStatusId = notificationStatusId;
            this.ModifyDate = DateTime.Now;
        }

        public void IncrementFailedAttempts(string exception)
        {
            JObject fae;
            if (String.IsNullOrEmpty(this.FailedAttemptsErrors))
            {
                fae = new JObject();
            }
            else
            {
                fae = JObject.Parse(this.FailedAttemptsErrors);
            }
            fae.Add(this.FailedAttempts.ToString(), exception);
            this.FailedAttemptsErrors = fae.ToString();
            this.FailedAttempts++;
            this.ModifyDate = DateTime.Now;
        }

        public void SetNextSendingAttemptTime(DateTime? sendNotBefore, bool setStatusTerminated)
        {
            this.SendNotBefore = sendNotBefore;

            if (setStatusTerminated)
            {
                this.NotificationStatusId = NotificationStatus.Terminated;
            }
        }
    }

    public class EserviceNotificationMap : EntityTypeConfiguration<EserviceNotification>
    {
        public EserviceNotificationMap()
        {
            // Primary Key
            this.HasKey(t => t.EserviceNotificationId);

            this.Property(t => t.EserviceNotificationId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Properties
            this.Property(t => t.Url)
                .IsRequired();

            this.Property(t => t.PostData)
                .IsRequired();

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("EserviceNotifications");
            this.Property(t => t.EserviceNotificationId).HasColumnName("EserviceNotificationId");
            this.Property(t => t.PaymentRequestId).HasColumnName("PaymentRequestId");
            this.Property(t => t.EserviceClientId).HasColumnName("EserviceClientId");
            this.Property(t => t.Url).HasColumnName("Url");
            this.Property(t => t.PostData).HasColumnName("PostData");
            this.Property(t => t.NotificationStatusId).HasColumnName("NotificationStatusId");
            this.Property(t => t.FailedAttempts).HasColumnName("FailedAttempts");
            this.Property(t => t.FailedAttemptsErrors).HasColumnName("FailedAttemptsErrors");
            this.Property(t => t.SendNotBefore).HasColumnName("SendNotBefore");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
