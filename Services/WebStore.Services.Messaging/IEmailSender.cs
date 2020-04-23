﻿namespace WebStore.Services.Messaging
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEmailSender
    {
        Task SendEmailAsync(
            string to,
            string subject,
            string htmlContent,
            string from = null,
            string fromName = null,
            IEnumerable<EmailAttachment> attachments = null);
    }
}
