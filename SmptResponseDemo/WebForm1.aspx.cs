using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Web.Services.Protocols;
using System.Web.UI;
using System.Web.UI.WebControls;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Pop3;
using MailKit.Search;
using MailKit.Security;

namespace SmptResponseDemo
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            MailMessage mail = new MailMessage();
            mail.Subject = txttitle.Text;
            mail.Body = txtmsg.Text;
            mail.IsBodyHtml = true;
            //mail.From = new MailAddress(from, fromName);
            mail.From = new MailAddress("test@test.com", "Test");

            mail.To.Add(txtToAddress.Text);

            SmtpClient smtp = new SmtpClient();
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            try
            {
                smtp.Send(mail);
            }
            catch (SmtpFailedRecipientsException ex)
            {
                for (int i = 0; i < ex.InnerExceptions.Length; i++)
                {
                    SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
                    if (status == SmtpStatusCode.MailboxBusy ||
                        status == SmtpStatusCode.MailboxUnavailable)
                    {
                        Console.WriteLine("Delivery failed - retrying in 5 seconds.");
                        System.Threading.Thread.Sleep(5000);
                        smtp.Send(mail);
                    }
                    else
                    {
                        Console.WriteLine("Failed to deliver message to {0}",
                            ex.InnerExceptions[i].FailedRecipient);
                    }
                }

            }
            catch (SmtpException ex)
            {
                Console.WriteLine("Exception caught in CreateTestMessage2(): {0}",
                    ex.ToString());
            }

        }

        protected void btnSentStatus_Click(object sender, EventArgs e)
        {
            UseImap();

            // UsePop3Client();
        }

        private void UsePop3Client()
        {
            using (var client = new Pop3Client())
            {
                client.Connect("mail.test.com", 993, false);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate("test@test.com", "password");

                for (int i = 0; i < client.Count; i++)
                {
                    var message = client.GetMessage(i);
                    lblerror.Text += string.Format("<br /> Subject: {0}", message.Subject);
                }

                client.Disconnect(true);
            }
        }

        private void UseImap()
        {
            using (var client = new ImapClient())
            {
                using (var cancel = new CancellationTokenSource())
                {
                    //client.Timeout = 200000;
                    client.Connect("imap.test.com", 143, SecureSocketOptions.None, cancel.Token);

                    // If you want to disable an authentication mechanism,
                    // you can do so by removing the mechanism like this:no
                    client.AuthenticationMechanisms.Remove("XOAUTH");

                    client.Authenticate("test@test.com", "password", cancel.Token);

                    // The Inbox folder is always available...
                    var inbox = client.Inbox;
                    inbox.Open(FolderAccess.ReadOnly, cancel.Token);

                    lblerror.Text = string.Format("Total messages: {0}", inbox.Count);
                    lblerror.Text += string.Format("<br />Recent messages: {0}", inbox.Recent);

                    // download each message based on the message index
                    //for (int i = 0; i < inbox.Count; i++)
                    //{
                    //    var message = inbox.GetMessage(i, cancel.Token);
                    //    Console.WriteLine("Subject: {0}", message.Subject);
                    //}

                    //// let's try searching for some messages...

                    //var query = SearchQuery.BodyContains(txttitle.Text).And(SearchQuery.BodyContains(txtToAddress.Text));

                    //foreach (var uid in inbox.Search(query, cancel.Token))
                    //{
                    //    var message = inbox.GetMessage(uid, cancel.Token);
                    //    lblerror.Text += string.Format("<br /> [match] {0}: {1}", uid, message.Subject);
                    //}

                    client.Disconnect(true, cancel.Token);
                }
            }
        }
    }
}

