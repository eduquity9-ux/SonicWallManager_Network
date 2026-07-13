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

            // Ignore SSL certificate warnings from firewall importan t
            ServicePointManager.ServerCertificateValidationCallback =
                (sender, certificate, chain, sslPolicyErrors) => true;

            ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Tls12;
        }

      /*  private async void btnLogin_Click(object sender, EventArgs e)
        {
            string firewallIp = txtFirewallIp.Text.Trim();

            if (string.IsNullOrEmpty(firewallIp))
            {
                MessageBox.Show("Please enter SonicWall Firewall IP.");
                return;
            }

            try
            {
                lblStatus.Text = "Status: Connecting...";
                lblStatus.ForeColor = Color.Orange;

                // Create cookie container for SonicWall session
                cookies = new CookieContainer();

                handler = new HttpClientHandler()
                {
                    CookieContainer = cookies,
                    ServerCertificateCustomValidationCallback =
                        (message, cert, chain, errors) => true
                };

                // Create HttpClient with handler
                client = new HttpClient(handler)
                {
                    BaseAddress = new Uri($"https://{firewallIp}")
                };

                // Basic Authentication header
                string auth = Convert.ToBase64String(
                    Encoding.ASCII.GetBytes($"{ApiUsername}:{ApiPassword}"));

                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Basic", auth);

                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                // Authentication body
                var body = new StringContent(
                    "{\"override\": true}",
                    Encoding.UTF8,
                    "application/json");

                // Authenticate with SonicWall API
                HttpResponseMessage response =
                    await client.PostAsync("/api/sonicos/auth", body);

                if (response.IsSuccessStatusCode)
                {
                    lblStatus.Text = "Status: Connected";
                    lblStatus.ForeColor = Color.Green;


                    this.Hide();

                    DashboardForm dashboard = new DashboardForm(client, firewallIp);
                    dashboard.ShowDialog();
                    // reset UI after logout
                    lblStatus.Text = "Status: Disconnected";
                    lblStatus.ForeColor = Color.Black;

                    handler = null;
                    cookies = null;
                    client = null;

                    this.Show();
                }
                else
                {
                    lblStatus.Text = "Status: Authentication Failed";
                    lblStatus.ForeColor = Color.Red;

                    string error = await response.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        "Authentication failed.\n\n" + error,
                        "Login Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Status: Connection Error";
                lblStatus.ForeColor = Color.Red;

                MessageBox.Show(
                    "Error connecting to firewall:\n" + ex.Message,
                    "Connection Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        } */

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string firewallIp = txtFirewallIp.Text.Trim();
            string[] passwordsToTry = { "password", "@duqu1ty" }; // Your passwords list

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

                var body = new StringContent("{\"override\": true}", Encoding.UTF8, "application/json");

                bool loginSuccess = false;

                // Loop through the password array
                foreach (var pwd in passwordsToTry)
                {
                    lblStatus.Text = $"Status: Trying {pwd}...";

                    // Generate auth header for the current password
                    string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{ApiUsername}:{pwd}"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);

                    HttpResponseMessage response = await client.PostAsync("/api/sonicos/auth", body);

                    if (response.IsSuccessStatusCode)
                    {
                        loginSuccess = true;
                        break; // Exit the loop as soon as one works
                    }
                }

                if (loginSuccess)
                {
                    lblStatus.Text = "Status: Connected";
                    lblStatus.ForeColor = Color.Green;

                    this.Hide();
                    DashboardForm dashboard = new DashboardForm(client, firewallIp);
                    dashboard.ShowDialog();

                    // Cleanup on logout
                    this.Show();
                    lblStatus.Text = "Status: Disconnected";
                    lblStatus.ForeColor = Color.Black;
                }
                else
                {
                    lblStatus.Text = "Status: Authentication Failed";
                    lblStatus.ForeColor = Color.Red;
                    MessageBox.Show("Authentication failed for all provided passwords.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Status: Connection Error";
                lblStatus.ForeColor = Color.Red;
                MessageBox.Show("Error connecting to firewall:\n" + ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}