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
            string from,
            string fromName,
            IEnumerable<EmailAttachment> attachments = null);
    }
}
