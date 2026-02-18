using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using GolfViewApartments.API.Services.Interfaces;

namespace GolfViewApartments.API.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<bool> SendBookingConfirmationAsync(BookingConfirmationEmail emailData)
        {
            try
            {
                var smtpSettings = _configuration.GetSection("EmailSettings");
                
                // Get configuration values with null checks
                var smtpHost = smtpSettings["SmtpHost"];
                var smtpPortStr = smtpSettings["SmtpPort"];
                var smtpUsername = smtpSettings["SmtpUsername"];
                var smtpPassword = smtpSettings["SmtpPassword"];
                var fromEmail = smtpSettings["FromEmail"];
                var fromName = smtpSettings["FromName"];
                var bccEmail = smtpSettings["BccEmail"];

                // Validate required settings
                if (string.IsNullOrEmpty(smtpHost))
                {
                    _logger.LogError("SMTP Host is not configured");
                    return false;
                }

                if (string.IsNullOrEmpty(smtpUsername))
                {
                    _logger.LogError("SMTP Username is not configured");
                    return false;
                }

                if (string.IsNullOrEmpty(smtpPassword))
                {
                    _logger.LogError("SMTP Password is not configured");
                    return false;
                }

                if (string.IsNullOrEmpty(fromEmail))
                {
                    _logger.LogError("From Email is not configured");
                    return false;
                }

                // Parse port with default
                if (!int.TryParse(smtpPortStr, out int smtpPort))
                {
                    smtpPort = 587; // Default SMTP port
                }

                using var smtpClient = new SmtpClient(smtpHost, smtpPort)
                {
                    Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(fromEmail, fromName ?? "GolfView Apartments"),
                    Subject = $"Booking Confirmation - {emailData.BookingReference}",
                    Body = GenerateEmailBody(emailData),
                    IsBodyHtml = true
                };

                mailMessage.To.Add(new MailAddress(emailData.GuestEmail, emailData.GuestName));

                // BCC to hotel management if configured
                if (!string.IsNullOrEmpty(bccEmail))
                {
                    mailMessage.Bcc.Add(bccEmail);
                }

                await smtpClient.SendMailAsync(mailMessage);
                _logger.LogInformation($"Booking confirmation email sent successfully to {emailData.GuestEmail}");
                
                return true;
            }
            catch (SmtpException smtpEx)
            {
                _logger.LogError(smtpEx, $"SMTP error sending booking confirmation email to {emailData.GuestEmail}. Status: {smtpEx.StatusCode}");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send booking confirmation email to {emailData.GuestEmail}");
                return false;
            }
        }

        private string GenerateEmailBody(BookingConfirmationEmail emailData)
        {
            var childrenInfo = emailData.Children > 0 
                ? $", {emailData.Children} Child(ren)" 
                : "";

            var childrenAgesInfo = "";
            if (emailData.Children > 0 && !string.IsNullOrEmpty(emailData.ChildrenAges))
            {
                childrenAgesInfo = $"<p style=\"margin: 5px 0; color: #666;\">Children Ages: {emailData.ChildrenAges}</p>";
            }

            var specialRequestsSection = "";
            if (!string.IsNullOrEmpty(emailData.SpecialRequests))
            {
                specialRequestsSection = $@"
                    <div style=""background: #FFF9E6; border-left: 4px solid #F59E0B; padding: 15px; margin: 20px 0; border-radius: 4px;"">
                        <h3 style=""margin: 0 0 10px 0; color: #92400E; font-size: 16px;"">
                            <span style=""margin-right: 8px;"">📝</span>Special Requests
                        </h3>
                        <p style=""margin: 0; color: #78350F; line-height: 1.6;"">{emailData.SpecialRequests}</p>
                    </div>";
            }

            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset=""utf-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Booking Confirmation</title>
</head>
<body style=""margin: 0; padding: 0; font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; background-color: #f5f5f5;"">
    <table role=""presentation"" style=""width: 100%; border-collapse: collapse;"">
        <tr>
            <td style=""padding: 20px 0;"">
                <table role=""presentation"" style=""max-width: 600px; margin: 0 auto; background-color: #ffffff; border-radius: 8px; box-shadow: 0 2px 4px rgba(0,0,0,0.1);"">
                    <!-- Header -->
                    <tr>
                        <td style=""background: linear-gradient(135deg, #2C5F2D 0%, #97BC62 100%); padding: 40px 30px; text-align: center; border-radius: 8px 8px 0 0;"">
                            <div style=""background: white; width: 80px; height: 80px; margin: 0 auto 20px; border-radius: 50%; display: flex; align-items: center; justify-content: center; box-shadow: 0 4px 6px rgba(0,0,0,0.1);"">
                                <span style=""font-size: 40px;"">✓</span>
                            </div>
                            <h1 style=""margin: 0; color: #ffffff; font-size: 28px; font-weight: bold;"">Booking Confirmed!</h1>
                            <p style=""margin: 10px 0 0 0; color: #ffffff; font-size: 16px; opacity: 0.95;"">Your reservation has been successfully received</p>
                        </td>
                    </tr>

                    <!-- Booking Reference -->
                    <tr>
                        <td style=""padding: 30px;"">
                            <div style=""background: #F0F9FF; border: 2px solid #2C5F2D; border-radius: 8px; padding: 20px; text-align: center; margin-bottom: 30px;"">
                                <p style=""margin: 0 0 8px 0; color: #666; font-size: 14px;"">Booking Reference</p>
                                <h2 style=""margin: 0; color: #2C5F2D; font-size: 32px; letter-spacing: 2px; font-weight: bold;"">{emailData.BookingReference}</h2>
                            </div>

                            <!-- Welcome Message -->
                            <p style=""margin: 0 0 20px 0; color: #333; font-size: 16px; line-height: 1.6;"">
                                Dear <strong>{emailData.GuestName}</strong>,
                            </p>
                            <p style=""margin: 0 0 30px 0; color: #666; font-size: 15px; line-height: 1.6;"">
                                Thank you for choosing Golf View Apartments! We're delighted to confirm your booking request. 
                                Our team will review your reservation and contact you within 24 hours to finalize the details.
                            </p>

                            <!-- Booking Details -->
                            <div style=""background: #f8f9fa; border-radius: 8px; padding: 25px; margin-bottom: 30px;"">
                                <h3 style=""margin: 0 0 20px 0; color: #2C5F2D; font-size: 18px; border-bottom: 2px solid #2C5F2D; padding-bottom: 10px;"">
                                    <span style=""margin-right: 8px;"">🏨</span>Reservation Details
                                </h3>

                                <!-- Guest Info -->
                                <div style=""margin-bottom: 20px;"">
                                    <table style=""width: 100%; border-collapse: collapse;"">
                                        <tr>
                                            <td style=""padding: 8px 0; width: 40%; color: #666; font-size: 14px;"">
                                                <strong>Guest Name:</strong>
                                            </td>
                                            <td style=""padding: 8px 0; color: #333; font-size: 14px;"">
                                                {emailData.GuestName}
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style=""padding: 8px 0; color: #666; font-size: 14px;"">
                                                <strong>Email:</strong>
                                            </td>
                                            <td style=""padding: 8px 0; color: #333; font-size: 14px;"">
                                                {emailData.GuestEmail}
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style=""padding: 8px 0; color: #666; font-size: 14px;"">
                                                <strong>Phone:</strong>
                                            </td>
                                            <td style=""padding: 8px 0; color: #333; font-size: 14px;"">
                                                {emailData.GuestPhone}
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                                <!-- Accommodation Details -->
                                <div style=""background: white; border-radius: 6px; padding: 15px; margin-bottom: 15px;"">
                                    <h4 style=""margin: 0 0 12px 0; color: #2C5F2D; font-size: 16px;"">
                                        <span style=""margin-right: 8px;"">🛏️</span>Accommodation
                                    </h4>
                                    <p style=""margin: 5px 0; color: #333; font-size: 14px;""><strong>Room:</strong> {emailData.RoomNumber}</p>
                                    <p style=""margin: 5px 0; color: #666; font-size: 14px;""><strong>Type:</strong> {emailData.RoomType}</p>
                                    <p style=""margin: 5px 0; color: #666; font-size: 14px;""><strong>Meal Plan:</strong> {emailData.BoardType}</p>
                                    <p style=""margin: 5px 0; color: #666; font-size: 14px;""><strong>Occupancy:</strong> {emailData.Occupancy}</p>
                                </div>

                                <!-- Stay Dates -->
                                <div style=""background: white; border-radius: 6px; padding: 15px; margin-bottom: 15px;"">
                                    <h4 style=""margin: 0 0 12px 0; color: #2C5F2D; font-size: 16px;"">
                                        <span style=""margin-right: 8px;"">📅</span>Stay Dates
                                    </h4>
                                    <table style=""width: 100%;"">
                                        <tr>
                                            <td style=""width: 50%; padding-right: 10px;"">
                                                <p style=""margin: 0 0 5px 0; color: #666; font-size: 12px;"">Check-in</p>
                                                <p style=""margin: 0; color: #333; font-size: 15px; font-weight: bold;"">{emailData.CheckInDate}</p>
                                            </td>
                                            <td style=""width: 50%; padding-left: 10px;"">
                                                <p style=""margin: 0 0 5px 0; color: #666; font-size: 12px;"">Check-out</p>
                                                <p style=""margin: 0; color: #333; font-size: 15px; font-weight: bold;"">{emailData.CheckOutDate}</p>
                                            </td>
                                        </tr>
                                    </table>
                                    <p style=""margin: 12px 0 0 0; color: #666; font-size: 14px;"">
                                        <strong>Duration:</strong> {emailData.TotalNights} night(s)
                                    </p>
                                </div>

                                <!-- Guests -->
                                <div style=""background: white; border-radius: 6px; padding: 15px;"">
                                    <h4 style=""margin: 0 0 8px 0; color: #2C5F2D; font-size: 16px;"">
                                        <span style=""margin-right: 8px;"">👥</span>Guests
                                    </h4>
                                    <p style=""margin: 5px 0; color: #333; font-size: 14px;"">{emailData.Adults} Adult(s){childrenInfo}</p>
                                    {childrenAgesInfo}
                                </div>
                            </div>

                            {specialRequestsSection}

                            <!-- Price Summary -->
                            <div style=""background: linear-gradient(135deg, #F0F9FF 0%, #E0F2FE 100%); border: 2px solid #2C5F2D; border-radius: 8px; padding: 20px; margin-bottom: 30px;"">
                                <h3 style=""margin: 0 0 15px 0; color: #2C5F2D; font-size: 18px;"">
                                    <span style=""margin-right: 8px;"">💰</span>Price Summary
                                </h3>
                                <table style=""width: 100%; border-collapse: collapse;"">
                                    <tr>
                                        <td style=""padding: 8px 0; color: #666; font-size: 14px;"">
                                            {emailData.TotalNights} night(s) × KES {emailData.PricePerNight:N0}
                                        </td>
                                        <td style=""padding: 8px 0; text-align: right; color: #333; font-size: 14px;"">
                                            KES {emailData.TotalPrice:N0}
                                        </td>
                                    </tr>
                                    <tr style=""border-top: 2px solid #2C5F2D;"">
                                        <td style=""padding: 12px 0 0 0;"">
                                            <strong style=""color: #2C5F2D; font-size: 18px;"">Total Amount</strong>
                                        </td>
                                        <td style=""padding: 12px 0 0 0; text-align: right;"">
                                            <strong style=""color: #2C5F2D; font-size: 24px;"">KES {emailData.TotalPrice:N0}</strong>
                                        </td>
                                    </tr>
                                </table>
                            </div>

                            <!-- Next Steps -->
                            <div style=""background: #EFF6FF; border-left: 4px solid #3B82F6; padding: 20px; margin-bottom: 30px; border-radius: 4px;"">
                                <h3 style=""margin: 0 0 15px 0; color: #1E40AF; font-size: 18px;"">
                                    <span style=""margin-right: 8px;"">📋</span>What's Next?
                                </h3>
                                <ul style=""margin: 0; padding-left: 20px; color: #1E3A8A;"">
                                    <li style=""margin-bottom: 10px; line-height: 1.6;"">Our team will review your reservation and contact you within <strong>24 hours</strong></li>
                                    <li style=""margin-bottom: 10px; line-height: 1.6;"">We'll confirm availability and provide payment instructions</li>
                                    <li style=""margin-bottom: 10px; line-height: 1.6;"">Please keep your booking reference <strong>{emailData.BookingReference}</strong> handy</li>
                                    <li style=""margin-bottom: 0; line-height: 1.6;"">Check-in time is 2:00 PM, Check-out time is 11:00 AM</li>
                                </ul>
                            </div>

                            <!-- Contact Info -->
                            <div style=""background: #F9FAFB; border-radius: 8px; padding: 20px; text-align: center;"">
                                <h4 style=""margin: 0 0 15px 0; color: #374151; font-size: 16px;"">Need Help?</h4>
                                <p style=""margin: 0 0 8px 0; color: #6B7280; font-size: 14px;"">
                                    <strong>Email:</strong> 
                                    <a href=""mailto:lapfund9@gmail.com"" style=""color: #2C5F2D; text-decoration: none;"">
                                        lapfund9@gmail.com
                                    </a>
                                </p>
                                <p style=""margin: 0; color: #6B7280; font-size: 14px;"">
                                    <strong>Phone:</strong> 
                                    <a href=""tel:+254700000000"" style=""color: #2C5F2D; text-decoration: none;"">
                                        +254 700 000 000
                                    </a>
                                </p>
                            </div>
                        </td>
                    </tr>

                    <!-- Footer -->
                    <tr>
                        <td style=""background: #2C5F2D; padding: 30px; text-align: center; border-radius: 0 0 8px 8px;"">
                            <p style=""margin: 0 0 10px 0; color: #ffffff; font-size: 18px; font-weight: bold;"">Golf View Apartments</p>
                            <p style=""margin: 0 0 15px 0; color: #ffffff; opacity: 0.9; font-size: 14px;"">
                                Your home away from home
                            </p>
                            <p style=""margin: 0; color: #ffffff; opacity: 0.8; font-size: 12px;"">
                                © 2026 GolfView Apartments. All rights reserved.
                            </p>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>";
        }
    }
}