using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace backend.Services;
public class EmailService
{
    private readonly SmtpSettings _smtpSettings;
    public EmailService(IOptions<SmtpSettings> smtpSettingd, ILogger<EmailService> logger)
        => _smtpSettings = smtpSettingd.Value;
    
    public bool Send(
        string toName,
        string toEmail,
        string subject,
        string body)
    {
        var smtpClient = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port)
        {
            Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
            DeliveryMethod = SmtpDeliveryMethod.Network,
            EnableSsl = true
        };

        var mail = new MailMessage
        {
            From = new MailAddress(_smtpSettings.Username, "Contato EventHub"),
            To = { new MailAddress(toEmail, toName) },
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        try
        {
            smtpClient.Send(mail);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

public string HtmlForEmail(string name, string password)
    {
        return $@"
            <!DOCTYPE html>
            <html lang=""pt-br"">
            <head>
            <meta charset=""UTF-8"">
            <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
            <title>EventHub - Senha do Usuário</title>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    background-color: #0f172a;
                    margin: 0;
                    padding: 0;
                    color: white;
                }}
                .container {{
                    width: 100%;
                    max-width: 600px;
                    margin: 30px auto;
                    padding: 20px;
                    background-color: #1e293b;
                    border-radius: 12px;
                    box-shadow: 0 4px 6px -1px rgba(0, 0, 0, 0.3);
                }}
                .header {{
                    text-align: center;
                    background-color: #2563eb;
                    background-image: linear-gradient(to right, #2563eb, #1d4ed8);
                    color: white;
                    padding: 20px;
                    border-radius: 12px 12px 0 0;
                }}
                .header h1 {{
                    margin: 0;
                    font-size: 28px;
                    font-weight: 700;
                }}
                .content {{
                    padding: 30px 20px;
                    text-align: center;
                    line-height: 1.6;
                    color: white;
                }}
                .password {{
                    display: inline-block;
                    margin: 20px 0;
                    padding: 15px 30px;
                    font-size: 20px;
                    font-weight: bold;
                    color: white;
                    background-color: #1e293b;
                    border: 2px solid #2563eb;
                    border-radius: 8px;
                }}
                .warning {{
                    margin-top: 20px;
                    padding: 15px;
                    background-color: #27272a;
                    border: 1px solid #3f3f46;
                    border-radius: 8px;
                    color: white;
                    font-size: 14px;
                }}
                .footer {{
                    text-align: center;
                    margin-top: 30px;
                    padding-top: 20px;
                    border-top: 1px solid #334155;
                    font-size: 13px;
                    color: white;
                }}
                .footer p {{
                    margin: 5px 0;
                }}
            </style>
            </head>
            <body>
            <div class=""container"">
                <div class=""header"">
                    <h1>EventHub</h1>
                </div>
                <div class=""content"">
                    <p>Olá, {name}!</p>
                    <p>Bem-vindo ao <strong>EventHub</strong>! Esta é a sua senha de acesso:</p>
                    <div class=""password"">{password}</div>
                    <div class=""warning"">
                        <p>⚠️ Por razões de segurança, recomendamos que você altere esta senha após seu primeiro acesso.</p>
                    </div>
                </div>
                <div class=""footer"">
                    <p>&copy; 2025 EventHub. Todos os direitos reservados.</p>
                </div>
            </div>
            </body>
            </html>
        ";
    }

    public string HtmlForEmailVerification(string name, string verificationLink)
    {
        return $@"
            <!DOCTYPE html>
            <html lang=""pt-br"">
            <head>
                <meta charset=""UTF-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <title>EventHub - Verificação de E-mail</title>
                <style>
                    body {{
                        font-family: Arial, sans-serif;
                        background-color: #0f172a;
                        margin: 0;
                        padding: 0;
                        color: white;
                    }}
                    .container {{
                        width: 100%;
                        max-width: 600px;
                        margin: 30px auto;
                        padding: 20px;
                        background-color: #1e293b;
                        border-radius: 12px;
                        box-shadow: 0 4px 6px -1px rgba(0, 0, 0, 0.3);
                    }}
                    .header {{
                        text-align: center;
                        background-color: #2563eb;
                        background-image: linear-gradient(to right, #2563eb, #1d4ed8);
                        color: white;
                        padding: 20px;
                        border-radius: 12px 12px 0 0;
                    }}
                    .header h1 {{
                        margin: 0;
                        font-size: 28px;
                        font-weight: 700;
                    }}
                    .content {{
                        padding: 30px 20px;
                        text-align: center;
                        line-height: 1.6;
                        color: white;
                    }}
                    .verify-button {{
                        display: inline-block;
                        margin: 25px 0;
                        padding: 12px 32px;
                        font-size: 16px;
                        font-weight: 600;
                        color: white;
                        background-color: #2563eb;
                        text-decoration: none;
                        border-radius: 8px;
                    }}
                    .warning {{
                        margin-top: 20px;
                        padding: 15px;
                        background-color: #27272a;
                        border: 1px solid #3f3f46;
                        border-radius: 8px;
                        color: white;
                        font-size: 14px;
                    }}
                    .footer {{
                        text-align: center;
                        margin-top: 30px;
                        padding-top: 20px;
                        border-top: 1px solid #334155;
                        font-size: 13px;
                        color: white;
                    }}
                    .footer p {{
                        margin: 5px 0;
                    }}
                </style>
            </head>
            <body>
                <div class=""container"">
                    <div class=""header"">
                        <h1>EventHub</h1>
                    </div>
                    <div class=""content"">
                        <p>Olá, {name}!</p>
                        <p>Para concluir seu cadastro, clique no botão abaixo:</p>
                        <a href=""{verificationLink}"" class=""verify-button"">Verificar E-mail</a>
                        <div class=""warning"">
                            <p>⚠️ Este link é válido por apenas 1 hora.</p>
                            <p>Após esse período, será necessário solicitar um novo link de verificação.</p>
                        </div>
                        <p style=""margin-top: 20px; color: white;"">Se você não solicitou este e-mail, pode ignorá-lo com segurança.</p>
                    </div>
                    <div class=""footer"">
                        <p>&copy; 2025 EventHub. Todos os direitos reservados.</p>
                    </div>
                </div>
            </body>
            </html>
        ";
    }
}