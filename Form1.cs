using System;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SonicWallManager
{
    public partial class Form1 : Form
    {
        private const string ApiUsername = "admin";
        private const string ApiPassword = "password";

        // Session objects
        private HttpClient client;
        private HttpClientHandler handler;
        private CookieContainer cookies;

        public Form1()
        {
            InitializeComponent();

            // Ignore SSL certificate warnings from firewall
            ServicePointManager.ServerCertificateValidationCallback =
                (sender, certificate, chain, sslPolicyErrors) => true;

            ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Tls12;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string firewallIp = txtFirewallIp.Text.Trim();
            string[] passwordsToTry = { "password", "@duqu1ty" };

            if (string.IsNullOrEmpty(firewallIp))
            {
                MessageBox.Show("Please enter SonicWall Firewall IP.");
                return;
            }

            try
            {
                lblStatus.Text = "Status: Connecting...";
                lblStatus.ForeColor = Color.Orange;

                // Initialize connection objects
                cookies = new CookieContainer();
                handler = new HttpClientHandler()
                {
                    CookieContainer = cookies,
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                client = new HttpClient(handler) { BaseAddress = new Uri($"https://{firewallIp}") };
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                bool loginSuccess = false;

                // Loop through the password array
                foreach (var pwd in passwordsToTry)
                {
                    lblStatus.Text = $"Status: Trying {pwd}...";

                    // Generate auth header for the current password
                    string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{ApiUsername}:{pwd}"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);

                    // Instantiate a fresh StringContent object for every request attempt
                    using (var body = new StringContent("{\"override\": true}", Encoding.UTF8, "application/json"))
                    {
                        HttpResponseMessage response = await client.PostAsync("/api/sonicos/auth", body);

                        if (response.IsSuccessStatusCode)
                        {
                            loginSuccess = true;
                            break; // Exit the loop as soon as authentication succeeds
                        }
                    }
                }

                if (loginSuccess)
                {
                    lblStatus.Text = "Status: Connected";
                    lblStatus.ForeColor = Color.Green;

                    this.Hide();
                    DashboardForm dashboard = new DashboardForm(client, firewallIp);
                    dashboard.ShowDialog();

                    // Reset session and UI after closing Dashboard
                    client?.Dispose();
                    handler?.Dispose();
                    client = null;
                    handler = null;
                    cookies = null;

                    this.Show();
                    lblStatus.Text = "Status: Disconnected";
                    lblStatus.ForeColor = Color.Black;
                }
                else
                {
                    lblStatus.Text = "Status: Authentication Failed";
                    lblStatus.ForeColor = Color.Red;
                    MessageBox.Show("Authentication failed for all provided passwords.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Clean up client on failure
                    client?.Dispose();
                    handler?.Dispose();
                    client = null;
                    handler = null;
                    cookies = null;
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Status: Connection Error";
                lblStatus.ForeColor = Color.Red;
                MessageBox.Show("Error connecting to firewall:\n" + ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Clean up client on exception
                client?.Dispose();
                handler?.Dispose();
                client = null;
                handler = null;
                cookies = null;
            }
        }
    }
}