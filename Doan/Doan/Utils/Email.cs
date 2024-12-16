using System.Net.Mail;
using System.Net;

namespace Doan.Utils
{
    public class Email
    {
        static readonly string from = "ltphat240103@gmail.com";
        static readonly string password = "quhxgastoodjtdzh";

        public static async Task<bool> SendEmailAsync(string to, string tieuDe, string noiDung)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential(from, password),
                    EnableSsl = true
                };

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(from),
                    Subject = tieuDe,
                    Body = noiDung,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(to);
                mailMessage.ReplyToList.Add(new MailAddress("21129858@st.hcmuaf.edu.vn"));

                // Gửi email không đồng bộ
                await smtpClient.SendMailAsync(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Gửi email thất bại: " + ex.Message);
                return false;
            }
        }
    }
}
