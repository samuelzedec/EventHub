using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace backend.Services;
public class EmailService
{
    private readonly SmtpSettings _smtpSettings;
    public EmailService(IOptions<SmtpSettings> smtpSettingd)
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
        catch (Exception ex)
        {
            Console.WriteLine(ex);
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
                        background-color: #f4f4f9;
                        margin: 0;
                        padding: 0;
                        color: #333;
                    }}
                    .container {{
                        width: 100%;
                        max-width: 600px;
                        margin: 30px auto;
                        padding: 20px;
                        background-color: #fff;
                        border-radius: 8px;
                        box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
                    }}
                    .header {{
                        text-align: center;
                        background-color: #007bff;
                        color: white;
                        padding: 15px;
                        border-radius: 8px 8px 0 0;
                    }}
                    .header h1 {{
                        margin: 0;
                        font-size: 24px;
                    }}
                    .content {{
                        padding: 20px;
                        text-align: center;
                    }}
                    .password {{
                        font-size: 18px;
                        font-weight: bold;
                        color: #007bff;
                        margin-top: 10px;
                    }}
                    .footer {{
                        text-align: center;
                        margin-top: 30px;
                        font-size: 12px;
                        color: #aaa;
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
                        <p class=""password"">{password}</p>
                        <p>Use esta senha para acessar sua conta. Recomendamos que você altere a senha após o primeiro login.</p>
                    </div>
                    <div class=""footer"">
                        <p>&copy; 2025 EventHub. Todos os direitos reservados.</p>
                    </div>
                </div>
            </body>
            </html>
        ";
    }

    public string HtmlForEmailVerification(string name, int verificationCode, string verificationLink)
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
                        background-color: #f4f4f9;
                        margin: 0;
                        padding: 0;
                        color: #333;
                    }}
                    .container {{
                        width: 100%;
                        max-width: 600px;
                        margin: 30px auto;
                        padding: 20px;
                        background-color: #fff;
                        border-radius: 8px;
                        box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
                    }}
                    .header {{
                        text-align: center;
                        background-color: #007bff;
                        color: white;
                        padding: 15px;
                        border-radius: 8px 8px 0 0;
                    }}
                    .header h1 {{
                        margin: 0;
                        font-size: 24px;
                    }}
                    .content {{
                        padding: 20px;
                        text-align: center;
                    }}
                    .verification-code {{
                        font-size: 22px;
                        font-weight: bold;
                        color: #007bff;
                        margin-top: 20px;
                        padding: 10px;
                        background-color: #f0f8ff;
                        border-radius: 5px;
                    }}
                    .verify-button {{
                        display: inline-block;
                        margin-top: 20px;
                        padding: 10px 20px;
                        font-size: 16px;
                        font-weight: bold;
                        color: white;
                        background-color: #007bff;
                        text-decoration: none;
                        border-radius: 5px;
                    }}
                    .footer {{
                        text-align: center;
                        margin-top: 30px;
                        font-size: 12px;
                        color: #aaa;
                    }}
                    .footer p {{
                        margin: 0;
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
                        <p>Para concluir seu cadastro, insira o código de verificação abaixo ou clique no botão:</p>
                        <p class=""verification-code"">{verificationCode}</p>
                        <a href=""{verificationLink}"" class=""verify-button"">Verificar E-mail</a>
                        <p>Este código é válido por 1 hora. Caso não tenha solicitado, por favor, ignore este e-mail.</p>
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