
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
using DotNetEnv;
using System.Runtime.CompilerServices;
using System.IO;


namespace EmailService {
/// <summary>
    /// Handles email-related operations such as sending verification emails and embedded image emails.
    /// </summary>
    internal class EmailVerification {
        /// <summary>
        /// Entry point for testing the email service.
        /// Sends a test email with an embedded image.
        /// </summary>
        private static async Task Main() {

            await Execute();
        }
        /// <summary>
        /// Sends a test email with an embedded image using the SendGrid API.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public static async Task Execute()  {

            string base64image = "data:image/jpeg;base64," + Base64Converter.ConvertImageToBase64("wwwroot/images/email.png");

            var apiKey = Env.GetString("ApiKeySenGridConfiguration");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("noreplay", "Hilsen fra kollega");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("", "Example User");
            
            var htmlContent  = $@"
            <h1>Welcome to Our Service</h1>
            <p>This is a test email with an image:</p>
            <img src='{base64image}' alt='Embedded Image' />
            <p>Thank you for signing up!</p>";
            var plaintext = "Hello";

            var msg = MailHelper.CreateSingleEmail(from, to, subject,plaintext, htmlContent);
            var response = await client.SendEmailAsync(msg);

            Console.WriteLine($"Response Status Code: {response.StatusCode}");
            Console.WriteLine($"Response Body: {await response.Body.ReadAsStringAsync()}");
            Console.WriteLine($"Response Headers: {response.Headers}");
        }
      /// <summary>
        /// Sends a verification email to a user with a verification code.
        /// </summary>
        /// <param name="email">The recipient's email address.</param>
        /// <param name="verificationCode">The verification code to include in the email.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="FileNotFoundException">
        /// Thrown if the email template file is not found at the specified path.
        /// </exception>
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

            await client.SendEmailAsync(msg);

            
            
                    }
    }
}
