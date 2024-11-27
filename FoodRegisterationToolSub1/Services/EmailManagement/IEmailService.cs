
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
using DotNetEnv;
using System.Runtime.CompilerServices;
using System.IO;


namespace EmailService {

    internal class EmailVerification {
        


        public static async Task SendVerificationCode(string email, string verificationCode) {

            var apiKey = Env.GetString("ApiKeySenGridConfiguration");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("noreplay@foodjournal.online", "Sign up for new User");
            var subject = "Your Verification Code";
            var to = new EmailAddress(email);

            string htmlTemplatePath = "wwwroot/templates/email/email.html";
            string htmlContent       = await File.ReadAllTextAsync(htmlTemplatePath);

            htmlContent = htmlContent.Replace("{{email}}", email);
            htmlContent = htmlContent.Replace("{{verificationcode}}", verificationCode);

            var msg = MailHelper.CreateSingleEmail(from, to, subject, "Your verification code", htmlContent);

            //await client.SendEmailAsync(msg);

            
            
